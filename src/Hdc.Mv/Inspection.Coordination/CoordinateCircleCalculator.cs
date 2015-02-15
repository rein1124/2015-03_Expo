using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Hdc.Mv.Inspection
{
    public class CoordinateCircleCalculator
    {
        public static List<ReferenceCircleInfo> Calculate(IList<InspectionResult> inspectionResults)
        {
            Dictionary<string, List<ReferenceCircleInfo>> dictionary =
                new Dictionary<string, List<ReferenceCircleInfo>>();

            for (int i = 1; i < inspectionResults[0].CoordinateCircles.Count; i++)
            {
                dictionary.Add(inspectionResults[0].CoordinateCircles[i].Name, new List<ReferenceCircleInfo>());
            }

            foreach (var result in inspectionResults)
            {
                for (int i = 1; i < result.CoordinateCircles.Count; i++)
                {
                    var origin = result.CoordinateCircles[0];
                    var ref1 = result.CoordinateCircles[1];
                    var coodCircle = result.CoordinateCircles[i];

                    var name = result.CoordinateCircles[i].Name;

                    ReferenceCircleInfo ci = Calculate(
                        origin.Circle.GetCenterVector(),
                        ref1.Circle.GetCenterVector(),
                        coodCircle.Circle.GetCenterVector());

                    dictionary[name].Add(ci);
                }
            }

            List<ReferenceCircleInfo> avgReferenceCircleInfos = new List<ReferenceCircleInfo>();

            foreach (var entry in dictionary)
            {
                var name = entry.Key;
                var list = entry.Value;

                var angle = list.Average(x => x.Angle);
                var offsetX = list.Average(x => x.Offset.X);
                var offsetY = list.Average(x => x.Offset.Y);
                var length = list.Average(x => x.Length);

                var angleR2b = 90 - angle;

                var r2X = Math.Sin(angle*(Math.PI/180.0))*length;
                var r2Y = Math.Cos(angle*(Math.PI/180.0))*length;

                avgReferenceCircleInfos.Add(new ReferenceCircleInfo()
                                            {
                                                Name = name,
                                                Angle = angleR2b,
                                                Offset = new Vector(r2X, r2Y),
                                                Length = length,
                                            });
            }

            return avgReferenceCircleInfos;
        }

        public static ReferenceCircleInfo Calculate(Vector r0, Vector r1, Vector r2)
        {
            var vR1 = r1 - r0;
            var vR2 = r2 - r0;

            var angleR2 = Vector.AngleBetween(vR2, vR1);

            var oldCenter = (r1 + r0)/2;
            var oldCenterPixel = new Vector(oldCenter.X*1000/16.0, oldCenter.Y*1000/16.0);

            var center = (r0 + r1 + r2)/3.0;
            var centerPixel = new Vector(center.X*1000/16.0, center.Y*1000/16.0);

            var offset = centerPixel - oldCenterPixel;

            return new ReferenceCircleInfo()
                   {
                       Angle = angleR2,
                       Offset = offset,
                       //                R1Length = vR1.Length,
                       Length = vR2.Length,
                   };
        }
    }

    public class ReferenceCircleInfo
    {
        public string Name { get; set; }
        public double Angle { get; set; }
        public Vector Offset { get; set; }
        public double Length { get; set; }
    }
}