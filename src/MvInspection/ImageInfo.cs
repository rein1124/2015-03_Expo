using System;
using System.Runtime.InteropServices;

namespace MvInspection
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageInfo
    {
        public int Index { get; set; }
        public int SurfaceTypeIndex { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int BitsPerPixel { get; set; }
        public IntPtr Buffer { get; set; }
    };
}