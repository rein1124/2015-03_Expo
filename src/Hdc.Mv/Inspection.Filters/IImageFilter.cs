using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface IImageFilter
    {
        HImage Process(HImage image);
    }
}