using Hdc.Mvvm;
using Hdc.Unity;
using ODM.Presentation.ViewModels.Inspection;
using ODM.Presentation.ViewModels.Reporting;
using ODM.Presentation.ViewModels.Schemas;
using Shared;

namespace ODM.Presentation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    public class ViewModelsModule : Hdc.Prism.Modularity.ViewModelsModule
    {
        public override void Initialize()
        {
            ViewModels();

            Container.RegisterType<WorkpieceInfoEntryViewModel, WorkpieceInfoEntryViewModel>();
//            Container.RegisterType<IDefectInfoViewModel, DefectInfoViewModel>();
//            Container.RegisterType<IMeasurementInfoViewModel, MeasurementInfoViewModel>();

            Container.RegisterType<IProductionSchemaViewModel, ProductionSchemaViewModel>();
            Container.RegisterType<IParameterEntryViewModel, ParameterEntryViewModel>();

            Container.RegisterTypeWithLifetimeManager<IPreviewReportingDialogService, PreviewReportingDialogService>();

            Container.RegisterType<IMachineViewModel, MachineViewModel>();

            Container.RegisterTypeWithLifetimeManager<IMachineViewModelProvider, MachineViewModelProvider>();
            Container.RegisterTypeWithLifetimeManager<IInspectionViewModelService, InspectionViewModelService>();
        }

        private void ViewModels()
        {
            Container.RegisterViewModelWithLifetimeManager<ProductionSchemaManagerViewModel>(
                ViewModelNames.Schemas_ProductionSchemaManager);

            Container.RegisterViewModelWithLifetimeManager<InspectionManagerViewModel>(
                ViewModelNames.Inspection_InspectionManager);

            Container.RegisterViewModelWithLifetimeManager<ReportingManagerViewModel>(
                ViewModelNames.Inspection_ReportingManager);

            Container.RegisterViewModelWithLifetimeManager<InfoBarViewModel>(
                ViewModelNames.InfoBar);

            Container.RegisterViewModelWithLifetimeManager<ProductionInfoMonitorViewModel>(
                ViewModelNames.ProductionInfoMonitor);

            Container.RegisterViewModelWithLifetimeManager<TestManagerViewModel>(
                ViewModelNames.TestManager);
        }
    }
}