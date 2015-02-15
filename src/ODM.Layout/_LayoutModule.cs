using Hdc.Mvvm;
using Hdc.Prism.Regions;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ODM.Layout.Screens;

namespace ODM.Layout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;
    using Shared;

    public class LayoutModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IViewModelRegister ViewModelRegister { get; set; }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

//        [Dependency]
//        public IPressConfigProvider PressConfigProvider { get; set; }

//        [Dependency]
//        public IEventAggregator EventAggregator { get; set; }

        public void Initialize()
        {
//            EventAggregator.PublishSplashMessageUpdatedEventWithTypeFullName<LayoutModule>();
//            RegionManager.RegisterViewWithRegion<DialogsPage>(RegionNames.DialogsPage);

            RegionManager.RegisterViewWithRegion<MainScreen>(RegionNames.MainScreen);
//            RegionManager.RegisterViewWithRegion<DialogHost>(RegionNames.Dialogs_CommonDialogsHost);

            Container.RegisterType<object, MainScreen>(ScreenNames.MainScreen);
            Container.RegisterType<object, MonitorScreen>(ScreenNames.MonitorScreen);
            Container.RegisterType<object, ParametersScreen>(ScreenNames.ParametersScreen);
            Container.RegisterType<object, ReportingScreen>(ScreenNames.ReportingScreen);
            Container.RegisterType<object, TestScreen>(ScreenNames.TestScreen);

            RegionManager.RequestNavigate(RegionNames.MainScreen_NavigationRegion, ScreenNames.MonitorScreen);
//            RegionManager.RequestNavigate(RegionNames.MainScreen_NavigationRegion, ScreenNames.ParametersScreen);
        }
    }
}