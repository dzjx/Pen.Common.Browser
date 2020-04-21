using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser
{
    public class BoundObject
    {
        private event Func<object, object> VisitorJsEvent;

        public BoundObject(Func<object, object> visitorJsEvent)
        {
            this.VisitorJsEvent += visitorJsEvent;
        }

        public object SendAndWait(object obj)
        {
            return this.VisitorJsEvent?.Invoke(obj);
        }

        public void Send(object obj)
        {
            this.VisitorJsEvent?.Invoke(obj);
        }
    }
}
