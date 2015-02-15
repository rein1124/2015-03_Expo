using Hdc.Mv;
using Hdc.Mv.ImageAcquisition;

namespace MvInspection.ImageAcquisition
{
    public class ImageLoader : IImageLoader
    {
        public ImageInfo LoadFromFile(string fileName)
        {
            return SimMatroxInteropApi.GrabSingleFrameFromFile(fileName);
        }
    }
}