using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using Hdc.Unity;
using ODM.Domain;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using ODM.Domain.Reporting;
using ODM.Domain.Schemas;

namespace ODM.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    public class InfrastructureModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            Database.SetInitializer(
                Container.Resolve<DatasContext.DatasContextCreateDatabaseIfNotExistsInitializer>());

            Database.SetInitializer(
                Container.Resolve<DatasContext.DatasContextDropCreateDatabaseIfModelChangesInitializer>());

            Container.RegisterTypeWithLifetimeManager<
                IDatasContext,
                DatasContext>();

            Container.RegisterTypeWithLifetimeManager<
                IWorkpieceInfoRepository,
                WorkpieceInfoRepository>();

            Container.RegisterTypeWithLifetimeManager<
                IParameterEntryRepository,
                ParameterEntryRepository>();

            Container.RegisterTypeWithLifetimeManager<
                IMachineConfigSaveLoadService,
                XamlMachineConfigSaveLoadService>();

            Container.RegisterTypeWithLifetimeManager<
                IReportExporter,
                ExcelReportExporter2>();

        }

    }
}