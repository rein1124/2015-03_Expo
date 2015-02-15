using System;
using Hdc.Mvvm.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using ODM.Domain.Reporting;
using Shared;

namespace ODM.Presentation.ViewModels.Reporting
{
    public class PreviewReportingDialogService : RegionDialogService<Report>,
                                                 IPreviewReportingDialogService
    {
        protected override string GetDialogRegionName()
        {
            return DialogNames.Reporting_PreviewReportingDialog;
        }

        protected override string GetDialogViewName()
        {
            return DialogNames.Reporting_PreviewReportingDialog;
        }

        public IObservable<DialogEventArgs<Report>> Show(Report report)
        {
            Report = report;
            ExportFileName = null;
            return base.Show();
        }



        public DelegateCommand ConfirmCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public DelegateCommand SelectExportFileNameCommand { get; set; }

        [Dependency]
        public IMessageDialogService MessageDialogService { get; set; }

        // ReSharper disable ConvertClosureToMethodGroup
        [InjectionMethod]
        public void Init()
        {
            ConfirmCommand = new DelegateCommand(
                () =>
                    {
                        if (ExportFileName == null)
                        {
                            MessageDialogService.Show("请选择导出路径！");
                            return;
                        }

                        Close(Report);
                    });

            CancelCommand = new DelegateCommand(
                () => { Cancel(); });

            SelectExportFileNameCommand = new DelegateCommand(
                () =>
                    {
                        var dialog = new SaveFileDialog()
                                         {
                                             Filter = "XLSX files (*.xlsx)|*.xlsx|" +
                                                      "All files (*.*)|*.*"
                                         };

                        var s = dialog.ShowDialog();
                        if (s.Value)
                        {
                            ExportFileName = dialog.FileName;
                            Report.ExportFileName = ExportFileName;
                        }
                    });
        }

        // ReSharper restore ConvertToLambdaExpression

        private Report _report;

        public Report Report
        {
            get { return _report; }
            set
            {
                if (Equals(_report, value)) return;
                _report = value;
                RaisePropertyChanged(() => Report);
            }
        }

        private string _exportFileName;

        public string ExportFileName
        {
            get { return _exportFileName; }
            set
            {
                if (Equals(_exportFileName, value)) return;
                _exportFileName = value;
                RaisePropertyChanged(() => ExportFileName);
            }
        }
    }
}