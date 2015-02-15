using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Hdc.Windows.Interactivity
{
    public class MouseWheelDeltaBehavior : Behavior<FrameworkElement>
    {
        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MouseWheelDeltaBehavior));

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
            }
        }

        void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var command = this.Command;

            if (command != null)
            {
                command.Execute(e.Delta);
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}