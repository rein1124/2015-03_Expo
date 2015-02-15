using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    public class DisplaySinlgeItemPanel : Grid
    {
        #region SelectedIndex

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", typeof(int), typeof(DisplaySinlgeItemPanel), new PropertyMetadata(-1, OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as DisplaySinlgeItemPanel;
            var index = (int)e.NewValue;

            foreach (var child in me.Children)
            {
                var childDo = child as UIElement;
                childDo.Visibility = Visibility.Hidden;
            }

            if (index < me.Children.Count && index > -1)
            {
                var childDo = me.Children[index] as UIElement;
                childDo.Visibility = System.Windows.Visibility.Visible;
            }

        }

        #endregion


    }
}