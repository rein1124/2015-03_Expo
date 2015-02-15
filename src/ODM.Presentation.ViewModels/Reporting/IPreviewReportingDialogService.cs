using System;
using Hdc.Mvvm.Dialogs;
using ODM.Domain.Reporting;

namespace ODM.Presentation.ViewModels.Reporting
{
    public interface IPreviewReportingDialogService
    {
        IObservable<DialogEventArgs<Report>> Show(Report report);
    }
}