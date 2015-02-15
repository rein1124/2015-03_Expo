using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hdc.Mv.Inspection.Mil.Interop;

namespace Hdc.Mv.Inspection.Mil.NativeApi.Sim.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int ret;
            ret = SimInteropApi.InitApp(0,0);

            ret = SimInteropApi.AddEdgeDefinition(
                startPointX:11,
                startPointY:12,
                endPointX:21,
                endPointY:22,
                roiWidth:33,
                polarity:44,
                orientation:55);

            ret = SimInteropApi.AddCircleDefinition(
                circleCenterX: 11,
                circleCenterY: 22,
                innerCircleRadius: 33,
                outerCircleRadius: 44,
                lowThreshold: 55,
                highThreshold: 66,
                posDiff:77,
                radDiff:88,
                ratio:99,
                processtype:111);

            int edgeCount;
            ret = SimInteropApi.GetEdgeDefinitionsCount(out edgeCount);

            int circleCount;
            ret = SimInteropApi.GetCircleDefinitionsCount(out circleCount);

            ret = SimInteropApi.CleanDefinitions();

            ret = SimInteropApi.Calculate(new ImageInfo()
                                               {
                                                   Index = 101,
                                                   SurfaceTypeIndex = 102,
                                                   PixelWidth = 103,
                                                   PixelHeight = 104,
                                                   BitsPerPixel = 105,
                                               });

            Line edgeLine;
            Point intersectionPoint;
            ret = SimInteropApi.GetEdgeResult(11, out edgeLine, out intersectionPoint);

            Circle foundCircle;
            int isNotFound;
            ret = SimInteropApi.GetCircleResult(11, out foundCircle, out isNotFound);



            double distInPixel;
            double distInWorld;
            double angle;
            Point footPoint1;
            Point footPoint2;
            ret = SimInteropApi.GetDistanceBetweenLines(
                new Line(101,102,103,104),
                new Line(201,202,203,204),
                out distInPixel,
                out distInWorld,
                out angle,
                out footPoint1,
                out footPoint2
                );

            double distInPixel2;
            double distInWorld2;
            double angle2;
            ret = SimInteropApi.GetDistanceBetweenPoints(
                new Point(101,102), 
                new Point(201,202), 
                out distInPixel2,
                out distInWorld2,
                out angle2);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
