using System;
using System.Runtime.InteropServices;

namespace Hdc.Mv
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageInfo
    {
        public int Index { get; set; }
        public int SurfaceTypeIndex { get; set; }
        public int PixelWidth { get; set; }
        public int PixelHeight { get; set; }
        public int BitsPerPixel { get; set; }
        public IntPtr BufferPtr { get; set; }
    };
}