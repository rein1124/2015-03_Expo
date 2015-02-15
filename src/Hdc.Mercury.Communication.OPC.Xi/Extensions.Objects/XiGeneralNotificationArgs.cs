using System;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiGeneralNotificationArgs : EventArgs
    {
        public string info { get; set; }

        public Exception ex { get; set; }

        public object tag { get; set; }
    }
}