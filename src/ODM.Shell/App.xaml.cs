using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hdc.Mercury;
using Hdc.Mercury.Communication.OPC.Xi;
using Hdc.Prism;
using Hdc.Prism.Commands;
using Hdc.Prism.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ODM.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //        private FirstFloor.XamlSpy.XamlSpyService service;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

//            var x = Math.Sqrt(4);

            //            this.service = new FirstFloor.XamlSpy.XamlSpyService();
            //            this.service.StartService();

            //            new ScreenSchema( new ScreenInfo( "Top",
            //                                 new ScreenInfo( "P1" ),
            //                                 new ScreenInfo( "P2" ) ) )
            //                                 .SerializeToXamlFile( @"ScreenSchema.Sample.xaml" );
            var fi = new FileInfo(typeof(App).Assembly.FullName);
            var dir = fi.DirectoryName;
            AppDomain.CurrentDomain.SetData("DataDirectory", dir);
            var s = AppDomain.CurrentDomain.GetData("DataDirectory");

            new BootstrapperRunner()
                .Run(
                    boot =>
                    boot.CreateModuleCatalogFromXaml(@"/ODM.ModuleCatalogs;component/ModulesCatalog.xaml")
                        .RegisterShell<ShellWindow>()
                        .ShowShell()
                        //                        .CreateScreenSchemaFromXaml( @"/PPH.Shell;component/ScreenSchema.xaml" )
                        .Skip()
                        .AddExtension<MercuryBootstrapperExtension>()
                        .AddExtension<XiBootstrapperExtension>()
                        .Log().SetLogger(new EntLibLoggerAdapter())
                        .End())
                .Handle(ex =>
                {
                    ExceptionPolicy.HandleException(ex, "Default Policy");
                    MessageBox.Show("ODM Unhandled Exception!\n\n" + ex.Message + ex.StackTrace);
                    Environment.Exit(1);
                });
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (HostCommands.ShutdownCommand.CanExecute(e))
                HostCommands.ShutdownCommand.Execute(e);
        }
    }
}
