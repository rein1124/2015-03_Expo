using Hdc.Patterns;
using Hdc.Unity;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using ODM.Domain.Reporting;
using ODM.Domain.Schemas;

namespace ODM.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;
    using Shared;

    public class DomainModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            Container.RegisterTypeWithLifetimeManager<IInspectionDomainService, InspectionDomainService>();
            Container.RegisterTypeWithLifetimeManager<IMachineConfigProvider, MachineConfigProvider>();
//            Container.RegisterTypeWithLifetimeManager<IInspectorControllerProvider, InspectorControllerProvider>();

            Container.RegisterType<IMachine, Machine>();
            Container.RegisterTypeWithLifetimeManager<IMachineProvider, MachineProvider>();
            Container.RegisterTypeWithLifetimeManager<IImageSaveLoadService, ImageSaveLoadService>();
            Container.RegisterTypeWithLifetimeManager<IReportingDomainService, ReportingDomainService>();
//            Container.RegisterType<IInspectorController, InspectorController>();
            Container.RegisterType<IInspectService, InspectService>();
        }
    }
}