using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Prism.Regions.Behaviors
{
    public class AddDialogRegionBehavior : Behavior<UIElement>
    {

        #region ContainerWindowStyle

        public Style ContainerWindowStyle
        {
            get { return (Style)GetValue(ContainerWindowStyleProperty); }
            set { SetValue(ContainerWindowStyleProperty, value); }
        }

        public static readonly DependencyProperty ContainerWindowStyleProperty = DependencyProperty.Register(
            "ContainerWindowStyle", typeof(Style), typeof(AddDialogRegionBehavior));

        #endregion

        #region RegionName

        public string RegionName
        {
            get { return (string)GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
            "RegionName", typeof(string), typeof(AddDialogRegionBehavior));

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            var assObj = AssociatedObject;

            assObj.SetValue(RegionPopupBehaviors.ContainerWindowStyleProperty, ContainerWindowStyle);

            if (IsInDesignMode(assObj))
            {
                return;
            }

            RegisterNewPopupRegion(assObj, RegionName);
        }



        /// <summary>
        /// Creates a new <see cref="IRegion"/> and registers it in the default <see cref="IRegionManager"/>
        /// attaching to it a <see cref="DialogActivationBehavior"/> behavior.
        /// </summary>
        /// <param name="owner">The owner of the Popup.</param>
        /// <param name="regionName">The name of the <see cref="IRegion"/>.</param>
        /// <remarks>
        /// This method would typically not be called directly, instead the behavior 
        /// should be set through the Attached Property <see cref="CreatePopupRegionWithNameProperty"/>.
        /// </remarks>
        public static void RegisterNewPopupRegion(DependencyObject owner, string regionName)
        {
            // Creates a new region and registers it in the default region manager.
            // Another option if you need the complete infrastructure with the default region behaviors
            // is to extend DelayedRegionCreationBehavior overriding the CreateRegion method and create an 
            // instance of it that will be in charge of registering the Region once a RegionManager is
            // set as an attached property in the Visual Tree.
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager != null)
            {
                IRegion region = new SingleActiveRegion();
                DialogActivationBehavior behavior;
#if SILVERLIGHT
                behavior = new PopupDialogActivationBehavior();
#else
                behavior = new WindowDialogActivationBehavior();
#endif
                behavior.HostControl = owner;

                region.Behaviors.Add(DialogActivationBehavior.BehaviorKey, behavior);
                regionManager.Regions.Add(regionName, region);
            }
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            // Due to a known issue in Cider, GetIsInDesignMode attached property value is not enough to know if it's in design mode.
            return DesignerProperties.GetIsInDesignMode(element) || Application.Current == null
                   || Application.Current.GetType() == typeof(Application);
        }
    }
}