using System;
using System.Windows;
using Hdc.Mvvm;

namespace Hdc.Mvvm.Dialogs
{
    public interface IAskDialogService: IGeneralInputOutputDialogService<string, bool>
    {
        //IObservable<DialogEventArgs<bool>> Show(string question);
    }
}