using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public delegate void VisitorSendCookieHandle(CefSharp.Cookie cookie);

    public class PenCookie
    {
        /// <summary>
        /// 要设置的cookie
        /// </summary>
        public List<PenCookieItem> Cookies { get; set; }

        /// <summary>
        /// 获取cookie的事件
        /// </summary>
        public VisitorSendCookieHandle VisitorSendCookieEvent { get; set; }
    }
}
