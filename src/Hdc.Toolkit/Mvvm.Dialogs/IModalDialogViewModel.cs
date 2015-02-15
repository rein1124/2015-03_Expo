using System;
using System.Reactive;

namespace Hdc.Mvvm.Dialogs
{
    public interface IModalDialogViewModel
    {
        void Close();

        IObservable<Unit> Show();
    }
}