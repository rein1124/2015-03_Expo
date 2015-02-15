using Hdc.Mv.Inspection;

namespace Hdc.Mv.ImageAcquisition
{
    public interface IImageLoader
    {
        ImageInfo LoadFromFile(string fileName);
    }
}