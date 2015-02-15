using System;

namespace Hdc.Mvvm.Dialogs
{
    public class SimpleDialogArg : DialogArg<object>
    {
        public SimpleDialogArg(Action cancelAction)
            : base(cancelAction)
        {
        }

        public void Show()
        {
            Show(this);
        }
    }
}