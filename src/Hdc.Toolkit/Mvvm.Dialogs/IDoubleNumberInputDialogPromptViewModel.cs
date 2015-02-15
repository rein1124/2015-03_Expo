using System.Windows.Input;

namespace Hdc.Mvvm.Dialogs
{
    public interface IDoubleNumberInputDialogPromptViewModel
    {
        double MaxValue { get; set; }

        double MinValue { get; set; }

        double Value { get; set; }

        ICommand ConfirmCommand { get; }

        ICommand CancelCommand { get; }
        
    }
}