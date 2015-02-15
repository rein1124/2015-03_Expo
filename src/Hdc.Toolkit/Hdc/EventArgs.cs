namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventArgs<T> :EventArgs
    {
        public T Data { get; set; }

        public EventArgs()
        {
        }

        public EventArgs(T data)
        {
            Data = data;
        }
    }
}