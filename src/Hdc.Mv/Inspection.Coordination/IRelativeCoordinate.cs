using System.Windows;

namespace Hdc.Mv
{
    public interface IRelativeCoordinate
    {
        Vector GetRelativeVector(Vector originalVector);

        Vector GetOriginalVector(Vector relativeVector);

        Vector OriginOffset { get; set; }

        double GetCoordinateAngle();
    }
}