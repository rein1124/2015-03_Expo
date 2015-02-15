using System.Windows;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class AskDialog : MessageDialog
    {
        public static RoutedCommand RoutedYesCommand = new RoutedCommand();

        public static RoutedCommand RoutedNoCommand = new RoutedCommand();

        public static readonly DependencyProperty YesCommandProperty = DependencyProperty.Register("YesCommand",
                                                                                                   typeof(ICommand),
                                                                                                   typeof(AskDialog));

        public static readonly DependencyProperty NoCommandProperty = DependencyProperty.Register("NoCommand",
                                                                                                  typeof(ICommand),
                                                                                                  typeof(AskDialog));

        public static readonly DependencyProperty IsCancelVisibleProperty =
            DependencyProperty.Register("IsCancelVisible", typeof(bool), typeof(AskDialog));

        static AskDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AskDialog), new FrameworkPropertyMetadata(typeof(AskDialog)));
        }

        public AskDialog()
        {
            CommandBindings.AddRange(new[]
                                         {
                                             new CommandBinding(RoutedYesCommand,
                                                                (s, e) =>
                                                                    {
                                                                        if (YesCommand == null)
                                                                        {
                                                                            return;
                                                                        }

                                                                        YesCommand.Execute(null);
                                                                    },
                                                                (s, e) => { e.CanExecute = true; }),
                                             new CommandBinding(RoutedNoCommand,
                                                                (s, e) =>
                                                                    {
                                                                        if (NoCommand == null)
                                                                        {
                                                                            return;
                                                                        }

                                                                        NoCommand.Execute(null);
                                                                    },
                                                                (s, e) => { e.CanExecute = true; }),
                                         });
        }

        public ICommand YesCommand
        {
            get
            {
                return (ICommand)GetValue(YesCommandProperty);
            }
            set
            {
                SetValue(YesCommandProperty, value);
            }
        }

        public ICommand NoCommand
        {
            get
            {
                return (ICommand)GetValue(NoCommandProperty);
            }
            set
            {
                SetValue(NoCommandProperty, value);
            }
        }

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
    }
}