using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Hdc.Mv;
using Hdc.Mv.Halcon;
using Hdc.Mv.Inspection;
using Hdc.Windows.Media.Imaging;
using Microsoft.Practices.ServiceLocation;
using ODM.Domain.Schemas;
using Omu.ValueInjecter;

namespace ODM.Domain.Inspection
{
    public static class Ex
    {
        static Ex()
        {
//            Mapper.CreateMap<Inspection.DefectInfo, DefectInfo>()
//                  .ForMember(x => x.X, o => o.MapFrom(x => x.DefectPosX))
//                  .ForMember(x => x.Y, o => o.MapFrom(x => x.DefectPosY))
//                  .ForMember(x => x.Width, o => o.MapFrom(x => x.DefectWidth))
//                  .ForMember(x => x.Height, o => o.MapFrom(x => x.DefectHeight))
//                  .ForMember(x => x.Index, o => o.MapFrom(x => x.DefectId))
                ;
        }

        public static WorkpieceInfoEntry ToEntry(this WorkpieceInfo workpieceInfo)
        {
            var id = new WorkpieceInfoEntry();
            id.InjectFrom(workpieceInfo);
            return id;
        }

//        public static InspectInfo ToEntity(this Hdc.Mv.Inspection.InspectionInfo di)
//        {
//            var inspectInfo = new InspectInfo()
//                              {
//                                  Index = di.Index,
//                                  HasError = di.HasError,
//                                  SurfaceTypeIndex = di.SurfaceTypeIndex,
//                                  DefectInfos =  di.DefectInfos.Select(x=>x.ToEntity()).ToList(),
//                                  MeasurementInfos =  di.MeasurementInfos.Select(x=>x.ToEntity()).ToList(),
//                              };
//            return inspectInfo;
//        }


        public static ImageInfo ToEntity(this Hdc.Mv.ImageInfo ii)
        {
            var ii2 = new ImageInfo()
            {
                SurfaceTypeIndex = ii.SurfaceTypeIndex,
                Width = ii.PixelWidth,
                Height = ii.PixelHeight,
                BitsPerPixel = ii.BitsPerPixel,
                Buffer = ii.BufferPtr,
            };
            return ii2;
        }

//        public static DefectInfo ToEntity(this Hdc.Mv.Inspection.DefectInfo ii)
//        {
//            var ii2 = new DefectInfo()
//            {
//                Index = ii.Index,
//                TypeCode = ii.TypeCode,
//                Type = (DefectType)ii.TypeCode,
//                X = ii.X,
//                Y = ii.Y,
//                Width = ii.Width,
//                Height = ii.Height,
//                Size = ii.Size,
//
//                XActualValue = ii.X_Real,
//                YActualValue = ii.Y_Real,
//                WidthActualValue = ii.Width_Real,
//                HeightActualValue = ii.Height_Real,
//                SizeActualValue = ii.Size_Real,
//            };
//            return ii2;
//        }

//        public static MeasurementInfo ToEntity(this Hdc.Mv.Inspection.MeasurementInfo ii)
//        {
//            var ii2 = new MeasurementInfo()
//            {
//                Index = ii.Index,
//                TypeCode = ii.TypeCode,
//                StartPointX = ii.StartPointX,
//                StartPointY = ii.StartPointY,
//                EndPointX = ii.EndPointX,
//                EndPointY = ii.EndPointY,
//                Value = ii.Value,
//                GroupIndex = ii.GroupIndex,
//
//                StartPointXActualValue = ii.StartPointXActualValue,
//                StartPointYActualValue = ii.StartPointYActualValue,
//                EndPointXActualValue = ii.EndPointXActualValue,
//                EndPointYActualValue = ii.EndPointYActualValue,
//                ValueActualValue = ii.ValueActualValue,
//            };
//            return ii2;
//        }


        public static StoredImageInfo SaveImage(this SurfaceInspectInfo surfaceInspectInfo)
        {
            var prefix = DateTime.Now.ToString(
                //                @"yyyy-MM-dd_hh\Hmm\Mss\S") + "_"
                @"yyyy-MM-dd_HH.mm.ss") + "_"
                         + "WI" + surfaceInspectInfo.WorkpieceIndex + "."
                         + "SI" + surfaceInspectInfo.SurfaceTypeIndex + "."
                         + "_";
            var sii = ImageSaveLoadService.StoreImage(
                fileName => surfaceInspectInfo.BitmapSource
                    .SaveToTiffAsync(fileName), prefix, ".tif");
            sii.SurfaceTypeIndex = surfaceInspectInfo.SurfaceTypeIndex;
            return sii;
        }

