using System.Runtime.InteropServices;
using Hdc.Mv;

namespace MvInspection.ImageAcquisition
{
    public static class E2VMatroxInteropApi
    {
        [DllImport(@"..\Release\MvInspection.Matrox.NativeApi.dll")]
        public static extern int InitGrabDevice();

        [DllImport(@"..\Release\MvInspection.Matrox.NativeApi.dll")]
        public static extern ImageInfo GrabSingleFrame();

        [DllImport(@"..\Release\MvInspection.Matrox.NativeApi.dll")]
        public static extern void FreeDevice();
    }
}
