using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Mvvm.Dialogs
{
    public class AskDialogArg : MessageDialogArg
    {
        private readonly Action _yesAction;

        private readonly Action _noAction;

        private readonly ICommand _yesCommand;

        private readonly ICommand _noCommand;

        public AskDialogArg(Action cancelAction, Action yesAction, Action noAction)
            : base(cancelAction)
        {
            _yesAction = yesAction;
            _noAction = noAction;
            _yesCommand = new DelegateCommand(
                () =>
                    {
                        if (_yesAction != null)
                        {
                            _yesAction();
                        }
                        Close();
                    });
            _noCommand = new DelegateCommand(
                () =>
                    {
                        if (_noAction != null)
                        {
                            _noAction();
                        }
                        Close();
                    });
        }

        public ICommand YesCommand
        {
            get
            {
                return _yesCommand;
            }
        }

        public ICommand NoCommand
        {
            get
            {
                return _noCommand;
            }
        }

        #region IsCancelVisible

        public bool IsCancelVisible
        {
            get
            {
                return (bool)GetValue(IsCancelVisibleProperty);
            }
            set
            {
                SetValue(IsCancelVisibleProperty, value);
            }
        }

        public static readonly DependencyProperty IsCancelVisibleProperty =
            DependencyProperty.Register("IsCancelVisible", typeof(bool), typeof(AskDialogArg));

        #endregion
    }
}