using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Hdc.Boot;
using Hdc.Generators;
using Hdc.Modularity;
using Hdc.Mvvm;
using Hdc.Mvvm.Dialogs;
using Hdc.Mvvm.Navigation;
using Hdc.Mvvm.Resources;
using Hdc.Patterns;
using Hdc.Unity;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace Hdc.Prism
{
    public class HdcPrismBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.AddNewExtension<LazySupportExtension>();

            Container.RegisterTypeWithLifetimeManager<
                IViewModelRegister,
                ServiceLocatorViewModelProvider>();
            Container.RegisterTypeWithLifetimeManager<
                IViewModelProvider,
                ServiceLocatorViewModelProvider>();

            ViewModelManager.Singleton.ViewModelProvider = Container.Resolve<IViewModelProvider>();

            Container.RegisterTypeWithLifetimeManager<ICommandBus, CommandBus>();
            Container.RegisterTypeWithLifetimeManager<IEventBus, EventBus>();
            Container.RegisterTypeWithLifetimeManager<IEventAggregator, EventBus>();

            Container.RegisterTypeWithLifetimeManager<
                IIdentityGenerator,
                SequentialIdentityGenerator>();
            IdentityGeneratorServiceLocator.IdentityGenerator = Container.Resolve<IIdentityGenerator>();

            Init_ComponentModule();

            Init_Dialogs();


            if (_screenSchema != null)
            {
                Container.RegisterTypeWithLifetimeManager<
                    IScreenProvider,
                    ScreenProvider>();

                Container.RegisterType<IScreen, Screen>();

                ScreenManager.Singleton.ScreenProvider = Container.Resolve<IScreenProvider>();
                var screenProvider = ScreenManager.Singleton.ScreenProvider as ScreenProvider;

                if (screenProvider != null)
                {
                    screenProvider.Initialize(_screenSchema);
                    var screenEventPublisher = Container.Resolve<ScreenEventPublisher>();
                }
            }

            //Register shell
            Container.RegisterType(typeof (IShell), _shellType, new ContainerControlledLifetimeManager());

            InitialzeResources();

            foreach (var extType in _extensionTypes)
            {
                var extension = Container.Resolve(extType) as IBootstrapperExtension;
                extension.Initialize(Container);
                _extensions.Add(extension);
            }
        }

        private void Init_Dialogs()
        {
            Container.RegisterTypeWithLifetimeManager<
                IAskDialogService,
                AskDialogService>();

            Container.RegisterTypeWithLifetimeManager<
                IAsk3DialogService,
                Ask3DialogService>();

            Container.RegisterTypeWithLifetimeManager<
                IBusyDialogService,
                BusyDialogService>();

            Container.RegisterTypeWithLifetimeManager<
                ICalculateDialogService,
                CalculateDialogService>();

            Container.RegisterTypeWithLifetimeManager<
                IMessageDialogService,
                MessageDialogService>();

            Container.RegisterTypeWithLifetimeManager<
                IStringInputDialogService,
                StringInputDialogService>();
        }

        private void Init_ComponentModule()
        {
            Container.RegisterTypeWithLifetimeManager<
                IComponentRegionUpdater,
                ComponentRegionUpdater>();

            Container.RegisterTypeWithLifetimeManager<
                IComponentWriter,
                IComponentWriter>();

            Container.RegisterTypeWithLifetimeManager<
                IComponentMap,
                ComponentMap>();

            Container.RegisterTypeWithLifetimeManager<
                IComponentManager,
                ComponentManager>();

            Container.RegisterTypeWithLifetimeManager<
                IComponentReader,
                ComponentReader>();
        }

        protected virtual void InitialzeResources()
        {
            DrawingBrushServiceLocator.Loader = new XamlDrawingBrushLoader();
        }

        private IModuleCatalog _moduleCatalog;
        private Type _shellType;
        private bool _isShellShow;
        private ScreenSchema _screenSchema;
        private IList<IBootstrapperExtension> _extensions = new List<IBootstrapperExtension>();
        private IList<Type> _extensionTypes = new List<Type>();

        public ISetShell CreateModuleCatalog(IModuleCatalog moduleCatalog)
        {
            _moduleCatalog = moduleCatalog;
            return new SetShell(this);
        }

        public ISetShell CreateModuleCatalogFromXaml(string uri)
        {
            var moduleCatalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                new Uri(uri, UriKind.Relative));

            return CreateModuleCatalog(moduleCatalog);
        }


        protected override ILoggerFacade CreateLogger()
        {
            return _loggerFacadeAgent;
        }

        private LoggerFacadeAgent _loggerFacadeAgent = new LoggerFacadeAgent();

        public class LoggerFacadeAgent : ILoggerFacade
        {
            private ILoggerFacade _loggerFacade;

            public LoggerFacadeAgent()
            {
            }

            public LoggerFacadeAgent(ILoggerFacade loggerFacade)
            {
                _loggerFacade = loggerFacade;
            }

            public void Log(string message, Category category, Priority priority)
            {
                if (_loggerFacade != null)
                    _loggerFacade.Log(message, category, priority);
            }

            public ILoggerFacade LoggerFacade
            {
                get { return _loggerFacade; }
                set { _loggerFacade = value; }
            }
        }

        public interface ISetShell
        {
            IShellShow RegisterShell<T>() where T : IShell;
        }

        public class SetShell : ISetShell
        {
            private readonly HdcPrismBootstrapper _hdcPrismBootstrapper;

            public SetShell(HdcPrismBootstrapper hdcPrismBootstrapper)
            {
                _hdcPrismBootstrapper = hdcPrismBootstrapper;
            }

            public IShellShow RegisterShell<T>() where T : IShell
            {
                _hdcPrismBootstrapper._shellType = typeof (T);
                return new ShellShow(_hdcPrismBootstrapper);
            }
        }

        public interface IShellShow
        {
            ICreateScreenSchemaOption ShowShell();
            ICreateScreenSchemaOption NotShowShell();
        }

        public class ShellShow : IShellShow
        {
            private readonly HdcPrismBootstrapper _bootstrapper;

            public ShellShow(HdcPrismBootstrapper bootstrapper)
            {
                _bootstrapper = bootstrapper;
            }

            public ICreateScreenSchemaOption ShowShell()
            {
                _bootstrapper._isShellShow = true;
                return new CreateScreenSchemaOption(_bootstrapper);
            }

            public ICreateScreenSchemaOption NotShowShell()
            {
                _bootstrapper._isShellShow = false;
                return new CreateScreenSchemaOption(_bootstrapper);
            }
        }

        public interface ICreateScreenSchemaOption
        {
            IAddExtensionOption CreateScreenSchema(ScreenSchema screenSchema);
            IAddExtensionOption CreateScreenSchemaFromXaml(string uri);
            IAddExtensionOption Skip();
        }

        public class CreateScreenSchemaOption : ICreateScreenSchemaOption
        {
            private readonly HdcPrismBootstrapper _bootstrapper;

            public CreateScreenSchemaOption(HdcPrismBootstrapper bootstrapper)
            {
                _bootstrapper = bootstrapper;
            }

            public IAddExtensionOption CreateScreenSchema(ScreenSchema screenSchema)
            {
                _bootstrapper._screenSchema = screenSchema;


                return new AddExtensionOption(_bootstrapper);
            }

            public IAddExtensionOption CreateScreenSchemaFromXaml(string uri)
            {
                var screenSchema = ScreenSchema.CreateFromXaml(
                    new Uri(uri, UriKind.Relative));

                return CreateScreenSchema(screenSchema);
            }

            public IAddExtensionOption Skip()
            {
                return new AddExtensionOption(_bootstrapper);
            }
        }

        public interface IAddExtensionOption : IEnd
        {
            IAddExtensionOption AddExtension<TExtension>() where TExtension : IBootstrapperExtension;
            ILog Log();

            //            IAddExtensionOption AddExtension<TExtension>(Action<TExtension> action)
            //                where TExtension : IBootstrapperExtension;
        }

        public class AddExtensionOption : IAddExtensionOption
        {
            private HdcPrismBootstrapper _bootstrapper;

            public AddExtensionOption(HdcPrismBootstrapper bootstrapper)
            {
                _bootstrapper = bootstrapper;
            }

            public IAddExtensionOption AddExtension<TExtension>() where TExtension : IBootstrapperExtension
            {
                _bootstrapper._extensionTypes.Add(typeof (TExtension));
                return this;
            }

            public ILog Log()
            {
                return new Log(_bootstrapper);
            }

            public IAddExtensionOption AddExtension<TExtension>(Action<TExtension> action)
                where TExtension : IBootstrapperExtension
            {
                var extension = _bootstrapper.Container.Resolve<TExtension>();
                extension.Initialize(_bootstrapper.Container);
                action(extension);
                _bootstrapper._extensions.Add(extension);
                return this;
            }

            UnityBootstrapper IEnd.End()
            {
                return _bootstrapper;
            }
        }

        public interface IEnd
        {
            UnityBootstrapper End();
        }

        private class End : IEnd
        {
            private readonly HdcPrismBootstrapper _bootstrapper;

            public End(HdcPrismBootstrapper bootstrapper)
            {
                _bootstrapper = bootstrapper;
            }

            UnityBootstrapper IEnd.End()
            {
                return _bootstrapper;
            }
        }


        protected override DependencyObject CreateShell()
        {
            var shellWindow = Container.Resolve<IShell>();
            if (_isShellShow)
                shellWindow.Show();
            return shellWindow as DependencyObject;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return _moduleCatalog;
        }


        public interface ILog
        {
            IEnd SetLogger(ILoggerFacade loggerFacade);
            IEnd SetLogger<TLogger>() where TLogger : ILoggerFacade;
        }

        public class Log : ILog
        {
            private readonly HdcPrismBootstrapper _bootstrapper;

            public Log(HdcPrismBootstrapper bootstrapper)
            {
                _bootstrapper = bootstrapper;
            }

            IEnd ILog.SetLogger(ILoggerFacade loggerFacade)
            {
                _bootstrapper._loggerFacadeAgent.LoggerFacade = loggerFacade;
                return new End(_bootstrapper);
            }

            public IEnd SetLogger<TLogger>() where TLogger : ILoggerFacade
            {
                _bootstrapper._loggerFacadeAgent.LoggerFacade = _bootstrapper.Container.Resolve<TLogger>();
                return new End(_bootstrapper);
            }
        }
    }
}