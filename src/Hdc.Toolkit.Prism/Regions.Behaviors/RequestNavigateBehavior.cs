using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Prism.Regions.Behaviors
{
    public class RequestNavigateBehavior : ButtonBaseNavigateBehavior
    {
        protected override void OnNavigating(IRegion navigationRegion)
        {
            RegionManager.RequestNavigate(RegionName, ViewName);
        }

        #region ViewName

        public string ViewName
        {
            get { return (string) GetValue(ViewNameProperty); }
            set { SetValue(ViewNameProperty, value); }
        }

        public static readonly DependencyProperty ViewNameProperty = DependencyProperty.Register(
            "ViewName", typeof (string), typeof (RequestNavigateBehavior));

        #endregion
    }
}