using Microsoft.Practices.Prism.Logging;

namespace Hdc.Prism.Logging
{
    public class VsOutputLogger : ILoggerFacade
    {

        public VsOutputLogger(string logPrefix)
        {
            _logPrefix = logPrefix;
        }

        public void Log(string message, Category category, Priority priority)
        {

            System.Diagnostics.Debug.WriteLine(
              string.Format(VsLogMsgFmt, _logPrefix, message, category, priority));
        }

        private readonly string _logPrefix;
        private const string VsLogMsgFmt = "{0}: \"{1}\"; cat: {2}, priority {3}";
    }
}