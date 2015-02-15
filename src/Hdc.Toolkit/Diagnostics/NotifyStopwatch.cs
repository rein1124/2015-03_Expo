namespace Hdc.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class NotifyStopwatch : Stopwatch, IDisposable
    {
        private readonly string _message;

        public NotifyStopwatch(string message)
        {
            _message = message;
            Start();
        }

        public void Dispose()
        {
            Stop();
            Debug.WriteLine("<< Watch Stoped >> " + _message + " :: " + Elapsed.TotalMilliseconds.ToString());
        }
    }
}