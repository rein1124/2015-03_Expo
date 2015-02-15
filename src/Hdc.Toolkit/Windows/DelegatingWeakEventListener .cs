//ref to "WeakReference Event Handlers" 2010-09-14

using System;
using System.Windows;

namespace Hdc.Windows
{
    public class DelegatingWeakEventListener : IWeakEventListener
    {
        private readonly Delegate _handler;

        public DelegatingWeakEventListener(Delegate handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            _handler = handler;
        }

        public DelegatingWeakEventListener(EventHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            _handler = handler;
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            _handler.DynamicInvoke(sender, e);
            return true;
        }
    }

}