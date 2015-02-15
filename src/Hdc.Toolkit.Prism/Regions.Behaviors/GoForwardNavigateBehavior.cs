using Microsoft.Practices.Prism.Regions;

namespace Hdc.Prism.Regions.Behaviors
{
    public class GoForwardNavigateBehavior : ButtonBaseNavigateBehavior
    {
        protected override void OnNavigating(IRegion navigationRegion)
        {
            navigationRegion.NavigationService.Journal.GoForward();
        }
    }
}