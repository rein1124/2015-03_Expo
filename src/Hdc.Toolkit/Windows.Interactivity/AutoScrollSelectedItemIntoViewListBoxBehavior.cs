using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Hdc.Windows.Interactivity
{
    public class AutoScrollSelectedItemIntoViewListBoxBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }
    }
}