        private static IImageSaveLoadService _imageSaveLoadService;

        public static IImageSaveLoadService ImageSaveLoadService
        {
            get
            {
                return _imageSaveLoadService ??
                       (_imageSaveLoadService = ServiceLocator.Current.GetInstance<IImageSaveLoadService>());
            }
        }

        public static Task InspectImageFileAsync(this IInspectService inspectService, int surfaceTypeIndex, string fileName)
        {
            var task= Task.Run(() => inspectService.InspectImageFile(surfaceTypeIndex, fileName));
            return task;
        }

        public static InspectInfo GetInspectionInfo(this IInspectionController inspectionController)
        {
            return inspectionController.InspectionResult.GetInspectionInfo(inspectionController.Coordinate);
        }

        public static MeasurementInfo GetMeasurementInfo(this Line line, IRelativeCoordinate coordinate)
        {
            Vector relativeP1 = coordinate.GetRelativeVector(line.GetPoint1().ToVector());
            Vector relativeP2 = coordinate.GetRelativeVector(line.GetPoint2().ToVector());

            var measurement = new MeasurementInfo
                              {
                                  StartPointX = line.GetPoint1().X,
                                  StartPointY = line.GetPoint1().Y,
                                  EndPointX = line.GetPoint2().X,
                                  EndPointY = line.GetPoint2().Y,
                                  Value = line.GetLength(),
                                  ValueActualValue = line.GetLength().ToMillimeterFromPixel(16),
                                  StartPointXActualValue = relativeP1.X.ToMillimeterFromPixel(16),
                                  StartPointYActualValue = relativeP1.Y.ToMillimeterFromPixel(16),
                                  EndPointXActualValue = relativeP2.X.ToMillimeterFromPixel(16),
                                  EndPointYActualValue = relativeP2.Y.ToMillimeterFromPixel(16),
                              };

            return measurement;
        }

