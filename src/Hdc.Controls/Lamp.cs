using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    public class Lamp : Control
    {
        static Lamp()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (Lamp),
                                                     new FrameworkPropertyMetadata(typeof (Lamp)));
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof (bool), typeof (Lamp));

        public static readonly DependencyProperty IsFlickeringProperty = DependencyProperty.Register(
            "IsFlickering", typeof (bool), typeof (Lamp));


        public bool IsOpen
        {
            get { return (bool) GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public bool IsFlickering
        {
            get { return (bool) GetValue(IsFlickeringProperty); }
            set { SetValue(IsFlickeringProperty, value); }
        }
    }
}