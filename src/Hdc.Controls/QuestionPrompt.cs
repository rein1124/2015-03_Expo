using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class QuestionPrompt : Control
    {
        static QuestionPrompt()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (QuestionPrompt),
                                                     new FrameworkPropertyMetadata(typeof (QuestionPrompt)));
        }

        #region YesCommand

        public ICommand YesCommand
        {
            get { return (ICommand) GetValue(YesCommandProperty); }
            set { SetValue(YesCommandProperty, value); }
        }

        public static readonly DependencyProperty YesCommandProperty = DependencyProperty.Register(
            "YesCommand", typeof (ICommand), typeof (QuestionPrompt));

        #endregion

        #region NoCommand

        public ICommand NoCommand
        {
            get { return (ICommand) GetValue(NoCommandProperty); }
            set { SetValue(NoCommandProperty, value); }
        }

        public static readonly DependencyProperty NoCommandProperty = DependencyProperty.Register(
            "NoCommand", typeof (ICommand), typeof (QuestionPrompt));

        #endregion

        #region CancelCommand

        public ICommand CancelCommand
        {
            get { return (ICommand) GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
            "CancelCommand", typeof (ICommand), typeof (QuestionPrompt));

        #endregion

        #region Question

        public string Question
        {
            get { return (string) GetValue(QuestionProperty); }
            set { SetValue(QuestionProperty, value); }
        }

        public static readonly DependencyProperty QuestionProperty = DependencyProperty.Register(
            "Question", typeof (string), typeof (QuestionPrompt));

        #endregion
    }
}