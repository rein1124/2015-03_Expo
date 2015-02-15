using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hdc.Controls
{
    public static class Ex
    {
        public static void MoveCenterPointInTheCanvas(this FrameworkElement rectangle, Point targetCenterPoint)
        {
            var p = rectangle.GetTopLeftCornerPoint(targetCenterPoint);

            rectangle.SetValue(Canvas.LeftProperty, p.X);
            rectangle.SetValue(Canvas.TopProperty, p.Y);
        }

        private static Point GetTopLeftCornerPoint(this FrameworkElement rectangle, Point targetCenterPoint)
        {
            var topLeftPointX = targetCenterPoint.X - rectangle.Width / 2;
            var topLeftPointY = targetCenterPoint.Y - rectangle.Height / 2;

            return new Point(topLeftPointX, topLeftPointY);
        }

        private static void MovePopupInTheCanvas(this FrameworkElement popup, Rect parentRect, Size canvasSize)
        {
            double borderX;
            double borderY;

            if ((parentRect.X + parentRect.Width + popup.ActualWidth) > canvasSize.Width)
                borderX = parentRect.X - popup.ActualWidth;
            else
                borderX = parentRect.X + parentRect.Width;

            if ((parentRect.Y + parentRect.Height + popup.ActualHeight) > canvasSize.Height)
                borderY = parentRect.Y - popup.ActualHeight;
            else
                borderY = parentRect.Y + parentRect.Height;

            popup.SetValue(Canvas.LeftProperty, borderX);
            popup.SetValue(Canvas.TopProperty, borderY);
        }

        private static void MovePopupInTheCanvas(this FrameworkElement popup, FrameworkElement parentRect,
            Size canvasSize)
        {
            Rect rect = new Rect((double)parentRect.GetValue(Canvas.LeftProperty),
                (double)parentRect.GetValue(Canvas.TopProperty), parentRect.Width, parentRect.Height);
            popup.MovePopupInTheCanvas(rect, canvasSize);
        }

        public static void MovePopupInTheCanvas(this FrameworkElement popup, FrameworkElement parentRect, Canvas canvas)
        {
            popup.MovePopupInTheCanvas(parentRect, new Size(canvas.ActualWidth, canvas.ActualHeight));
        }

        public static Point GetDisplayPoint(this Point targetPoint, Canvas canvas, BitmapSource bitmapSource)
        {
            var canvasActualSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            var imagePixelSize = new Size(bitmapSource.PixelWidth, bitmapSource.PixelHeight);

            return targetPoint.GetDisplayPoint(canvasActualSize, imagePixelSize);
        }

        private static Point GetDisplayPoint(this Point targetPoint, Size canvasSize, Size imagePixelSize)
        {
            Point center = new Point();
            var wRatio = canvasSize.Width / imagePixelSize.Width;
            var hRatio = canvasSize.Height / imagePixelSize.Height;

            double originX;
            double originY;
            double actualRatio;

            if (wRatio > hRatio)
            {
                actualRatio = hRatio;

                var imageActualWidth = imagePixelSize.Width * actualRatio;
                originX = (canvasSize.Width - imageActualWidth) / 2;

                center.X = targetPoint.X * actualRatio + originX;
                center.Y = targetPoint.Y * actualRatio;
            }
            else
            {
                actualRatio = wRatio;

                var imageActualHeight = imagePixelSize.Height * actualRatio;
                originY = (canvasSize.Height - imageActualHeight) / 2;

                center.X = targetPoint.X * actualRatio;
                center.Y = targetPoint.Y * actualRatio + originY;
            }
            return center;
        }
    }
}