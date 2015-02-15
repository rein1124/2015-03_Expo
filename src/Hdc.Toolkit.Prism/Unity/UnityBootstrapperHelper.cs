using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using System;

namespace Hdc.Prism.Unity
{
    public class UnityBootstrapperHelper
    {
        private readonly Application _application;

        private readonly UnityBootstrapper _bootstrapper;

        public UnityBootstrapperHelper(Application application, UnityBootstrapper bootstrapper)
        {
            _application = application;
            _bootstrapper = bootstrapper;
            application.Startup += new StartupEventHandler(application_Startup);
        }

        private void application_Startup(object sender, StartupEventArgs e)
        {
#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            _application.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void RunInDebugMode()
        {
            _bootstrapper.Run();
        }

        private void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                _bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

//            ExceptionPolicy.HandleException(ex, "Default Policy");
            MessageBox.Show("Mercury UnhandledException");
            Environment.Exit(1);
        }
    }
}