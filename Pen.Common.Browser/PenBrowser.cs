using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using CefSharp.Event;


namespace Pen.Common.Browser
{
    /// <summary>
    /// 1：支持多实例
    /// 2：包含功能 缩放、刷新、关闭、cookie操作、JS脚本操作、代理支持
    /// </summary>
    public partial class PenBrowser : UserControl
    {
        public IWinFormsWebBrowser Browser { get; private set; }
        public BroserSetting Setting { get; set; }

        public PenBrowser(Action<string, int, BroserSetting, string> openNewTab, string url, BroserSetting setting = null)
        {
            InitializeComponent();
            this.Setting = setting == null ? new BroserSetting() : setting;

            GlobalSettings.Init();
            var browser = new ChromiumWebBrowser(url) { Dock = DockStyle.Fill };

            browser.LifeSpanHandler = new PenLifeSpanHandler(openNewTab, this.Setting);
            browser.RequestHandler = new PenRequestHandler(openNewTab, this.Setting);
            browser.MenuHandler = new PenMenuHandler();
            browser.RenderProcessMessageHandler = new PenRenderProcessMessageHandler(OnContextCreated);

            browser.LoadingStateChanged += this.LoadingStateChanged;
            browser.FrameLoadStart += this.FrameLoadStart;
            browser.FrameLoadEnd += this.FrameLoadEnd;
            

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            browser.JavascriptObjectRepository.ObjectBoundInJavascript += ObjectBoundInJavascript;
            browser.JavascriptObjectRepository.Register("pen", new BoundObject(this.Setting.VisitorJsEvent == null ? this.DefaultVisitorJs : this.Setting.VisitorJsEvent), true);

            if (this.Setting.CookiesSetting != null && this.Setting.CookiesSetting.Cookies != null)
            {
                this.SetCookie(this.Setting.CookiesSetting.Cookies);
            }

            this.Browser = browser;
            this.browserPanel.Controls.Add(browser);
        }

        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="zoomIncrement">+放大、-减小</param>
        public void SettingZoomLevel(double zoomIncrement)
        {
            var task = this.Browser.GetZoomLevelAsync();
            task.ContinueWith(previous =>
            {
                if (previous.Status == TaskStatus.RanToCompletion)
                {
                    var currentLevel = previous.Result;
                    this.Browser.SetZoomLevel(currentLevel + zoomIncrement);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="ignoreCache">忽略缓存</param>
        public void Reload(bool ignoreCache)
        {
            this.Browser.Reload(ignoreCache);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (this.Browser != null && !this.Browser.IsDisposed)
            {
                this.Browser.Dispose();
            }
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        public async void DeleteCookie(string url = null, string name = null)
        {
            var cookieManager = Cef.GetGlobalCookieManager();
            await cookieManager.DeleteCookiesAsync(url, name);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookies"></param>
        public async void SetCookie(List<PenCookieItem> cookies)
        {
            var cookieManager = Cef.GetGlobalCookieManager();
            foreach (var cookie in cookies)
            {
                await cookieManager.SetCookieAsync(cookie.IsHttps ? "https://" : "http://" + cookie.Domain, new CefSharp.Cookie
                {
                    Creation = cookie.Creation,
                    Domain = cookie.Domain,
                    Expires = cookie.Expires,
                    HttpOnly = cookie.HttpOnly,
                    LastAccess = cookie.LastAccess,
                    Name = cookie.Name,
                    Path = cookie.Path,
                    Secure = cookie.Secure,
                    Value = cookie.Value
                });
            }
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script"></param>
        public void ExecuteJs(string script, int iFrameIndex = 0)
        {
            this.Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(script);

            var list = this.Browser.GetBrowser().GetFrameNames();
            if (iFrameIndex > 0)
            {
                this.Browser.GetBrowser().GetFrame(list[iFrameIndex]).ExecuteJavaScriptAsync(script);
            }
        }

        private void DefaultVisitorSendCookie(CefSharp.Cookie cookie)
        {
            var cookies = cookie.Domain.TrimStart('.') + "  " + cookie.Name + "  " + cookie.Value;
            Log.Loging.Info("获取到Cookies 信息：" + cookies);
        }

        private object DefaultVisitorJs(object obj)
        {
            Log.Loging.Info("JS CallInfo " + obj.ToString());
            return null;
        }

        private void FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            var browser = (ChromiumWebBrowser)sender;
            if (e.Frame.IsMain)
            {
                SettingZoomLevel(this.Setting.ZoomLevel);
            }
        }

        private void FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (this.Setting.CookiesSetting != null)
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                CookieVisitor visitor = new CookieVisitor();

                visitor.SendCookie += this.Setting.CookiesSetting.VisitorSendCookieEvent == null ? this.DefaultVisitorSendCookie : this.Setting.CookiesSetting.VisitorSendCookieEvent;
                cookieManager.VisitAllCookies(visitor);
            }
        }


        private void LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                if (this.Setting != null && !string.IsNullOrEmpty(this.Setting.InjectJS))
                {
                    this.ExecuteJs(this.Setting.InjectJS);
                }
            }
        }

        private void OnContextCreated(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {
            pic_loading.InvokeOnUiThreadIfRequired(() =>
            {
                this.pic_loading.Visible = false;
            });
        }

        private void ObjectBoundInJavascript(object sender, JavascriptBindingCompleteEventArgs e)
        {
            Log.Loging.Info($"Js 处理对象 {e.ObjectName} 绑定成功.");
        }
    }
}
