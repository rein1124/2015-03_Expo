using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hdc.Controls
{
    public class SelectionToggleButton : ToggleButton
    {
        static SelectionToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SelectionToggleButton),
                                                     new FrameworkPropertyMetadata(typeof (SelectionToggleButton)));
        }

        #region IndexName

        public string IndexName
        {
            get { return (string) GetValue(IndexNameProperty); }
            set { SetValue(IndexNameProperty, value); }
        }

        public static readonly DependencyProperty IndexNameProperty = DependencyProperty.Register(
            "IndexName", typeof (string), typeof (SelectionToggleButton));

        #endregion

        #region IdentifyBrush

        public Brush IdentifyBrush
        {
            get { return (Brush) GetValue(IdentifyBrushProperty); }
            set { SetValue(IdentifyBrushProperty, value); }
        }

        public static readonly DependencyProperty IdentifyBrushProperty = DependencyProperty.Register(
            "IdentifyBrush", typeof (Brush), typeof (SelectionToggleButton));

        #endregion
    }
}