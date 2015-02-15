using System.Windows;

namespace Hdc.Windows
{
    public static class Extensions
    {
        public static Point GetCenterPoint(this Rect rect)
        {
            return new Point(rect.X + rect.Width / 2,
                             rect.Y + rect.Height / 2);
        } 
    }
}