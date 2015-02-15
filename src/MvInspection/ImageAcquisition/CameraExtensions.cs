using System.Threading.Tasks;
using MvInspection;

namespace MvInspection.ImageAcquisition
{
    public static class CameraExtensions
    {
        public static Task<ImageInfo> AcquisitionAsync(this ICamera camera)
        {
            return Task.Run(() => camera.Acquisition());
        }
    }
}