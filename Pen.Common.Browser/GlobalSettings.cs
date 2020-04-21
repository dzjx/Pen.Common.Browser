using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public static class GlobalSettings
    {
        static GlobalSettings()
        {
            Cef.EnableHighDPISupport();

            // 全局代理
            //settings.CefCommandLineArgs.Add("proxy-server", ProxyAddress);
            //CefSharp.CefSharpSettings.Proxy = new CefSharp.ProxyOptions(代理ip, 端口, 用户名, 密码);
        }

        public static void Init()
        {
        }
    }
}
