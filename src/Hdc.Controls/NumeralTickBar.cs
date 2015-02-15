using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
namespace Hdc.Controls
{
    public static class SnapPixel
    {
        public static Point SnapToDevicePixel(this Point point, Pen pen)
        {
            return new Point(Math.Round(point.X) + pen.Thickness / 2, Math.Round(point.Y) + pen.Thickness / 2);
        }

        public static void DrawLineSnapped(this DrawingContext dc, Pen pen, Point point1, Point point2)
        {
            dc.DrawLine(pen, point1.SnapToDevicePixel(pen), point2.SnapToDevicePixel(pen));
        }
    }

    public class NumeralTickBar : TickBar
    {
        private Pen pen;

        private double fontSize;

        private Brush foreground;

        private int count;

        protected override void OnRender(DrawingContext dc)
        {
            pen = new Pen(Fill, 1);
            count = (int)((Maximum - Minimum) / TickFrequency);

            FormattedText formattedText;
            FormattedText maxText;

            fontSize = (double)GetValue(TextElement.FontSizeProperty);
            foreground = (Brush)GetValue(TextElement.ForegroundProperty);

            var texts = new List<FormattedText>();

            for (int i = 0; i < count; i++)
            {
                texts.Add(new FormattedText((Minimum + i * TickFrequency).ToString(),
                                            CultureInfo.GetCultureInfo("en-us"),
                                            FlowDirection.LeftToRight,
                                            new Typeface("Verdana"),
                                            fontSize,
                                            foreground));
            }
            maxText = new FormattedText(Maximum.ToString(),
                                        CultureInfo.GetCultureInfo("en-us"),
                                        FlowDirection.LeftToRight,
                                        new Typeface("Verdana"),
                                        fontSize,
                                        foreground);

            double xUnit;
            double yUnit;
            double x;
            double y;

            if (Placement == TickBarPlacement.Top || Placement == TickBarPlacement.Bottom)
            {
                DrawVerticalLines(dc);
                xUnit = ActualWidth / count;
                y = 0;

                for (int i = 0; i < count; i++)
                {
                    x = i * xUnit + 2;
                    dc.DrawText(texts[i], new Point(x, y));
                }
                dc.DrawText(maxText, new Point(ActualWidth, y));
            }

            else if (Placement == TickBarPlacement.Left || Placement == TickBarPlacement.Right)
            {
                DrawHorizontalLines(dc);

                yUnit = ActualHeight / count;
                x = 0;

                for (int i = 0; i < count; i++)
                {
                    y = ActualHeight - i * yUnit;
                    dc.DrawText(texts[i], new Point(x, y));
                }
                dc.DrawText(maxText, new Point(0, 0));
            }
        }

        private void DrawHorizontalLines(DrawingContext dc)
        {
            var yUnit = ActualHeight / count;
            double x = 0;
            double y = 0;
            for (int i = 0; i < count; i++)
            {
                y = ActualHeight - i * yUnit;
                dc.DrawLineSnapped(pen, new Point(0, y), new Point(ActualWidth, y));
            }
            dc.DrawLineSnapped(pen, new Point(0, 0), new Point(ActualWidth, 0));
        }

        private void DrawVerticalLines(DrawingContext dc)
        {
            var xUnit = ActualWidth / count;
            double x = 0;
            double y = 0;
            for (int i = 0; i < count; i++)
            {
                x = i * xUnit;
                dc.DrawLineSnapped(pen, new Point(x, 0), new Point(x, ActualHeight));
            }
            dc.DrawLineSnapped(pen, new Point(ActualWidth, 0), new Point(ActualWidth, ActualHeight));
        }
    }
}