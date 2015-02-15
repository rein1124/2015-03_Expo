using System.Runtime.InteropServices;

namespace MvInspection
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct InspectInfo
    {
        public int Index;

        public int SurfaceTypeIndex;

        public int HasError;

        public int DefectsCount;

//        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
//        public DefectInfo[] DefectInfos;

        public int MeasurementsCount;

//        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
//        public MeasurementInfo[] MeasurementInfos;
    };
}