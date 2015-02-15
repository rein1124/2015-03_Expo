using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Prism.Regions.Behaviors
{
    public abstract class ButtonBaseNavigateBehavior : Behavior<ButtonBase>
    {
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

        private void target_Click(object sender, RoutedEventArgs e)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            IRegion region = regionManager.Regions[RegionName];
            region.NavigationService.NavigationFailed += NavigationService_NavigationFailed;
            OnNavigating(region);
            region.NavigationService.NavigationFailed -= NavigationService_NavigationFailed;
        }

        private void NavigationService_NavigationFailed(object sender, RegionNavigationFailedEventArgs e)
        {
            throw e.Error;
        }

        #region RegionName

        public string RegionName
        {
            get { return (string) GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
            "RegionName", typeof (string), typeof (RequestNavigateBehavior));

        private IRegionManager _regionManager;

        #endregion

        protected abstract void OnNavigating(IRegion navigationRegion);

        public IRegionManager RegionManager
        {
            get { return _regionManager ?? (_regionManager = ServiceLocator.Current.GetInstance<IRegionManager>()); }
        }
    }
}