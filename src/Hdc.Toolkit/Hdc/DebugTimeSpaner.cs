using System.Diagnostics;

namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Diagnostics;

    public class DebugTimeSpaner : IDisposable
    {
        #region FIELD

        private readonly string message;

        private readonly TimeSpan start;

        private TimeSpan finish;

        #endregion

        #region PROPERTY

        public object Container { get; private set; }

        public int During { get; private set; }

        #endregion

        #region CONSTRUCTOR

        public DebugTimeSpaner(string message)
            : this(null, message)
        {
        }

        public DebugTimeSpaner(object container, string message)
        {
            Container = container;
            this.message = message;
            Debug.WriteLine(message + " ©³ Start  ©·"); // + During + " ms.©·\n");
            start = new TimeSpan(DateTime.Now.Ticks);
        }

        #endregion

        public int Stop()
        {
            finish = new TimeSpan(DateTime.Now.Ticks);
            During = (int)((finish - start).TotalMilliseconds);
            Debug.WriteLine(message + " ©» Finish ©¿" + During + " ms.\n");
            return During;
        }

        public void Dispose()
        {
            Stop();
        }
    }

}