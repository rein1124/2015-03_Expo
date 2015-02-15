using System;

namespace Hdc.Mvvm.Dialogs
{
    public class SimplePocoDialogArg : PocoDialogArg<object>
    {
        public SimplePocoDialogArg(Action cancelAction) : base(cancelAction)
        {
        }

        public void Show()
        {
            Show(this);
        }
    }
}