//ref: Prism 4.0
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace Hdc.Controls
{
    /// <summary>
    /// Behavior that ensures a popup is located at the bottom-right corner of its parent.
    /// </summary>
    public class RelocatePopupBehavior : Behavior<Popup>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.Opened += this.OnPopupOpened;
            this.AssociatedObject.Closed += this.OnPopupClosed;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Opened -= this.OnPopupOpened;
            this.AssociatedObject.Closed -= this.OnPopupClosed;

            this.DetachSizeChangeHandlers();

            base.OnDetaching();
        }

        private void OnPopupOpened(object sender, EventArgs e)
        {
            this.UpdatePopupOffsets();
            this.AttachSizeChangeHandlers();
        }

        private void OnPopupClosed(object sender, EventArgs e)
        {
            this.DetachSizeChangeHandlers();
        }

        private void AttachSizeChangeHandlers()
        {
            var child = this.AssociatedObject.Child as FrameworkElement;
            if (child != null)
            {
                child.SizeChanged += this.OnChildSizeChanged;
            }

            var parent = this.AssociatedObject.Parent as FrameworkElement;
            if (parent != null)
            {
                parent.SizeChanged += this.OnParentSizeChanged;
            }
        }

        private void DetachSizeChangeHandlers()
        {
            var child = this.AssociatedObject.Child as FrameworkElement;
            if (child != null)
            {
                child.SizeChanged -= this.OnChildSizeChanged;
            }

            var parent = this.AssociatedObject.Parent as FrameworkElement;
            if (parent != null)
            {
                parent.SizeChanged -= this.OnParentSizeChanged;
            }
        }

        private void OnChildSizeChanged(object sender, EventArgs e)
        {
            this.UpdatePopupOffsets();
        }

        private void OnParentSizeChanged(object sender, EventArgs e)
        {
            this.UpdatePopupOffsets();
        }

        private void UpdatePopupOffsets()
        {
            if (this.AssociatedObject != null)
            {
                var child = this.AssociatedObject.Child as FrameworkElement;
                var parent = this.AssociatedObject.Parent as FrameworkElement;

                if (child != null && parent != null)
                {
                    var anchor = new Point(parent.ActualWidth, parent.ActualHeight);

                    this.AssociatedObject.HorizontalOffset = anchor.X - child.ActualWidth;
                    this.AssociatedObject.VerticalOffset = anchor.Y - child.ActualHeight;
                }
            }
        }
    }
}