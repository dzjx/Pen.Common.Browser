using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Pen.Common.Browser
{
    public class PenRequestHandler : IRequestHandler
    {
        private readonly Action<string, int, BroserSetting, string> openNewTab;
        private BroserSetting broserSetting;

        public PenRequestHandler(Action<string, int, BroserSetting, string> openNewTab, BroserSetting broserSetting)
        {
            this.openNewTab = openNewTab;
            this.broserSetting = broserSetting;
        }

        public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            Task.Run(() =>
            {
                using (callback)
                {
                    if (broserSetting != null && broserSetting.Proxy != null && !string.IsNullOrEmpty(broserSetting.Proxy.UserName) && !string.IsNullOrEmpty(broserSetting.Proxy.Password))
                    {
                        callback.Continue(broserSetting.Proxy.UserName, broserSetting.Proxy.Password);
                        Log.Loging.Info(string.Format("{0}:{1}-{2}-{3} 应用代理密码:", host, port, realm, scheme));
                    }
                }
            });

            return true;
        }

        public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return null;
        }

        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            if (this.broserSetting != null && this.broserSetting.Proxy != null && !string.IsNullOrEmpty(this.broserSetting.Proxy.Url))
            {
                var rc = chromiumWebBrowser.GetBrowser().GetHost().RequestContext;
                var v = new Dictionary<string, object>();
                string error;

                v["mode"] = "fixed_servers";
                v["server"] = this.broserSetting.Proxy.Url;
                bool success = rc.SetPreference("proxy", v, out error);

                if (!string.IsNullOrWhiteSpace(error))
                {
                    Log.Loging.Error(error);
                }
                Log.Loging.Info(string.Format("{0} 设置当前实例代理:", this.broserSetting.Proxy.Url));
            }

            return false;
        }

        public bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            if (openNewTab == null)
            {
                return false;
            }

            var control = (Control)chromiumWebBrowser;

            control.InvokeOnUiThreadIfRequired(delegate ()
            {
                openNewTab(targetUrl, -1, this.broserSetting, string.Empty);
            });

            return true;
        }

        public void OnPluginCrashed(IWebBrowser chromiumWebBrowser, IBrowser browser, string pluginPath)
        {

        }

        public bool OnQuotaRequest(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {

        }

        public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {

        }

        public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            return false;
        }
    }
}