        public static InspectInfo GetInspectionInfo(this InspectionResult inspectionResult, IRelativeCoordinate coordinate)
        {
            var inspectionInfo = new InspectInfo();

            Debug.WriteLine("GetInspectionInfo().Start");

            for (int i = 0; i < inspectionResult.DistanceBetweenPointsResults.Count; i++)
            {
                var result = inspectionResult.DistanceBetweenPointsResults[i];
                if (result.HasError)
                {
                    var measurement2 = new MeasurementInfo { HasError = true };
                    inspectionInfo.MeasurementInfos.Add(measurement2);
                    continue;
                };

                var line = new Line(result.Point1, result.Point2);
                var measurement = line.GetMeasurementInfo(coordinate);
                measurement.Index = i;
                measurement.GroupName = result.Definition.GroupName;
                measurement.TypeCode = 100 + i;
                measurement.Name = result.Definition.Name;
                measurement.DisplayName = result.Definition.DisplayName;
                measurement.ExpectValue = result.Definition.ExpectValue;
                inspectionInfo.MeasurementInfos.Add(measurement);
            }

            Debug.WriteLine("GetInspectionInfo().DistanceBetweenPointsResults");


            for (int i = 0; i < inspectionResult.RegionTargets.Count; i++)
            {
                var result = inspectionResult.RegionTargets[i];

                if (!result.Definition.Rect2Len2Line_DisplayEnabled &&
                    !result.Definition.Rect2Len1Line_DisplayEnabled)
                    continue;

                if (result.HasError)
                {
                    var measurement = new MeasurementInfo {HasError = true};
                    inspectionInfo.MeasurementInfos.Add(measurement);
                    continue;
                };

                var rect2 = result.TargetRegion.GetSmallestHRectangle2();
                var roiRect = rect2.GetRoiRectangle();

                if (result.Definition.Rect2Len2Line_DisplayEnabled)
                {
                    var line = roiRect.GetWidthLine();
                    var measurement = line.GetMeasurementInfo(coordinate);
                    measurement.DisplayName = result.Definition.Rect2Len2Line_DisplayName;
                    measurement.GroupName = result.Definition.Rect2Len2Line_GroupName;
                    measurement.ExpectValue = result.Definition.Rect2Len2Line_ExpectValue;
                    inspectionInfo.MeasurementInfos.Add(measurement);
                }

                if (result.Definition.Rect2Len1Line_DisplayEnabled)
                {
                    var line = roiRect.GetLine();
                    var measurement = line.GetMeasurementInfo(coordinate);
                    measurement.DisplayName = result.Definition.Rect2Len1Line_DisplayName;
                    measurement.GroupName = result.Definition.Rect2Len1Line_GroupName;
                    measurement.ExpectValue = result.Definition.Rect2Len1Line_ExpectValue;
                    inspectionInfo.MeasurementInfos.Add(measurement);
                }
            }

            Debug.WriteLine("GetInspectionInfo().RegionTargets");


            for (int i = 0; i < inspectionResult.Circles.Count; i++)
            {
                var result = inspectionResult.Circles[i];

                if (!result.Definition.Diameter_DisplayEnabled) 
                    continue;

                if (result.HasError)
                {
                    var measurement2 = new MeasurementInfo { HasError = true };
                    inspectionInfo.MeasurementInfos.Add(measurement2);
                    continue;
                };

                var line = result.Circle.GetLine(-45);
                var measurement = line.GetMeasurementInfo(coordinate);
                measurement.DisplayName = result.Definition.Diameter_DisplayName;
                measurement.GroupName = result.Definition.Diameter_GroupName;
                measurement.ExpectValue = result.Definition.Diameter_ExpectValue;
                inspectionInfo.MeasurementInfos.Add(measurement);
            }

            Debug.WriteLine("GetInspectionInfo().Circles");

            for (int i = 0; i < inspectionResult.Parts.Count; i++)
            {
                var part = inspectionResult.Parts[i];

                if (part.PartRegion==null)
                {
                    Debug.WriteLine("GetInspectionInfo().Parts: PartRegion == null");
                    continue;
                }

                if (part.PartRegion.CountObj() == 0)
                {
                    Debug.WriteLine("GetInspectionInfo().Parts: PartRegion.CountObj == 0");
                    continue;
                }

                if (part.PartRegion.GetArea() < 1)
                {
                    Debug.WriteLine("GetInspectionInfo().Parts: PartRegion.GetArea() < 1");
                    continue;
                }

                var partRegion = part.PartRegion;

                var centerX = partRegion.GetColumn();
                var centerY = partRegion.GetRow();
                var x = partRegion.GetColumn1();
                var y = partRegion.GetRow1();
                var width = partRegion.GetWidth();
                var height = partRegion.GetHeight();
                var area = partRegion.GetArea();

                var relativeCenter = coordinate.GetRelativeVector(new Vector(centerX,centerY));

                var di = new DefectInfo
                {
                    Index = i,
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height,
                    TypeCode = (int) DefectType.PartExist,
                    Size = area,
                    XActualValue = relativeCenter.X.ToMillimeterFromPixel(16),
                    YActualValue = relativeCenter.Y.ToMillimeterFromPixel(16),
                    WidthActualValue = ((double)width).ToMillimeterFromPixel(16),
                    HeightActualValue = ((double)height).ToMillimeterFromPixel(16),
                    SizeActualValue = area * 256.0 / 1000000.0
                };

                inspectionInfo.DefectInfos.Add(di);
            }

            Debug.WriteLine("GetInspectionInfo().Parts");

            var drs = inspectionResult.RegionDefectResults.SelectMany(x => x.DefectResults).ToList();

            for (int i = 0; i < drs.Count; i++)
            {
                var dr = drs[i];

                var relPoint = coordinate.GetRelativeVector(new Vector(dr.X - dr.Width / 2.0, dr.Y - dr.Height / 2.0));

                var di = new DefectInfo
                {
                    Index = i,
                    X = dr.X - dr.Width / 2.0,
                    Y = dr.Y - dr.Height / 2.0,
                    Width = dr.Width,
                    Height = dr.Height,
                    TypeCode = dr.TypeCode,
                    Size = dr.Size * 256.0 / 1000000.0,
                    XActualValue = relPoint.X.ToMillimeterFromPixel(16),
                    YActualValue = relPoint.Y.ToMillimeterFromPixel(16),
                    WidthActualValue = dr.Width.ToMillimeterFromPixel(16),
                    HeightActualValue = dr.Height.ToMillimeterFromPixel(16),
                    SizeActualValue = dr.Size * 256.0 / 1000000.0
                };

                inspectionInfo.DefectInfos.Add(di);
            }

            Debug.WriteLine("GetInspectionInfo().RegionDefectResults");

            return inspectionInfo;
        }
    }
}