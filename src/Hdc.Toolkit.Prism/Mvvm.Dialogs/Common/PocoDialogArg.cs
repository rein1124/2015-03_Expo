using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;


namespace Hdc.Mvvm.Dialogs
{
    public class PocoDialogArg<TContext> : ViewModel
        where TContext : class
    {
        private readonly Action _cancelAction;

        private readonly ICommand _cancelCommand;

        public PocoDialogArg(Action cancelAction)
        {
            _cancelAction = cancelAction;

            _cancelCommand = new DelegateCommand(
                () =>
                    {
                        if (_cancelAction != null)
                        {
                            _cancelAction();
                        }
                        Close();
                    });
        }

        public void Show(TContext context)
        {
            Context = context;
        }

        public void Close()
        {
            Context = null;
        }

        private TContext _context;

        public TContext Context
        {
            get { return _context; }
            set
            {
                if (Equals(_context, value)) return;
                _context = value;
                RaisePropertyChanged(() => Context);
            }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }
    }
}