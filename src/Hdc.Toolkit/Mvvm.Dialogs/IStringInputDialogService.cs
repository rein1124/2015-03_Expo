using System;
using Hdc.Mvvm;

namespace Hdc.Mvvm.Dialogs
{
    public interface IStringInputDialogService
    {
//        StringInputDialogArg DialogArg { get; }

        IObservable<DialogEventArgs<string>> Show(string title,string defaultString);
        IObservable<DialogEventArgs<string>> Show(string title,string defaultString,int maxLength);
    }
}