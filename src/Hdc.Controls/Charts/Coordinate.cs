using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hdc.Controls.Charts
{
    public class Coordinate : Control
    {
        static Coordinate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Coordinate), new FrameworkPropertyMetadata(typeof(Coordinate)));
        }

        public static readonly DependencyProperty ShowLeftProperty = DependencyProperty.Register("ShowLeft", typeof(bool), typeof(Coordinate), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty ShowRightProperty = DependencyProperty.Register("ShowRight", typeof(bool), typeof(Coordinate), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty AxisBrushProperty = DependencyProperty.Register("AxisBrush", typeof(Brush), typeof(Coordinate), new FrameworkPropertyMetadata(Brushes.Black));

        public bool ShowLeft
        {
            get { return (bool)GetValue(ShowLeftProperty); }
            set { SetValue(ShowLeftProperty, value); }
        }

        public bool ShowRight
        {
            get { return (bool)GetValue(ShowRightProperty); }
            set { SetValue(ShowRightProperty, value); }
        }

        public Brush AxisBrush
        {
            get { return GetValue(AxisBrushProperty) as Brush; }
            set { SetValue(AxisBrushProperty, value); }
        }
    }
}
