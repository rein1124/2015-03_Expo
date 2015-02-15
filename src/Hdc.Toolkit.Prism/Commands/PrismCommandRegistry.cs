using System;
using System.Windows.Input;
using Hdc.Mvvm.Commands;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Prism.Commands
{
    public class PrismCommandRegistry : CommandRegistry
    {

        public override ICommand Add(Action<object> executeFn, Func<object, bool> canExecuteFn)
        {

            var cmd = new Microsoft.Practices.Prism.Commands.DelegateCommand<object>(executeFn, canExecuteFn);

            if (null != canExecuteFn)
            {
                AddRaiseCanExecuteChangedToList(cmd.RaiseCanExecuteChanged);
            }

            return cmd;
        }

    }
}   