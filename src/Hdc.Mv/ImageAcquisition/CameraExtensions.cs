using System.Threading.Tasks;
using Hdc.Mv.Inspection;

namespace Hdc.Mv.ImageAcquisition
{
    public static class CameraExtensions
    {
        public static Task<ImageInfo> AcquisitionAsync(this ICamera camera)
        {
            return Task.Run(() => camera.Acquisition());
        }
    }
}