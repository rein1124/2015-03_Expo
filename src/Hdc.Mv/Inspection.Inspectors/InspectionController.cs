using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Windows;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Reflection;
using Hdc.Serialization;

namespace Hdc.Mv.Inspection
{
    public interface IInspectionController :
        ISetInspectionSchema,
        ISetImage,
        IDisposable
    {
        InspectionResult InspectionResult { get; }

        IRelativeCoordinate Coordinate { get; }

        HImage Image { get; }

        InspectionSchema InspectionSchema { get; }
    }

    public interface ISetInspectionSchema
    {
        ISetImage SetInspectionSchema(InspectionSchema inspectionSchema);
    }

    public interface ICreateCoordinate
    {
        IInspect CreateCoordinate();
    }

    public interface IInspect
    {
        IInspectionController Inspect();
    }

    public interface ISetImage
    {
        ICreateCoordinate SetImage(HImage image);
    }

    public class InspectionController :
        IInspectionController,
        ISetInspectionSchema,
        ISetImage,
        ICreateCoordinate,
        IInspect
    {
        private IRelativeCoordinate _coordinate;
        private IObservable<InspectionResult> _coordinateCreatedEvent = new Subject<InspectionResult>();
        private HImage _image;
        private InspectionSchema _inspectionSchema;
        private InspectionResult _inspectionResult;

        public ICreateCoordinate SetImage(HImage image)
        {
            var dir = typeof(Ex).Assembly.GetAssemblyDirectoryPath();
            var cacheDir = Path.Combine(dir, "CacheImages");
            if (Directory.Exists(cacheDir))
            {
                foreach (var file in Directory.GetFiles(cacheDir))
                {
                    File.Delete(file);
                }
            }

            if (!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }

            _inspectionResult = new InspectionResult();

            if (_image != null)
                _image.Dispose();
            _image = image;

            return this;
        }

        public static InspectionSchema GetInspectionSchema()
        {
            var dir = typeof (InspectionController).Assembly.GetAssemblyDirectoryPath();

            var inspectionSchemaDirPath = dir + "\\InspectionSchema";
            var inspectionSchemaFilePath = dir + "\\InspectionSchema\\InspectionSchema.xaml";

            if (!Directory.Exists(inspectionSchemaDirPath))
            {
                Directory.CreateDirectory(inspectionSchemaDirPath);
            }

            if (!File.Exists(inspectionSchemaFilePath))
            {
                var ds = InspectionSchemaFactory.CreateDefaultSchema();
                ds.SerializeToXamlFile(inspectionSchemaFilePath);
            }


            InspectionSchema schema;
            try
            {
                schema = inspectionSchemaFilePath.DeserializeFromXamlFile<InspectionSchema>();
            }
            catch (Exception e)
            {
                throw;
            }

            var files = Directory.GetFiles(inspectionSchemaDirPath);
            foreach (var file in files)
            {
                if (file == inspectionSchemaFilePath)
                    continue;

                var slaveSchema = file.DeserializeFromXamlFile<InspectionSchema>();
                if (!slaveSchema.Disabled)
                    schema.Merge(slaveSchema);
            }

            return schema;
        }

        public ISetImage SetInspectionSchema(string fileName = null)
        {
            var inspectionSchema = string.IsNullOrEmpty(fileName)
                ? GetInspectionSchema()
                : fileName.DeserializeFromXamlFile<InspectionSchema>();

            ((ISetInspectionSchema) this).SetInspectionSchema(inspectionSchema);

            return this;
        }

        public ISetImage SetInspectionSchema(InspectionSchema inspectionSchema)
        {
            _inspectionSchema = inspectionSchema;

            return this;
        }

        public InspectionController CreateCoordinate()
        {
            var sw = new NotifyStopwatch("InspectionController.CreateCoordinate.Inspect()");

            try
            {
                switch (_inspectionSchema.CoordinateType)
                {
                    case CoordinateType.Baseline:
                        var origin = _inspectionResult.CoordinateCircles[0];
                        var refCircle = _inspectionResult.CoordinateCircles[1];
                        _coordinate = new RelativeCoordinate(
                            origin.Circle.GetCenterPoint(),
                            refCircle.Circle.GetCenterPoint(),
                            refCircle.Definition.BaselineAngle);
                        break;
                    case CoordinateType.VectorsCenter:
                        var inspector =
                            InspectorFactory.CreateCircleInspector(_inspectionSchema.CircleSearching_InspectorName);
                        var searchCoordinateCircles = inspector.SearchCircles(_image, _inspectionSchema.CoordinateCircles);
                        _inspectionResult.CoordinateCircles = new CircleSearchingResultCollection(searchCoordinateCircles);
                        _coordinate = RelativeCoordinateFactory.CreateCoordinate(_inspectionResult.CoordinateCircles);
                        break;
                    case CoordinateType.NearOrigin:
                        throw new NotSupportedException("CoordinateType does not implement!");
                        break;
                    case CoordinateType.Boarder:
                        var inspector2 = InspectorFactory.CreateEdgeInspector(_inspectionSchema.EdgeSearching_InspectorName);
                        var searchCoordinateEdges = inspector2.SearchEdges(_image, _inspectionSchema.CoordinateEdges);
                        _inspectionResult.CoordinateEdges = new EdgeSearchingResultCollection(searchCoordinateEdges);
                        _coordinate =
                            RelativeCoordinateFactory.CreateCoordinateUsingBorder(_inspectionResult.CoordinateEdges);
                        break;
                    default:
                        throw new NotSupportedException("CoordinateType does not support!");
                }
            }
            catch (CreateCoordinateFailedException e)
            {
//                _inspectionResult
                throw;
            }
          

            if (_inspectionSchema.CoordinateOriginOffsetEnable)
            {
                _coordinate.OriginOffset = new Vector(_inspectionSchema.CoordinateOriginOffsetX,
                    _inspectionSchema.CoordinateOriginOffsetY);
            }

            _inspectionResult.CoordinateCircles.UpdateRelativeCoordinate(_coordinate);

            // 
            _inspectionSchema.CoordinateCircles.UpdateRelativeCoordinate(_coordinate);
            _inspectionSchema.CoordinateEdges.UpdateRelativeCoordinate(_coordinate);
            _inspectionSchema.CircleSearchingDefinitions.UpdateRelativeCoordinate(_coordinate);
            _inspectionSchema.EdgeSearchingDefinitions.UpdateRelativeCoordinate(_coordinate);
            _inspectionSchema.PartSearchingDefinitions.UpdateRelativeCoordinate(_coordinate);
            _inspectionSchema.SurfaceDefinitions.UpdateRelativeCoordinate(_coordinate);
            _inspectionSchema.RegionTargetDefinitions.UpdateRelativeCoordinate(_coordinate);

            sw.Dispose();

            return this;
        }

        public IInspectionController Inspect()
        {
            var sw2 = new NotifyStopwatch("IInspectionController.Inspect()");

            var inspectionSchema = _inspectionSchema;

            if (inspectionSchema.RegionTargetDefinitions.Any())
            {
                var inspector = new RegionTargetInspector();
                var results = inspector.SearchRegionTargets(_image, inspectionSchema.RegionTargetDefinitions);
                _inspectionResult.RegionTargets = results;
            }

            if (inspectionSchema.PartSearchingDefinitions.Any())
            {
                var partInspector = new PartInspector();
                var results= partInspector.SearchParts(_image, inspectionSchema.PartSearchingDefinitions);
                _inspectionResult.Parts = results;
            }

            if (inspectionSchema.CircleSearchingDefinitions.Any())
            {
                var sw = new NotifyStopwatch("SearchCircles()");
                var circles = InspectorFactory
                    .CreateCircleInspector(inspectionSchema.CircleSearching_InspectorName)
                    .SearchCircles(_image, inspectionSchema.CircleSearchingDefinitions);
                sw.Dispose();
                _inspectionResult.Circles = new CircleSearchingResultCollection(circles);
            }

            if (inspectionSchema.SurfaceDefinitions.Any())
            {
                IList<SurfaceResult> regionResults = null;

                using (new NotifyStopwatch("SearchSurfaces()"))
                    regionResults = InspectorFactory
                        .CreateSurfaceInspector(inspectionSchema.Surfaces_InspectorName)
                        .SearchSurfaces(_image, inspectionSchema.SurfaceDefinitions);

//                IList<DefectResult> defectResultCollection = null;
                if (inspectionSchema.DefectDefinitions.Any())
                {
                    var defectInspector = InspectorFactory.CreateDefectInspector(inspectionSchema.Defects_InspectorName);
                    var defectResultCollection = defectInspector.SearchDefects(_image, inspectionSchema.DefectDefinitions,
                        regionResults);
                    _inspectionResult.RegionDefectResults = defectResultCollection;
                    //                inspectionResult.ClosedRegionResults = regionResults;
                }
            }

            if (inspectionSchema.EdgeSearchingDefinitions.Any())
            {
                if (inspectionSchema.EdgeSearching_SaveCacheImage_Disabled)
                {
                    foreach (var def in inspectionSchema.EdgeSearchingDefinitions)
                    {
                        def.Domain_SaveCacheImageEnabled = false;
                        def.RegionExtractor_Disabled = false;
                        def.ImageFilter_SaveCacheImageEnabled = false;
                    }
                }

                var finalEdges = InspectorFactory
                    .CreateEdgeInspector(inspectionSchema.EdgeSearching_InspectorName)
                    .SearchEdges(_image, inspectionSchema.EdgeSearchingDefinitions);
                _inspectionResult.Edges = new EdgeSearchingResultCollection(finalEdges);


                int i = 0;
                foreach (var def in inspectionSchema.DistanceBetweenIntersectionPointsDefinitions)
                {
                    var line1 = finalEdges.Single(x => x.Name == def.Line1Name);
                    var line2 = finalEdges.Single(x => x.Name == def.Line2Name);

                    var line1Center = line1.Definition.Line.GetCenterPoint();
                    var line2Center = line2.Definition.Line.GetCenterPoint();

                    var linkLine = new Line(line1Center, line2Center);

                    var intersection1 = line1.EdgeLine.IntersectionWith(linkLine);
                    var intersection2 = line2.EdgeLine.IntersectionWith(linkLine);

                    if (Math.Abs(intersection1.X) < 0.00001 ||
                        Math.Abs(intersection2.X) < 0.00001)
                    {
                        Debug.WriteLine(@"DistanceBetweenIntersectionPointsDefinitions failed: {0}", def.Name);
                    }

                    //var distance = t1.ToVector() - t2.ToVector();
                    var distance = (intersection1 - intersection2).Length;

//                    Debug.WriteLine("Distance {0}: {1}\t", def.Name, distance);

                    var distanceBetweenPointsResult = new DistanceBetweenPointsResult()
                    {
                        Definition = def.DeepClone(),
                        Index = i,
                        Name = def.Name,
                        Point1 = intersection1,
                        Point2 = intersection2,
                        DistanceInPixel = (intersection1 - intersection2).Length,
                        DistanceInWorld =
                            (intersection1 - intersection2).Length
                            .ToMillimeterFromPixel(16),
                    };

                    _inspectionResult.DistanceBetweenPointsResults.Add(distanceBetweenPointsResult);

                    i++;
                }
            }

            sw2.Dispose();

            return this;
        }

        public IRelativeCoordinate Coordinate
        {
            get { return _coordinate; }
        }

        public HImage Image
        {
            get { return _image; }
        }

        public InspectionSchema InspectionSchema
        {
            get { return _inspectionSchema; }
        }

        public InspectionResult InspectionResult
        {
            get { return _inspectionResult; }
        }

        public IObservable<InspectionResult> CoordinateCreatedEvent
        {
            get { return _coordinateCreatedEvent; }
            set { _coordinateCreatedEvent = value; }
        }

        public void Dispose()
        {
            _image.Dispose();
        }

        IInspect ICreateCoordinate.CreateCoordinate()
        {
            return CreateCoordinate();
        }
    }
}