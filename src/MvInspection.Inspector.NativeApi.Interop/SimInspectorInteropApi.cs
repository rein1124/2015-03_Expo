using System.Runtime.InteropServices;
using System.Security;
using Hdc.Mv;
using Hdc.Mv.Inspection;

namespace MvInspection.Inspection
{
    public static class SimInspectorInteropApi
    {
        // General
        [DllImport(@"MvInspection.Inspector.NativeApi.Simulation.dll")]
        public static extern int Init();

        [DllImport(@"MvInspection.Inspector.NativeApi.Simulation.dll")]
        public static extern int LoadParameters();

        [DllImport(@"MvInspection.Inspector.NativeApi.Simulation.dll")]
        public static extern void FreeObject();

        // Inspect
        [DllImport(@"MvInspection.Inspector.NativeApi.Simulation.dll", CallingConvention = CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public static extern InspectInfo Inspect([In]ImageInfo imageInfo,
            [In, Out]DefectInfo[] defectInfos,
            [In, Out]MeasurementInfo[] measurementInfos);
    }
}