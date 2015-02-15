using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Mvvm.Dialogs
{
    public class AskPocoPocoDialogArg : MessagePocoDialogArg
    {
        private readonly Action _yesAction;

        private readonly Action _noAction;

        private readonly ICommand _yesCommand;

        private readonly ICommand _noCommand;

        public AskPocoPocoDialogArg(Action cancelAction, Action yesAction, Action noAction)
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

        private bool _isCancelVisible;

        public bool IsCancelVisible
        {
            get { return _isCancelVisible; }
            set
            {
                if (Equals(_isCancelVisible, value)) return;
                _isCancelVisible = value;
                RaisePropertyChanged(() => IsCancelVisible);
            }
        } 
    }
}