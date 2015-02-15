using System.Windows;

namespace Hdc.Mv
{
    public  class MockRelativeCoordinate : IRelativeCoordinate
    {
        public Vector GetRelativeVector(Vector originalVector)
        {
            return originalVector;
        }

        public Vector GetOriginalVector(Vector relativeVector)
        {
            return relativeVector;
        }

        public Vector OriginOffset { get; set; }

        public double GetCoordinateAngle()
        {
           return 0.0;
        }
    }
}