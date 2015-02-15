using System.Runtime.InteropServices;

namespace MvInspection
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MeasurementInfo
    {
        public int Index { get; set; }
        public int TypeCode { get; set; }
        public int StartPointX { get; set; }
        public int StartPointY { get; set; }
        public int EndPointX { get; set; }
        public int EndPointY { get; set; }
        public int Value { get; set; }
        public int GroupIndex { get; set; }
        public double StartPointX_Real { get; set; }
        public double StartPointY_Real { get; set; }
        public double EndPointX_Real { get; set; }
        public double EndPointY_Real { get; set; }
        public double Value_Real { get; set; }
    };
}