using System;

namespace Hdc.Mv
{
    [Serializable]
    public class Rectangle2 : IRectangle2
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Angle { get; set; }
        public double HalfWidth { get; set; }
        public double HalfHeight { get; set; } 
    }
}