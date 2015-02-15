using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using Hdc.Mvvm.Dialogs;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Prism.Regions.Behaviors
{
    public class BusyRequestNavigateBehavior : Behavior<ButtonBase>
    {
        private bool _isFirstTime = true;
        protected override void OnAttached()
        {
            base.OnAttached();

            var target = AssociatedObject as ButtonBase;
            if (target == null)
            {
                return;
            }

            target.Click += target_Click;
        }

        void target_Click(object sender, RoutedEventArgs e)
        {
            if (JustShowOnFirstTime && _isFirstTime)
            {
                var dialogService = ServiceLocator.Current.GetInstance<IBusyDialogService>();
                dialogService.Show(Message);
                _isFirstTime = false;
            }
            
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RequestNavigate(RegionName, ViewName);
        }

        #region RegionName

        public string RegionName
        {
            get { return (string)GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
            "RegionName", typeof(string), typeof(BusyRequestNavigateBehavior));

        #endregion

        #region ViewName

        public string ViewName
        {
            get { return (string)GetValue(ViewNameProperty); }
            set { SetValue(ViewNameProperty, value); }
        }

        public static readonly DependencyProperty ViewNameProperty = DependencyProperty.Register(
            "ViewName", typeof(string), typeof(BusyRequestNavigateBehavior));

        #endregion

        #region Message

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof (string), typeof (BusyRequestNavigateBehavior));

        #endregion

        #region JustShowOnFirstTime

        public bool JustShowOnFirstTime
        {
            get { return (bool) GetValue(JustShowOnFirstTimeProperty); }
            set { SetValue(JustShowOnFirstTimeProperty, value); }
        }

        public static readonly DependencyProperty JustShowOnFirstTimeProperty = DependencyProperty.Register(
            "JustShowOnFirstTime", typeof (bool), typeof (BusyRequestNavigateBehavior),new PropertyMetadata(true));

        #endregion
    }
}