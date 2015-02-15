using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface IDoubleNumberInputDialogService//: IGeneralOutputDialogService<double>
    {
        IObservable<DialogEventArgs<double>> Show(double minValue, double maxValue,string titile=null);
    }
}