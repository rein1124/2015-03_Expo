using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Hdc.Windows.Interactivity
{
    public class DragOverDistanceDifferBehavior : Behavior<FrameworkElement>
    {
        private Point _lastPoint;

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(DragOverDistanceDifferBehavior));

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.DragOver -= AssociatedObject_DragOver;
            AssociatedObject.PreviewMouseDown -= AssociatedObject_PreviewMouseDown;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                base.AssociatedObject.DragOver += AssociatedObject_DragOver;
                //                base.AssociatedObject.Drop += AssociatedObject_Drop;
                AssociatedObject.PreviewMouseDown += AssociatedObject_PreviewMouseDown;
            }
        }

        private void AssociatedObject_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _lastPoint = e.GetPosition(AssociatedObject);
            DragDrop.DoDragDrop(AssociatedObject, new object(), DragDropEffects.Move);
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            Point mousePos = e.GetPosition(AssociatedObject);
            Vector diff = mousePos - _lastPoint;
            //            Debug.WriteLine("xDiff={0}, yDiff={1}",diff.X,diff.Y);
            _lastPoint = mousePos;

            var command = this.Command;

            //            if (command != null && command.CanExecute(param))
            //            {
            //                command.Execute(param);
            //            }

            if (command != null)
            {
                command.Execute(diff);
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}