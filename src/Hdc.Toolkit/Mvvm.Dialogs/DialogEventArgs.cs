using System;

namespace Hdc.Mvvm.Dialogs
{
    public class DialogEventArgs : EventArgs
    {
        public DialogEventArgs(bool iscanceled = false)
        {
            IsCanceled = iscanceled;
        }

        public bool IsCanceled { get; set; }
    }
}