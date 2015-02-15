using Hdc.Mv;
using Hdc.Mv.ImageAcquisition;

namespace MvInspection.ImageAcquisition
{
    public class E2VMatroxCamera: ICamera
    {
        public void Dispose()
        {
            E2VMatroxInteropApi.FreeDevice();
        }

        public bool Init()
        {
           return E2VMatroxInteropApi.InitGrabDevice() == 0 ;
        }

        public ImageInfo Acquisition()
        {
            return E2VMatroxInteropApi.GrabSingleFrame();
        }
    }
}