using System.Windows;

namespace Hdc.Mv
{
    public class RelativeCoordinate : IRelativeCoordinate
    {
        private readonly Vector _coordinateVector;
        private readonly double _coordinateVectorAngle;
        private readonly Vector _originVector;
        private readonly Vector _relativeOrigin;

        public Vector GetRelativeVector(Vector originalVector)
        {
            var v2 = originalVector;
            var vFromOriginToTarget = v2.GetVectorTo(_originVector);
            var vectorNoAngle = new Vector(vFromOriginToTarget.Length, 0);

            var angleBetweenTargetAndRight = vFromOriginToTarget.GetAngleTo(_coordinateVector);
            var vectorIncludeAngle = vectorNoAngle.Rotate(0 - angleBetweenTargetAndRight);
            return vectorIncludeAngle + OriginOffset;
        }

        public Vector GetOriginalVector(Vector relativeVector)
        {
            if (relativeVector == new Vector(0, 0))
                return _originVector;

            var vSum = relativeVector + OriginOffset - _relativeOrigin; ;

            var vectorIncludeAngle = vSum.Rotate(_coordinateVectorAngle);
            return vectorIncludeAngle;
        }

        public Vector OriginOffset { get; set; }

        public double GetCoordinateAngle()
        {
            return _coordinateVectorAngle;
        }

        public RelativeCoordinate(Vector originVector, double coordinateAngle)
        {
            _originVector = originVector;
            _coordinateVector = new Vector(100, 0).Rotate(coordinateAngle);
            _coordinateVectorAngle = coordinateAngle;

            _relativeOrigin = GetRelativeVector(new Vector(0, 0));
        }

        public RelativeCoordinate(Point originPoint, double coordinateAngle)
            : this(originPoint.ToVector(), coordinateAngle)
        {

        }

        public RelativeCoordinate(Vector originVector, Vector refVector, double refAngle)
        {
            var link = refVector - originVector;
            var angleBetween = Vector.AngleBetween(new Vector(100, 0), link);
            var coordinateVectorAngle = angleBetween - refAngle;

            _originVector = originVector;
            _coordinateVector = new Vector(100, 0).Rotate(coordinateVectorAngle);
            _coordinateVectorAngle = coordinateVectorAngle;

            _relativeOrigin = GetRelativeVector(new Vector(0, 0));
        }

        public RelativeCoordinate(Point originPoint, Point refPoint, double refAngle)
            : this(originPoint.ToVector(),refPoint.ToVector(),refAngle)
        {
            
        }

        public Vector CoordinateVector
        {
            get { return _coordinateVector; }
        }

        public double CoordinateVectorAngle
        {
            get { return _coordinateVectorAngle; }
        }

        public Vector OriginVector
        {
            get { return _originVector; }
        }

        public Vector RelativeOrigin
        {
            get { return _relativeOrigin; }
        }
    }
}