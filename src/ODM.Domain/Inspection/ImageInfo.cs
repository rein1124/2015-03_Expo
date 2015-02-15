using System;

namespace ODM.Domain.Inspection
{
    public class ImageInfo
    {
        public int SurfaceTypeIndex { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int BitsPerPixel { get; set; }
        public IntPtr Buffer { get; set; }
    };
}

