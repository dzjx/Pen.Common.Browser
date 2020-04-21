using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public class PenRenderProcessMessageHandler : IRenderProcessMessageHandler
    {
        Action<IWebBrowser, IBrowser, IFrame> ContextCreated;
        public PenRenderProcessMessageHandler(Action<IWebBrowser, IBrowser, IFrame> contextCreated)
        {
            this.ContextCreated = contextCreated;
        }

        void IRenderProcessMessageHandler.OnFocusedNodeChanged(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IDomNode node)
        {

        }

        void IRenderProcessMessageHandler.OnContextCreated(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {
            ContextCreated?.Invoke(chromiumWebBrowser, browser, frame);
        }

        void IRenderProcessMessageHandler.OnContextReleased(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {

        }

        void IRenderProcessMessageHandler.OnUncaughtException(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, JavascriptException exception)
        {

        }
    }
}
