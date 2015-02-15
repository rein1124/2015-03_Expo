using Hdc.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Hdc.Prism.Regions
{
    public static class RegionManagerExtensions
    {
        public static IRegionManager RegisterViewWithRegion<TView>(this IRegionManager regionManager, string regionName)
        {
            regionManager.RegisterViewWithRegion(regionName, typeof (TView));
            return regionManager;
        }

        public static IRegionManager RegisterViewWithRegions<TView>(this IRegionManager regionManager,
                                                                    params string[] regionNames)
        {
            foreach (var regionName in regionNames)
            {
                regionManager.RegisterViewWithRegion(regionName, typeof (TView));
            }
            return regionManager;
        }

        public static IRegionManager RegisterViewWithRegion<TView>(
            this IRegionManager regionManager,
            string regionName,
            IUnityContainer container)
        {
            container.RegisterTypeWithLifetimeManager<TView>();

            regionManager.RegisterViewWithRegion(regionName, () => container.Resolve<TView>());
            return regionManager;
        }

        public static IRegionManager RegisterViewWithRegion<TView>(
            this IRegionManager regionManager,
            string regionName,
            IUnityContainer container,
            string key)
        {
            container.RegisterTypeWithLifetimeManager<TView>(key);

            regionManager.RegisterViewWithRegion(regionName, () => container.Resolve<TView>(key));
            return regionManager;
        }

        public static IRegionManager RegisterViewWithRegions<TView>(
            this IRegionManager regionManager,
            IUnityContainer container,
            params string[] regionNames)
        {
            foreach (var regionName in regionNames)
            {
                regionManager.RegisterViewWithRegion<TView>(regionName, container);
            }
            return regionManager;
        }
    }
}