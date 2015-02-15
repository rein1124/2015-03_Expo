using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Mvvm.Dialogs
{
    public class CloseMessageDialogBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var target = AssociatedObject as FrameworkElement;
            if (target == null)
            {
                return;
            }

            target.Loaded += target_Click;
        }

        private void target_Click(object sender, RoutedEventArgs e)
        {
            var dialogService = ServiceLocator.Current.GetInstance<IMessageDialogService>();
            dialogService.Close();
        }
    }
}