using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public class CookieVisitor : CefSharp.ICookieVisitor
    {
        public event VisitorSendCookieHandle SendCookie;

        public void Dispose()
        {
            SendCookie = null;
        }

        public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            deleteCookie = false;
            if (SendCookie != null)
            {
                SendCookie(cookie);
            }

            return true;
        }
    }
}
