using System;
using System.Collections.Generic;
using System.Windows;

namespace Hdc.Mv
{
    [Serializable]
    public struct Line
    {
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }

        public Line(double x1, double y1, double x2, double y2) : this()
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Line(Point p1, Point p2) : this()
        {
            X1 = p1.X;
            Y1 = p1.Y;
            X2 = p2.X;
            Y2 = p2.Y;
        }

        public bool Equals(Line other)
        {
            return X1.Equals(other.X1) && X2.Equals(other.X2) && Y1.Equals(other.Y1) && Y2.Equals(other.Y2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Line && Equals((Line) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X1.GetHashCode();
                hashCode = (hashCode*397) ^ X2.GetHashCode();
                hashCode = (hashCode*397) ^ Y1.GetHashCode();
                hashCode = (hashCode*397) ^ Y2.GetHashCode();
                return hashCode;
            }
        }

        private static readonly Line Empty = new Line();

        public bool IsEmpty
        {
            get { return Equals(this, Empty); }
        }
    }
}