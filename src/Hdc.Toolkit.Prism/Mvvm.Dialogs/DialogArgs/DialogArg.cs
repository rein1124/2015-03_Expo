using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Mvvm.Dialogs
{
    public class DialogArg<TContext> : DependencyObject
        where TContext : class
    {
        private readonly Action _cancelAction;

        private readonly ICommand _cancelCommand;

        public static readonly DependencyProperty ContextProperty = DependencyProperty.Register("Context",
            typeof(TContext),
            typeof(DialogArg<TContext>));

        public DialogArg(Action cancelAction)
        {
            _cancelAction = cancelAction;
            _cancelCommand = new DelegateCommand(() =>
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

        public TContext Context
        {
            get
            {
                return (TContext)GetValue(ContextProperty);
            }
            set
            {
                SetValue(ContextProperty, value);
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand;
            }
        }
    }
}