using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Hdc.Controls
{
    [Obsolete]
    public class DisplaySinlgeItemSelector : Selector
    {
        static DisplaySinlgeItemSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (DisplaySinlgeItemSelector),
                                                     new FrameworkPropertyMetadata(typeof (DisplaySinlgeItemSelector)));
        }
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            Update();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

//            Update();
        }

        void Update()
        {
            foreach (var child in Items)
            {
                var childDo = child as UIElement;
                childDo.Visibility = Visibility.Hidden;
            }

            if (SelectedItem != null)
            {
                var childDo = SelectedItem as UIElement;
                childDo.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}