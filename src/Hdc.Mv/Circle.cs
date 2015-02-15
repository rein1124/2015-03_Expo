using System.Windows;

namespace Hdc.Mv
{
    public struct Circle
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Radius { get; set; }


//        public Circle(Point centerPoint)
//            : this(centerPoint.X, centerPoint.Y)
//        { 
//        }

        public Circle(Point centerPoint, double radius = 0)
            : this(centerPoint.X, centerPoint.Y, radius)
        {
        }


//        public Circle(double centerX, double centerY)
//            : this()
//        {
//            CenterX = centerX;
//            CenterY = centerY;
//        }

        public Circle(double centerX, double centerY, double radius = 0)
            : this()
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = radius;
        }

        public bool Equals(Circle other)
        {
            return CenterX.Equals(other.CenterX) && CenterY.Equals(other.CenterY) && Radius.Equals(other.Radius);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Circle && Equals((Circle) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = CenterX.GetHashCode();
                hashCode = (hashCode*397) ^ CenterY.GetHashCode();
                hashCode = (hashCode*397) ^ Radius.GetHashCode();
                return hashCode;
            }
        }

        private static readonly Circle Empty = new Circle();

        public bool IsEmpty
        {
            get { return Equals(this, Empty); }
        }
    }
}