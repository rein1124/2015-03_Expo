using Hdc.Prism.Regions;
using ODM.Presentation.Views.Inspection;
using ODM.Presentation.Views.Reporting;
using PPG.Presentation.Views.Dialogs;
using Shared;

namespace ODM.Presentation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    public class ViewsModule : Hdc.Prism.Modularity.ViewsModule
    {
        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion<ProductionSchemaManagerView>(
                RegionNames.ParametersScreen_ProductionSchemaManager);

            RegionManager.RegisterViewWithRegion<ReportingManagerView>(
                RegionNames.ReportingScreen_ReportingManager);

            RegionManager.RegisterViewWithRegion<InspectionManagerView>(
                RegionNames.MonitorScreen_InspectionManager);

            RegionManager.RegisterViewWithRegion<InfoBarView>(
                RegionNames.MonitorScreen_InfoBar);

            RegionManager.RegisterViewWithRegion<ProductionInfoMonitorView>(
                RegionNames.MonitorScreen_ProductionInfoMonitor);

            RegionManager.RegisterViewWithRegion<TestManagerView>(
                RegionNames.TestScreen_TestManager);

            Container.RegisterType<object, MessageDialog2View>(DialogNames.MessageDialog);
            Container.RegisterType<object, BusyDialogView>(DialogNames.BusyDialog);
            Container.RegisterType<object, AskDialogView2>(DialogNames.AskDialog);
            Container.RegisterType<object, CalculateDialog2View>(DialogNames.Calculate);
            Container.RegisterType<object, StringInputDialogView>(DialogNames.StringInput);

            Container.RegisterType<object, PreviewReportingDialogView>(DialogNames.Reporting_PreviewReportingDialog);
        }
    }
}