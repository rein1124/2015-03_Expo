using System.Runtime.InteropServices;
using Hdc.Mv;

namespace MvInspection.ImageAcquisition
{
    public static class SimMatroxInteropApi
    {
        [DllImport(@"MvInspection.Matrox.NativeApi.Simulation.dll")]
        public static extern int InitGrabDevice();

        [DllImport(@"MvInspection.Matrox.NativeApi.Simulation.dll")]
        public static extern ImageInfo GrabSingleFrame();

        [DllImport(@"MvInspection.Matrox.NativeApi.Simulation.dll")]
        public static extern ImageInfo GrabSingleFrameFromFile([MarshalAs(UnmanagedType.LPTStr)] string fileName);

        [DllImport(@"MvInspection.Matrox.NativeApi.Simulation.dll")]
        public static extern void FreeDevice();
    }
}