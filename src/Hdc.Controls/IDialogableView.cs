using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Hdc.Controls
{
    public interface IDialogableView
    {
        event EventHandler<DialogResultEventArgs> ValidateDialog;

//          event EventHandler<CancelEventArgs> ValidateDialog;
    }

    public class DialogResultEventArgs : EventArgs
    {
        public DialogResultEventArgs()
        {
        }

        public DialogResultEventArgs(bool? result, object value)
        {
            Result = result;
            Value = value;
        }

        public bool? Result { get; set; }

        public object Value { get; set; }
    }
}