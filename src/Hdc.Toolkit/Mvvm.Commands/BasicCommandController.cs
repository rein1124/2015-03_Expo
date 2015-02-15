using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Hdc.Mvvm.Commands
{
    public class BasicCommandController
    {

        public BasicCommandController Add(object element, Action executeFn)
        {
            AddExecuteFn(element, executeFn);
            return this;
        }

        #region AddExecuteFn

        private static void AddExecuteFn(object element, Action executeFn)
        {
            HookDefaultEvent(element, executeFn);
        }

        // ToDo: Hook default events of other kinds of UI elements
        private static void HookDefaultEvent(object element, Action executeFn)
        {
            if (HookButton(element, executeFn))
                return;
            if (HookSelector(element, executeFn))
                return;
        }

        private static bool HookButton(object element, Action executeFn)
        {
            var button = element as Button;
            if (null == button)
                return false;
            button.Click += delegate { executeFn(); };
            return true;
        }

        private static bool HookSelector(object element, Action executeFn)
        {
            var selector = element as Selector;
            if (null == selector)
                return false;
            selector.SelectionChanged += delegate { executeFn(); };
            return true;
        }

        #endregion

    }
}