using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface IInt32NumberInputDialogService
    {
        IObservable<DialogEventArgs<int>> Show(int minValue, int maxValue, int defaultValue, string valueName);

    }
}