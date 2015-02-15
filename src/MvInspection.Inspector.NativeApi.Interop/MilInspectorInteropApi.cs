using System.Runtime.InteropServices;
using System.Security;
using Hdc.Mv;
using Hdc.Mv.Inspection;

namespace MvInspection.Inspection
{
    public static class MilInspectorInteropApi
    {
        // General
        [DllImport(@"MvHdcDll.dll")]
        public static extern int Init();

        [DllImport(@"MvHdcDll.dll")]
        public static extern int LoadParameters();

        [DllImport(@"MvHdcDll.dll")]
        public static extern void FreeObject();

        // Inspect
        [DllImport(@"MvHdcDll.dll", CallingConvention = CallingConvention.Cdecl)]
        [SuppressUnmanagedCodeSecurity]
        public static extern InspectInfo Inspect([In]ImageInfo imageInfo,
                                                 [In, Out]DefectInfo[] defectInfos,
                                                 [In, Out]MeasurementInfo[] measurementInfos);
    }
}