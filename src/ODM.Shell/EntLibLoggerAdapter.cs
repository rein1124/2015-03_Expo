using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;

namespace Hdc.Prism.Logging
{
    public class EntLibLoggerAdapter : ILoggerFacade
    {
        public EntLibLoggerAdapter()
        {
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        public void Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int)priority);
        }
    }
}