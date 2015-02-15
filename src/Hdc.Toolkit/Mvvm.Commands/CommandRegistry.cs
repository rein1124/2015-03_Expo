using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Hdc.Mvvm.Commands
{
    public class CommandRegistry : ICommandRegistry
    {

        public ICommand Add(Action executeFn)
        {
            return Add(p => executeFn(), null);
        }

        public ICommand Add(Action executeFn, Func<object, bool> canExecuteFn)
        {
            return Add(p => executeFn(), canExecuteFn);
        }

        public ICommand Add(Action<object> executeFn)
        {
            return Add(executeFn, null);
        }

        public virtual ICommand Add(Action<object> executeFn, Func<object, bool> canExecuteFn)
        {
            var cmd = new BasicCommand(executeFn, canExecuteFn);

            if (null != canExecuteFn)
            {
                AddRaiseCanExecuteChangedToList(cmd.RaiseCanExecuteChanged);
            }

            return cmd;
        }

        public void RaiseCanExecuteChanged()
        {
            foreach (var each in _raiseCanExecuteChangedList)
                each();
        }

        protected void AddRaiseCanExecuteChangedToList(Action raiser)
        {
            _raiseCanExecuteChangedList.Add(raiser);
        }

        private readonly IList<Action> _raiseCanExecuteChangedList = new List<Action>();

    }
}