using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using HalconDotNet;
using Hdc.Collections.Generic;
using Hdc.Mv.Halcon;
using Hdc.Mv.Inspection.Mil.Interop;
using Omu.ValueInjecter;

namespace Hdc.Mv.Inspection.Mil
{
    public class MilGeneralInspector// : IGeneralInspector
    {
        private ImageInfo _imageInfo;

        public InspectionResult Inspect(InspectionSchema inspectionSchema)
        {
            var inspectionResult = new InspectionResult();

            Debug.WriteLine("MilGeneralInspector.Inspect in");

            int errorCode;

            InteropApi.CleanDefinitions();

            Debug.WriteLine("InteropApi.Calculate begin");
            errorCode = InteropApi.Calculate(_imageInfo);
            Debug.WriteLine("InteropApi.Calculate end");

            if (errorCode != 0)
            {
                Debug.WriteLine("InteropApi.Calculate error");
                throw new MilInteropException("Calculate", errorCode);
            }

            Debug.WriteLine("MilGeneralInspector.Inspect out");

            return inspectionResult;
        }

        public InspectionResult Inspect(HImage imageInfo, InspectionSchema inspectionSchema)
        {
            SetImageInfo(imageInfo);
            return Inspect(inspectionSchema);
        }
        
        public DefectResultCollection SearchDefects(HImage imageInfo)
        {
            var drs = new DefectResultCollection();
            //            return drs;

            InspectInfo inspectInfo;

            int errorCode = 0;

            errorCode = InteropApi.InspectCalculate(_imageInfo, imageInfo.ToImageInfo(), out inspectInfo);

            if (errorCode != 0)
                throw new MilInteropException("InteropApi.InspectCalculate", errorCode);

            for (int i = 0; i < inspectInfo.DefectsCount; i++)
            {
                DefectInfo defectInfo;
                errorCode = InteropApi.GetInspectDefect(i, out defectInfo);
                if (errorCode != 0)
                    throw new MilInteropException("InteropApi.GetInspectDefect", errorCode);

                var defectResult = new DefectResult();
                defectResult.InjectFrom(defectInfo);
                drs.Add(defectResult);
            }

            return drs;
        }

        public DefectResultCollection SearchDefects(HImage imageInfo, HImage mask)
        {
            var drs = new DefectResultCollection();

            InspectInfo inspectInfo;

            int errorCode = 0;

            errorCode = InteropApi.InspectCalculate(imageInfo.ToImageInfo(), mask.ToImageInfo(), out inspectInfo);

            if (errorCode != 0)
                throw new MilInteropException("InteropApi.InspectCalculate", errorCode);

            for (int i = 0; i < inspectInfo.DefectsCount; i++)
            {
                DefectInfo defectInfo;
                errorCode = InteropApi.GetInspectDefect(i, out defectInfo);
                if (errorCode != 0)
                    throw new MilInteropException("InteropApi.GetInspectDefect", errorCode);

                var defectResult = new DefectResult();
                defectResult.InjectFrom(defectInfo);
                drs.Add(defectResult);
            }

            return drs;
        }

        public void Dispose()
        {
            InteropApi.FreeApp();
        }

        public void Init()
        {
            InteropApi.InitApp(8192, 12500);
        }

        public void SetImageInfo(HImage imageInfo)
        {
            _imageInfo = imageInfo.ToImageInfo();
        }

        public void Init(int width, int height)
        {
            InteropApi.InitApp(8192, 12500);
        }

        public InspectionResult SearchCircles(ImageInfo imageInfo, InspectionSchema inspectionSchema)
        {
            throw new NotImplementedException();
        }

        public InspectionResult SearchEdges(ImageInfo imageInfo, InspectionSchema inspectionSchema)
        {
            throw new NotImplementedException();
        }

        public DistanceBetweenLinesResult GetDistanceBetweenLines(Line line1, Line line2)
        {
            double distInPixel;
            double distInWorld;
            double angle;
            Point footPoint1;
            Point footPoint2;

            var errorCode = InteropApi.GetDistanceBetweenLines(
                line1,
                line2,
                out distInPixel,
                out distInWorld,
                out angle,
                out footPoint1,
                out footPoint2);

            if (errorCode != 0)
                throw new MilInteropException("GetDistanceBetweenLines", errorCode);

            return new DistanceBetweenLinesResult()
                   {
                       DistanceInPixel = distInPixel,
                       DistanceInWorld = distInWorld,
                       Angle = angle,
                       FootPoint1 = footPoint1,
                       FootPoint2 = footPoint2,
                   };
        }

        public DistanceBetweenPointsResult GetDistanceBetweenPoints(Point point1, Point point2)
        {
            double distInPixel2;
            double distInWorld2;
            double angle2;

            var errorCode = InteropApi.GetDistanceBetweenPoints(
                point1,
                point2,
                out distInPixel2,
                out distInWorld2,
                out angle2);

            if (errorCode != 0)
                throw new MilInteropException("GetDistanceBetweenPoints", errorCode);

            return new DistanceBetweenPointsResult()
                   {
                       DistanceInPixel = distInPixel2,
                       DistanceInWorld = distInWorld2,
                       Angle = angle2,
                       Point1 = point1,
                       Point2 = point2,
                   };
        }
    }
}