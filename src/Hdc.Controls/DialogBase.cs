namespace Hdc.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
     

    public abstract class DialogBase : Control
    {
        public static RoutedCommand RoutedConfirmCommand = new RoutedCommand();

        public static RoutedCommand RoutedCancelCommand = new RoutedCommand();

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
                "CancelCommand", typeof(ICommand), typeof(DialogBase));

        public static readonly DependencyProperty ConfirmCommandProperty = DependencyProperty.Register(
                "ConfirmCommand", typeof(ICommand), typeof(DialogBase));

        protected DialogBase()
        {
            CommandBindings.AddRange(
                    new[]
                        {
                                new CommandBinding(
                                        RoutedCancelCommand, RoutedCancelCommandExecuted, RoutedCancelCommandCanExecute)
                                ,
                                new CommandBinding(
                                        RoutedConfirmCommand,
                                        RoutedConfirmCommandExecuted,
                                        RoutedConfirmCommandCanExecute),
                        });
        }

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public ICommand ConfirmCommand
        {
            get { return (ICommand)GetValue(ConfirmCommandProperty); }
            set { SetValue(ConfirmCommandProperty, value); }
        }

        protected void RoutedConfirmCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (ConfirmCommand == null)
                return;

            OnRoutedConfirmCommandExecuted(sender, e);
        }

        protected virtual void OnRoutedConfirmCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        protected virtual void RoutedConfirmCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        protected void RoutedCancelCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (CancelCommand == null)
                return;

            OnRoutedCancelCommandExecuted(sender, e);
        }

        protected virtual void OnRoutedCancelCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        protected virtual void RoutedCancelCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}