using System;
using System.Reactive.Subjects;
using Microsoft.Practices.Prism;

namespace Hdc.Prism
{
    public class BootstrapperRunner
    {
        private Func<Bootstrapper> _createBootstrapper;

        private Action<Exception> _exceptionHandler;

        public IHandlerException Run(Action<HdcPrismBootstrapper> createBootstrapper)
        {
            _createBootstrapper = () =>
                                      {
                                          var boot = new HdcPrismBootstrapper();
                                          createBootstrapper(boot);
                                          return boot;
                                      };

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            return new HandlerException(this);
        }

        private void RunInDebugMode()
        {
            _createBootstrapper().Run();
        }

        private void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => HandleException(e.ExceptionObject as Exception);

            try
            {
                _createBootstrapper().Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            if (_exceptionHandler != null)
                _exceptionHandler(ex);
        }

        public interface IHandlerException
        {
            void Handle(Action<Exception> exceptionHandler);
        }

        private class HandlerException : IHandlerException
        {
            private readonly BootstrapperRunner _bootstrapperRunner;

            public HandlerException(BootstrapperRunner bootstrapperRunner)
            {
                _bootstrapperRunner = bootstrapperRunner;
            }

            public void Handle(Action<Exception> exceptionHandler)
            {
                _bootstrapperRunner._exceptionHandler = exceptionHandler;
            }
        }
    }
}