using System.Windows.Input;

namespace Hdc.Mvvm.Dialogs
{
    public interface IInt32NumberInputDialogPromptViewModel
    {
        int MaxValue { get; set; }

        int MinValue { get; set; }

        int DefaultValue { get; set; }

        string ValueName { get; set; }

        int Value { get; set; }

        ICommand ConfirmCommand { get; }

        ICommand CancelCommand { get; }
    }
}