using System;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System.Reactive.Linq;

namespace Hdc.Mvvm.Dialogs
{
    [Obsolete("TBD")]
    public class ShowMessageBehavior : Behavior<UIElement>
    {
        IMessageDialogService _messageDialogService = ServiceLocator.Current.GetInstance<IMessageDialogService>();

        protected override void OnAttached()
        {
            base.OnAttached();

            var target = AssociatedObject as UIElement;
            if (target == null)
            {
                return;
            }

            target.PreviewMouseUp += target_Click;
        }

        private void target_Click(object sender, RoutedEventArgs e)
        {
            _messageDialogService.Show(Message);

            return;

            var ds = new DispatcherScheduler(Application.Current.Dispatcher, DispatcherPriority.Render);
            Observable.Start(() =>
                                 {
                                     _messageDialogService.Show(Message);
                                 },
                                 ds); //DispatcherScheduler.Current

            //            e.Handled = false;
        }

        #region Message

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(ShowMessageBehavior));

        #endregion
    }
}