using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public class PenCookieItem
    {
        public bool IsHttps { get; set; }

        public DateTime Creation { get; set; }

        public string Domain { get; set; }

        public DateTime? Expires { get; set; }

        public bool HttpOnly { get; set; }

        public DateTime LastAccess { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool Secure { get; set; }

        public string Value { get; set; }
    }
}
