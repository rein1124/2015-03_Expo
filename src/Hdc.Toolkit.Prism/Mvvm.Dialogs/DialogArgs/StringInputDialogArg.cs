using System;

namespace Hdc.Mvvm.Dialogs
{
    public class StringInputDialogArg : InputDialogArg<string>
    {
        public StringInputDialogArg(Action cancelAction, Action<string> confirmAction)
            : base(cancelAction, confirmAction)
        {
        }
    }
}