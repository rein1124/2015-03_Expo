using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface ICircleExtractor
    {
        Circle FindCircle(HImage image, double centerX, double centerY, double innerRadius, double outerRadius);

        string Name { get; set; }

        bool SaveCacheImageEnabled { get; set; }
    }
}