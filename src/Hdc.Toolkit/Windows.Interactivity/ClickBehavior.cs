using System.Windows;
using System.Windows.Input;

namespace Hdc.Mvvm.AttachedBehaviors
{
    public class ClickBehavior
    {
        public static DependencyProperty MouseUpCommandProperty =
                DependencyProperty.RegisterAttached(
                        "MouseUpCommand",
                        typeof(ICommand),
                        typeof(ClickBehavior),
                        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(MouseUpCommandChanged)));

        public static void SetMouseUpCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(MouseUpCommandProperty, value);
        }

        private static void MouseUpCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = target as UIElement;

            if (element != null)
            {
                // If we're putting in a new command and there wasn't one already

                // hook the event

                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    element.PreviewMouseUp += MouseUpHandler;
                    element.MouseUp += MouseUpHandler;
                }

                        // If we're clearing the command and it wasn't already null

                        // unhook the event

                else if ((e.NewValue == null) && (e.OldValue != null))
                    element.PreviewMouseUp -= MouseUpHandler;
            }
        }

        private static void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;

            ICommand command = (ICommand)element.GetValue(ClickBehavior.MouseUpCommandProperty);

            command.Execute(null);
        }
    }
}