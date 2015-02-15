using System;

namespace Hdc.Mv.Halcon
{
    public class HalconInspectorException: Exception
    {
        public HalconInspectorException()
        {
        }

        public HalconInspectorException(string message) : base(message)
        {
        }

        public HalconInspectorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}