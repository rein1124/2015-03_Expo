using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Prism.Regions.Behaviors
{
    public class GoBackNavigateBehavior : ButtonBaseNavigateBehavior
    {
        protected override void OnNavigating(IRegion navigationRegion)
        {
            navigationRegion.NavigationService.Journal.GoBack();
        }
    }
}