using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Hdc.Windows.Interactivity
{
    public class SelectionChangedBehavior
    {
        public static DependencyProperty SelectionChangedCommandProperty =
                DependencyProperty.RegisterAttached(
                        "SelectionChangedCommand",
                        typeof(ICommand),
                        typeof(SelectionChangedBehavior),
                        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SelectionChangedCommandHandler)));

        public static void SetSelectionChangedCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(SelectionChangedCommandProperty, value);
        }

        private static void SelectionChangedCommandHandler(
                DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as Selector;

            if (element == null)
                return;

            if ((e.NewValue != null) && (e.OldValue == null))
                element.SelectionChanged += SelectionChangedHandler;

            else if ((e.NewValue == null) && (e.OldValue != null))
                element.SelectionChanged -= SelectionChangedHandler;
        }

        private static void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            var element = (Selector)sender;

            var command = (ICommand)element.GetValue(SelectionChangedCommandProperty);

            command.Execute(element.SelectedItem);
        }
    }
}