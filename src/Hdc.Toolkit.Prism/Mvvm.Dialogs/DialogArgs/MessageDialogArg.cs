using System;
using System.Windows;

namespace Hdc.Mvvm.Dialogs
{
    public class MessageDialogArg : SimpleDialogArg
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
            typeof(string),
            typeof(MessageDialogArg));

        public MessageDialogArg(Action cancelAction)
            : base(cancelAction)
        {
        }

        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }
    }
}