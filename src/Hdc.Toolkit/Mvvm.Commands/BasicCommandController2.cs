using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Hdc.Mvvm.Commands
{
    public class BasicCommandController2
    {

        public BasicCommandController2 Add(object element, Action executeFn)
        {
            return Add(element, o => executeFn(), null);
        }

        public BasicCommandController2 Add(object element, Action<object> executeFn)
        {
            return Add(element, executeFn, null);
        }

        public BasicCommandController2 Add(object element, Action<object> executeFn, Func<object, bool> canExecuteFn)
        {
            AddExecuteFn(element, executeFn);
            AddCanExecuteFn(element, canExecuteFn);
            return this;
        }

        public BasicCommandController2 Add(object element, ICommand command)
        {
            return Add(element, command.Execute, command.CanExecute);
        }


        public void Execute(object element)
        {
            if (CanExecute(element)) _executeFns[element](null);
        }

        public void CanExecuteChanged()
        {
            foreach (var each in _canExecuteFns) SetControlIsEnabled(each.Key, each.Value);
        }


        #region AddExecuteFn

        private void AddExecuteFn(object element, Action<object> executeFn)
        {
            _executeFns.Add(element, executeFn);
            HookDefaultEvent(element, executeFn);
        }

        // ToDo: Hook default events of other kinds of UI elements
        private static void HookDefaultEvent(object element, Action<object> executeFn)
        {
            if (HookButton(element, executeFn)) return;
            if (HookSelector(element, executeFn)) return;
        }

        private static bool HookButton(object element, Action<object> executeFn)
        {
            var button = element as Button;
            if (null == button)
                return false;
            button.Click += delegate { executeFn(null); };
            return true;
        }

        private static bool HookSelector(object element, Action<object> executeFn)
        {
            var selector = element as Selector;
            if (null == selector)
                return false;
            selector.SelectionChanged += delegate { executeFn(null); };
            return true;
        }

        #endregion


        private void AddCanExecuteFn(object element, Func<object, bool> canExecuteFn)
        {
            if (null == canExecuteFn) return;
            _canExecuteFns.Add(element, canExecuteFn);
        }

        private bool CanExecute(object element)
        {

            if (null == element ||
                (_canExecuteFns.ContainsKey(element) &&
                 !_canExecuteFns[element](null)))
                return false;

            if (_executeFns.ContainsKey(element)) return true;

            throw new ArgumentException("Can't find an action to execute for element, " + element);
        }

        private static void SetControlIsEnabled(object element, Func<object, bool> canExecuteFn)
        {
            var control = element as Control;
            if (null == control) return;
            control.IsEnabled = canExecuteFn(null);
        }


        private readonly IDictionary<object, Action<object>> _executeFns
            = new Dictionary<object, Action<object>>();

        private readonly IDictionary<object, Func<object, bool>> _canExecuteFns
            = new Dictionary<object, Func<object, bool>>();
    }
}