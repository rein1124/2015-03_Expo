using Hdc.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class RegionDialogService : ViewModel
    {

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IServiceLocator CompositionContainer { get; set; }

        protected RegionDialogService()
        {
            DialogRegionName = GetDialogRegionName();
            DialogViewName = GetDialogViewName();
        }

        public virtual void CloseDialog()
        {
            // Check to see if the region exists.
            if (RegionManager.Regions.ContainsRegionWithName(DialogRegionName))
            {
                IRegion region = RegionManager.Regions[DialogRegionName];

                object currentView = region.GetView(DialogViewName);

                // If the view exists in the region remove it.
                if (currentView != null)
                {
                    region.Deactivate(currentView);
                    //                    region.Remove(currentView);
                }
            }
        }

        public virtual void ShowDialog()
        {
            // Check to see if the region exists.
            if (RegionManager.Regions.ContainsRegionWithName(DialogRegionName))
            {
                IRegion region = RegionManager.Regions[DialogRegionName];


                object currentView = region.GetView(DialogViewName);


                if (currentView != null)
                {
                    region.Deactivate(currentView);

                    if (IsModaless)
                    {
                        // If the view exists in the region remove it.
                        region.Remove(currentView);
                    }
                }

                if (currentView == null || IsModaless)
                {
                    var newView = CompositionContainer.GetInstance<object>(DialogViewName);
                    dynamic view = newView;
                    view.DataContext = this;
                    region.Add(newView, DialogViewName);
                    currentView = newView;
                }

                region.Activate(currentView);
            }
        }

        protected abstract string GetDialogRegionName();

        protected abstract string GetDialogViewName();

        //        protected abstract string DialogName { get; }
        public string DialogRegionName { get; private set; }

        public string DialogViewName { get; private set; }

        public bool IsModaless { get; set; }
    }
}