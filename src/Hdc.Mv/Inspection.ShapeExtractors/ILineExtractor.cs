using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface ILineExtractor
    {
        Line FindLine(HImage image, Line searchLine);

        string Name { get; set; }

        bool SaveCacheImageEnabled { get; set; }
    }
}