using System;

namespace Hdc.Mvvm.Dialogs
{
    /// <summary>
    /// string is default message. ob is messageUpdater
    /// </summary>
    public interface IBusyIndicatorDialogService : IObserverDialogService<string>
    {
    }
}