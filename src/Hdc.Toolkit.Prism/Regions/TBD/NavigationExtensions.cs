// TBD
// ref: http://compositewpf.codeplex.com/discussions/248576

using System;
using Microsoft.Practices.Prism.Regions;

namespace Hdc.Prism.Regions
{
    /// <summary>
    /// Extends functionality of the region navigation in PRISM.
    /// </summary>
    public static class NavigationExtensions
    {
        #region RegionManager Extensions

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationCallback">The navigation callback.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (regionManager == null)
            {
                navigationCallback(new NavigationResult(new NavigationContext(null, target), false));
                return;
            }

            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                regionManager.Regions[regionName].RequestNavigate(target, navigationCallback, navigationParameters);
            }
            else
            {
                navigationCallback(new NavigationResult(new NavigationContext(null, target), false));
            }
        }

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string target, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, new Uri(target, UriKind.RelativeOrAbsolute), (nr) => { }, navigationParameters);
        }

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationCallback">The navigation callback.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, new Uri(target, UriKind.RelativeOrAbsolute), navigationCallback, navigationParameters);
        }

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri target, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, target, (nr) => { }, navigationParameters);
        }

        #endregion

        #region Region Extensions

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationCallback">The navigation callback.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegion region, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (region == null)
            {
                return;
            }

            region.Context = navigationParameters;

            region.RequestNavigate(target, navigationCallback);
        }

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegion region, string target, NavigationParameters navigationParameters)
        {
            RequestNavigate(region, new Uri(target, UriKind.RelativeOrAbsolute), (nr) => { }, navigationParameters);
        }

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationCallback">The navigation callback.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegion region, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            RequestNavigate(region, new Uri(target, UriKind.RelativeOrAbsolute), navigationCallback, navigationParameters);
        }

        /// <summary>
        /// Requests the navigate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="target">The target.</param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public static void RequestNavigate(this IRegion region, Uri target, NavigationParameters navigationParameters)
        {
            RequestNavigate(region, target, (nr) => { }, navigationParameters);
        }

        #endregion

        #region NavigationContext Extensions

        /// <summary>
        /// Gets the navigation parameters.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The NavigationParameters for the NavigationContext.</returns>
        public static NavigationParameters GetNavigationParameters(this NavigationContext context)
        {
            if (context == null)
            {
                return new NavigationParameters();
            }

            if (context.NavigationService == null || context.NavigationService.Region == null)
            {
                return new NavigationParameters();
            }

            if (context.NavigationService.Region.Context is NavigationParameters)
            {
                return context.NavigationService.Region.Context as NavigationParameters;
            }

            return new NavigationParameters();
        }

        #endregion
    }
}