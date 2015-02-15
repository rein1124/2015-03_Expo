using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hdc.Mv.Inspection.Mil.Interop
{
#if SIM
    public static class SimInteropApi
#else
    public static class InteropApi
#endif
    {
#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int InitApp(int width, int height);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int FreeApp();

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int AddEdgeDefinition(
            double startPointX,
            double startPointY,
            double endPointX,
            double endPointY,
            double roiWidth, // half of width
            int polarity,
            int orientation);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int AddCircleDefinition(
            double circleCenterX,
            double circleCenterY,
            double innerCircleRadius,
            double outerCircleRadius,
            int lowThreshold, // if low=0, select x>hight, mains select bright area
            int highThreshold, // if low!=0, select x<low, mains select dark area
            double posDiff, // unit: pixel, posDiff < 10, e.g: 5 
            double radDiff, // unit: pixel, radDiff < 10, e.g: 5
            double ratio, // 0 < ratio < 1, e.g: 0.5
            int processtype // 0: use, !=0 not use. use low & high Threshold
            );

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetEdgeDefinitionsCount(
            out int count);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetCircleDefinitionsCount(
            out int count);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int CleanDefinitions();


#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int Calculate(ImageInfo imageInfo);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetEdgeResult(
            int index,
            out Line edgeLine,
            out Point intersectionPoint);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetCircleResult(
            int index,
            out Circle foundCircle,
            out int isNotFound); // notFound = 0, OK. notFound = 1, cannot found.


#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetDistanceBetweenLines(
            Line line1,
            Line line2,
            out double distanceInPixel,
            out double distanceInWorld,
            out double angle,
            out Point footPoint1,
            out Point footPoint2
            );

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetDistanceBetweenPoints(
            Point point1,
            Point point2,
            out double distanceInPixel,
            out double distanceInWorld,
            out double angle
            );

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int InspectCalculate(ImageInfo imageInfo, ImageInfo maskImageInfo, out InspectInfo inspectInfo);

#if SIM
        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
#else
        [DllImport(@"..\Release\Hdcmvbyd.dll")]
#endif
        public static extern int GetInspectDefect(int defectIndex, out DefectInfo defectInfo);

//#if SIM
//        [DllImport(@"Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll")]
//#else
//        [DllImport(@"..\Release\Hdcmvbyd.dll")]
//#endif
//        public static extern int GetMeasurementInfo(int measurementIndex, out MeasurementInfo measurementInfo);
    }
}