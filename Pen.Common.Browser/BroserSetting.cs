using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public class BroserSetting
    {
        public Func<object, object> VisitorJsEvent { get; set; }
        public PenCookie CookiesSetting { get; set; }
        public PenProxy Proxy { get; set; }
        public double ZoomLevel { get; set; }
        public string InjectJS { get; set; }
    }
}
