using System;

namespace Hdc.Mvvm.Dialogs
{
    public class MessagePocoDialogArg : SimplePocoDialogArg
    { 
        public MessagePocoDialogArg(Action cancelAction)
            : base(cancelAction)
        {
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (Equals(_message, value)) return;
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }
    }
}