using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Hdc.Controls
{

    public class MessageDialog : Control
    {
        public static RoutedCommand RoutedCancelCommand = new RoutedCommand();

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand",
            typeof(ICommand),
            typeof(MessageDialog));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
            typeof(string),
            typeof(MessageDialog));

        static MessageDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialog),
                new FrameworkPropertyMetadata(typeof(MessageDialog)));
        }

        public MessageDialog()
        {
            CommandBindings.AddRange(new[]
                {
                    new CommandBinding(RoutedCancelCommand,
                        (s, e) =>
                            {
                                if (CancelCommand == null)
                                {
                                    return;
                                }

                                CancelCommand.Execute(null);
                            },
                        (s, e) => { e.CanExecute = true; })
                });
        }

        public ICommand CancelCommand
        {
            get
            {
                return (ICommand)GetValue(CancelCommandProperty);
            }
            set
            {
                SetValue(CancelCommandProperty, value);
            }
        }

        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }
    }
}