using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class MessagePrompt : Control
    {
        static MessagePrompt()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (MessagePrompt),
                                                     new FrameworkPropertyMetadata(typeof (MessagePrompt)));
        }

        #region CloseCommand

        public ICommand CloseCommand
        {
            get { return (ICommand) GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
            "CloseCommand", typeof (ICommand), typeof (MessagePrompt));

        #endregion

        #region Message

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof (string), typeof (MessagePrompt));

        #endregion
    }
}