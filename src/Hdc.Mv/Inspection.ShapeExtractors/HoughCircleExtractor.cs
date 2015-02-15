using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class HoughCircleExtractor : ICircleExtractor
    {
        public Circle FindCircle(HImage image, double centerX, double centerY, double innerRadius, double outerRadius)
        {
            HTuple cX, cY;
            HObject centerRegion;
            HDevelopExport.Singletone.FindCircleCenterUseHough(
                image,
                out centerRegion,
                MinGray,
                MaxGray,
                ExpectRadius,
                Percent,
                out cY,
                out cX
                );

            double x = cX;
            double y = cY;

            if (Math.Abs(x) > 0.000001 && Math.Abs(y) > 0.000001)
            {
                return new Circle(x, y, ExpectRadius);
            }
            else
            {
                return new Circle();
            }
        }

        public string Name { get; set; }

        public bool SaveCacheImageEnabled { get; set; }

        public double Sigma { get; set; }
        public int MinGray { get; set; }
        public int MaxGray { get; set; }
        public int ExpectRadius { get; set; }
        public int Percent { get; set; }
    }
//    [Serializable]
//    public class HoughCircleExtractor : ICircleExtractor
//    {
//        public Circle FindCircle(HImage image, double centerX, double centerY, double innerRadius, double outerRadius)
//        {
//            double cX, cY;
//
//            var isOK2 = HDevelopExport.Singletone.FindCircleCenterUseHough(
//                image,
//                centerX,
//                centerY,
//                innerRadius,
//                outerRadius,
//                Sigma,
//                MinGray,
//                MaxGray,
//                ExpectRadius,
//                Percent,
//                out cY,
//                out cX
//                );
//
//            if (isOK2)
//            {
//                return new Circle(cX, cY, ExpectRadius);
//            }
//            else
//            {
//                return new Circle();
//            }
//        }
//
//        public string Name { get; set; }
//
//        public bool SaveCacheImageEnabled { get; set; }
//
//        public double Sigma { get; set; }
//        public int MinGray { get; set; }
//        public int MaxGray { get; set; }
//        public int ExpectRadius { get; set; }
//        public int Percent { get; set; }
//    }
}