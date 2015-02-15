using System;
using Hdc.Mvvm;

namespace Hdc.Mvvm.Dialogs
{
    public interface ICalculatePromptViewModel
    {
        NumberInputDialogArg DialogArg { get; }

        IObservable<DialogEventArgs<double>> ClosedEvent { get; }


        void Update(double minValue,
                    double maxValue,
                    double unitValue,
                    double defaultValue,
                    string valueName);
    }
}