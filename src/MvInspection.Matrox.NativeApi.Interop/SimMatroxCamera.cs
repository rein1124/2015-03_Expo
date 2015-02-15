using Hdc.Mv;
using Hdc.Mv.ImageAcquisition;

namespace MvInspection.ImageAcquisition
{
    public class SimMatroxCamera: ICamera
    {
        public void Dispose()
        {
            SimMatroxInteropApi.FreeDevice();
        }

        public bool Init()
        {
            return SimMatroxInteropApi.InitGrabDevice() == 0;
        }

        public ImageInfo Acquisition()
        {
            return SimMatroxInteropApi.GrabSingleFrame();
        }
    }
}