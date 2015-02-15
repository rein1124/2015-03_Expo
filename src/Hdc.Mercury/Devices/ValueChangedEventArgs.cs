using System;

namespace Hdc.Mercury
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public object Sender { get; set; }

        public T OldValue { get; set; }

        public T NewValue { get; set; }
    }
}