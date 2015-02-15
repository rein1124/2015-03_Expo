using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
//    [TemplateVisualState(Name = "DDLL", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DDLU", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DDUL", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DDULLU", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DDUU", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DDUULL", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DSLU", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "DSUU", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "SDUL", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "SDUU", GroupName = "SwapModes")]
//    [TemplateVisualState(Name = "SSUU", GroupName = "SwapModes")]
    public class SwapModeIndicator : Control
    {
        static SwapModeIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwapModeIndicator),
                                                     new FrameworkPropertyMetadata(typeof(SwapModeIndicator)));
        }

        #region SwapMode

        public SwapMode SwapMode
        {
            get { return (SwapMode)GetValue(SwapModeProperty); }
            set { SetValue(SwapModeProperty, value); }
        }

        public static readonly DependencyProperty SwapModeProperty = DependencyProperty.Register(
            "SwapMode", typeof(SwapMode), typeof(SwapModeIndicator));

        #endregion

    }
}