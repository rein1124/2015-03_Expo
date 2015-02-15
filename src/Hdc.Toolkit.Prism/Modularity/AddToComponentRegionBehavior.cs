using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using Hdc.Controls;

namespace Hdc.Modularity
{
    public class AddToComponentRegionBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var e = ComponentManager.Instance.GetComponentRegion(RegionName);

            BindingOperations.SetBinding(
                AssociatedObject,
                UIElement.IsEnabledProperty,
                new Binding(PropertySupport.ExtractPropertyName<IComponentRegion, bool>(x => x.IsEnabled))
                    {
                        Source = e
                    });

            BindingOperations.SetBinding(
                AssociatedObject,
                UIElement.VisibilityProperty,
                new Binding(PropertySupport.ExtractPropertyName<IComponentRegion, bool>(x => x.IsActive))
                    {
                        Source = e,
                        Converter = new BooleanToVisibilityConverter(),
                    });


/*            var mb = new MultiBinding();

            mb.Bindings.Add(new Binding(PropertySupport.ExtractPropertyName<IComponentRegion, bool>(x => x.IsVisible))
                                {
                                    Source = e,
                                });

            mb.Bindings.Add(new Binding(PropertySupport.ExtractPropertyName<IComponentRegion, bool>(x => x.IsCollapsed))
                                {
                                    Source = e,
                                });

            mb.Converter = new BooleansToVisibilityConverter();

            BindingOperations.SetBinding(
                AssociatedObject,
                UIElement.VisibilityProperty,
                mb);*/
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();

            throw new NotImplementedException();
        }

        #region RegionName

        public string RegionName
        {
            get { return (string) GetValue(ComponentNameProperty); }
            set { SetValue(ComponentNameProperty, value); }
        }

        public static readonly DependencyProperty ComponentNameProperty = DependencyProperty.Register(
            "RegionName", typeof (string), typeof (AddToComponentRegionBehavior));

        #endregion

        #region IsCollapsed

        public bool IsCollapsed
        {
            get { return (bool) GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register(
            "IsCollapsed", typeof (bool), typeof (AddToComponentRegionBehavior));

        #endregion
    }
}