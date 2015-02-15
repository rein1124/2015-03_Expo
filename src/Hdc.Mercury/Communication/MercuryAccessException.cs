using System;

namespace Hdc.Mercury.Communication
{
    public class MercuryAccessException:Exception
    {
        public MercuryAccessException()
        {
        }

        public MercuryAccessException(string message) : base(message)
        {
        }

        public MercuryAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}