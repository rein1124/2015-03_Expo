using System.Windows;

namespace Hdc.Mv
{
    public static class RelativeCoordinateEx
    {
        public static IRelativeCoordinate DefaultRelativeCoordinate { get; set; }

        public static Point GetRelativePoint(this Point relativePoint)
        {
            return DefaultRelativeCoordinate.GetRelativePoint(relativePoint);
        }

        public static Point GetOriginalPoint(this Point relativePoint)
        {
            return DefaultRelativeCoordinate.GetOriginalPoint(relativePoint);
        }

        public static Line GetOriginalLine(this Line relativePoint)
        {
            var originalLine = new Line(
                DefaultRelativeCoordinate.GetOriginalPoint(relativePoint.GetPoint1()),
                DefaultRelativeCoordinate.GetOriginalPoint(relativePoint.GetPoint2())
                );
            return originalLine;
        }

        public static Point GetRelativePoint(this IRelativeCoordinate coordinate, Point originalPoint)
        {
            return coordinate.GetRelativeVector(originalPoint.ToVector()).ToPoint();
        }

        public static Point GetOriginalPoint(this IRelativeCoordinate coordinate, Point relativePoint)
        {
            return coordinate.GetOriginalVector(relativePoint.ToVector()).ToPoint();
        }

        public static void ChangeOriginOffsetUsingRelative(this IRelativeCoordinate coordinate, Vector relativeVector)
        {
            coordinate.OriginOffset = relativeVector;
        }

        public static void ChangeOriginOffsetUsingActual(this IRelativeCoordinate coordinate, Vector actualVector)
        {
            var relativeVector = coordinate.GetRelativeVector(actualVector);
            coordinate.OriginOffset = relativeVector;
        }

        public static void ChangeOriginOffsetUsingRelative(this IRelativeCoordinate coordinate, Point relativePoint)
        {
            coordinate.ChangeOriginOffsetUsingRelative(relativePoint.ToVector());
        }

        public static void ChangeOriginOffsetUsingActual(this IRelativeCoordinate coordinate, Point actualPoint)
        {
            coordinate.ChangeOriginOffsetUsingActual(actualPoint.ToVector());
        }
    }
}