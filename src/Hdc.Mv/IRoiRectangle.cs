// ReSharper disable InconsistentNaming

namespace Hdc.Mv
{
    public interface IRoiRectangle
    {
        double StartX { get; set; }
        double StartY { get; set; }
        double EndX { get; set; }
        double EndY { get; set; }
        double ROIWidth { get; set; }
    }
}

// ReSharper restore InconsistentNaming