using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hdc.Controls
{

    [TemplateVisualState(Name = "State1", GroupName = "DirectionState")]
    [TemplateVisualState(Name = "State2", GroupName = "DirectionState")]
    [TemplateVisualState(Name = "State3", GroupName = "DirectionState")]
    [TemplateVisualState(Name = "State4", GroupName = "DirectionState")]


    public class WebPressUnitSelectorLine : Control
    {
        static WebPressUnitSelectorLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WebPressUnitSelectorLine),
                                                     new FrameworkPropertyMetadata(typeof(WebPressUnitSelectorLine)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateShowStateStates(false);

        }

        protected virtual void UpdateShowStateStates(bool useTransitions)
        {
            if ((MasterIndex == UnitIndex) && (NextUnitMasterIndex == UnitIndex))
            {
                VisualStateManager.GoToState(this, "State1", useTransitions);
            }

            if ((MasterIndex < UnitIndex)&& (NextUnitMasterIndex == UnitIndex+1))
            {
                VisualStateManager.GoToState(this, "State2", useTransitions);
            }

            if ((MasterIndex < UnitIndex) && (NextUnitMasterIndex < UnitIndex))
            {
                VisualStateManager.GoToState(this, "State3", useTransitions);
            }

            if ((MasterIndex == UnitIndex) && (NextUnitMasterIndex == UnitIndex + 1))
            {
                VisualStateManager.GoToState(this, "State4", useTransitions);
            }
        }

        private static void PropertyChangedCallback(DependencyObject s,
                                                          DependencyPropertyChangedEventArgs e)
        {
            var me = s as WebPressUnitSelectorLine;
            if (me == null) return;

            me.UpdateShowStateStates(true);
        }

        #region
        public static readonly DependencyProperty UnitIndexProperty =
            DependencyProperty.Register("UnitIndex", typeof(int), typeof(WebPressUnitSelectorLine), 
            new PropertyMetadata(new PropertyChangedCallback(PropertyChangedCallback)));
        [Bindable(true), Category("Common Properties")]
        public int UnitIndex
        {
            get { return (int)GetValue(UnitIndexProperty); }
            set { SetValue(UnitIndexProperty, value); }
        }

        public static readonly DependencyProperty MasterIndexProperty =
            DependencyProperty.Register("MasterIndex", typeof(int), typeof(WebPressUnitSelectorLine), 
            new PropertyMetadata(new PropertyChangedCallback(PropertyChangedCallback)));
        [Bindable(true), Category("Common Properties")]
        public int MasterIndex
        {
            get { return (int)GetValue(MasterIndexProperty); }
            set { SetValue(MasterIndexProperty, value); }
        }

        public static readonly DependencyProperty NextUnitMasterIndexProperty =
            DependencyProperty.Register("NextUnitMasterIndex", typeof(int), typeof(WebPressUnitSelectorLine), 
            new PropertyMetadata(new PropertyChangedCallback(PropertyChangedCallback)));
        [Bindable(true), Category("Common Properties")]
        public int NextUnitMasterIndex
        {
            get { return (int)GetValue(NextUnitMasterIndexProperty); }
            set { SetValue(NextUnitMasterIndexProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(WebPressUnitSelectorLine),
            new PropertyMetadata(PropertyChangedCallback));
        [Bindable(true), Category("Common Properties")]
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        #endregion
    }
}
