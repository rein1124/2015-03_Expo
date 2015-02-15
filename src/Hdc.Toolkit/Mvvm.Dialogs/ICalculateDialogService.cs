using System;
using System.Windows;
using Hdc.Mvvm;

namespace Hdc.Mvvm.Dialogs
{
    public interface ICalculateDialogService // : IDialogViewModel
    {
//        Action<double> ConfirmAction { get; set; }
//        Action CancelAction { get; set; }
//
//        double MaxValue { get; set; }
//        double MinValue { get; set; }
//        double UnitValue { get; set; }
//        double DefaultValue { get; set; }
//        string ValueName { get; set; }
//        NumberInputDialogArg DialogArg { get; }


        IObservable<DialogEventArgs<double>> Show(double minValue,
                                                  double maxValue,
                                                  double unitValue,
                                                  double defaultValue,
                                                  string valueName);
    }
}