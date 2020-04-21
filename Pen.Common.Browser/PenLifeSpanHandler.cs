using System;
using CefSharp.WinForms;
using CefSharp;


namespace Pen.Common.Browser
{
    public class PenLifeSpanHandler : ILifeSpanHandler
    {
        private readonly Action<string, int, BroserSetting,string> openNewTab;
        private BroserSetting broserSetting;

        public PenLifeSpanHandler(Action<string, int, BroserSetting,string> openNewTab, BroserSetting broserSetting)
        {
            this.openNewTab = openNewTab;
            this.broserSetting = broserSetting;
        }

        public bool DoClose(IWebBrowser browserControl, CefSharp.IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }

            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            var chromeBrowser = (ChromiumWebBrowser)chromiumWebBrowser;
            chromeBrowser.Invoke(new Action(() =>
            {
                openNewTab(targetUrl, -1, this.broserSetting,string.Empty);
            }));

            newBrowser = null;

            return true;
        }
    }
}
