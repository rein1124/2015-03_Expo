//see WPF Behavior Library 0.2+:
//wpfbehaviorlibrary-07774e9acae5.zip
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hdc.Windows.Interactivity
{
    /// <summary>
    /// Handles the visual indication of the drop point
    /// </summary>
    public class DropAdorner
        : Adorner, IDisposable
    {
        internal bool IsTopHalf { get; set; }

        private AdornerLayer myAdornerLayer;
        private Pen myPen;
        private bool myDrawHorizontal;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropAdorner"/> class.
        /// </summary>
        /// <param name="isTopHalf">if set to <c>true</c> we are adorning the top half of the item.</param>
        /// <param name="drawHorizontal">if set to <c>true</c> the item being adorned has a horizontal orientation.</param>
        /// <param name="adornedElement">The adorned element.</param>
        /// <param name="adornerLayer">The adorner layer.</param>
        public DropAdorner(bool isTopHalf, bool drawHorizontal, UIElement adornedElement, AdornerLayer adornerLayer)
            : base(adornedElement)
        {
            if (adornedElement == null) throw new ArgumentNullException("adornedElement");
            if (adornerLayer == null) throw new ArgumentNullException("adornerLayer");

            this.IsTopHalf = isTopHalf;
            myAdornerLayer = adornerLayer;
            myDrawHorizontal = drawHorizontal;

            this.IsHitTestVisible = false;

            myAdornerLayer.Add(this);

            DoubleAnimation animation = new DoubleAnimation(0.5, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

            myPen = new Pen(new SolidColorBrush(Colors.Red), 3.0);
            myPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
        }

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (drawingContext == null) throw new ArgumentNullException("drawingContext");

            Point[] points;
            if (myDrawHorizontal)
                points = DetermineHorizontalLinePoints();
            else
                points = DetermineVerticalLinePoints();

            drawingContext.DrawLine(myPen, points.First(), points.Last());
        }

        private Point[] DetermineHorizontalLinePoints()
        {
            double width = this.AdornedElement.RenderSize.Width;
            double height = this.AdornedElement.RenderSize.Height;

            Point startPoint;
            Point endPoint;
            if (this.IsTopHalf)
            {
                startPoint = new Point(0, 0);
                endPoint = new Point(0, height);
            }
            else
            {
                startPoint = new Point(width, 0);
                endPoint = new Point(width, height);
            }

            return new Point[] { startPoint, endPoint };
        }

        private Point[] DetermineVerticalLinePoints()
        {
            double width = this.AdornedElement.RenderSize.Width;
            double height = this.AdornedElement.RenderSize.Height;

            Point startPoint;
            Point endPoint;
            if (this.IsTopHalf)
            {
                startPoint = new Point(0, 0);
                endPoint = new Point(width, 0);
            }
            else
            {
                startPoint = new Point(0, height);
                endPoint = new Point(width, height);
            }

            return new Point[] { startPoint, endPoint };
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Free managed resources
                myAdornerLayer.Remove(this);
            }
        }

        #endregion
    }
}