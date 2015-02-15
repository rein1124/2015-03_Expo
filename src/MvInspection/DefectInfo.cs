using System.Runtime.InteropServices;

namespace MvInspection
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DefectInfo
    {
        public int Index { get; set; }
        public int TypeCode { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public double X_Real { get; set; }
        public double Y_Real { get; set; }
        public double Width_Real { get; set; }
        public double Height_Real { get; set; }
        public double Size_Real { get; set; }
    };
}