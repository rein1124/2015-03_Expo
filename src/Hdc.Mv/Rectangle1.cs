using System;

namespace Hdc.Mv
{
    [Serializable]
    public class Rectangle1 : IRectangle1
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }
}