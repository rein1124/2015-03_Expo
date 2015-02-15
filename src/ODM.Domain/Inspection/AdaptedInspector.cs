using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Hdc.Mv;
using Hdc.Mv.Inspection;
using Hdc.Mv.Inspection.Mil;
using Hdc.Mvvm.Dialogs;
using Hdc.Reflection;
using Hdc.Serialization;
using ODM.Domain.Configs;

namespace ODM.Domain.Inspection
{

    public class AdaptedInspector : IInspector
    {
        public IGeneralInspector GeneralInspector { get; private set; }

        public MachineConfig MachineConfig { get; set; }

        private InspectionSchema _schema;

        public void Dispose()
        {
            GeneralInspector.Dispose();
        }

        public bool Init()
        {
            _schema = GetInspectionSchema();

            switch (_schema.InspectorName)
            {
                case "Sim":
                    {
                        var sim = new SimGeneralInspector();
                        GeneralInspector = sim;
                    }
                    break;
                case "Mil":
                    {
                        var mi = new MilGeneralInspector();
                        int imageWidth = MachineConfig.MV_LineScanFrameWidth;
                        int imageHeight = MachineConfig.MV_LineScanFrameHeight * MachineConfig.MV_LineScanMosaicCount;

                        mi.Init(imageWidth, imageHeight);

                        GeneralInspector = mi;
                    }
                    break;
                case "Hal":
                    {
                        var hi = new HalconGeneralInspector();
                        GeneralInspector = hi;
                    }
                    break;
                default:
                    throw new NotSupportedException("InspectionSchema.InspectorName not be set!");
            }


            return true;
        }

        public bool LoadParameters()
        {
            return true;
        }

        public void FreeObject()
        {
            GeneralInspector.Dispose();
        }

        private double c2SumX, c3SumX, c4SumX, c5SumX;
        private double c2SumY, c3SumY, c4SumY, c5SumY;
        private double c2SumXDiffer, c3SumXDiffer, c4SumXDiffer, c5SumXDiffer;
        private double c2SumYDiffer, c3SumYDiffer, c4SumYDiffer, c5SumYDiffer;
        private double counter;

//        private List<Hdc.Mv.Inspection.MeasurementInfo> c1MeasurementInfos = new List<Hdc.Mv.Inspection.MeasurementInfo>();
        private List<Hdc.Mv.Inspection.MeasurementInfo> c2MeasurementInfos = new List<Hdc.Mv.Inspection.MeasurementInfo>();
        private List<Hdc.Mv.Inspection.MeasurementInfo> c3MeasurementInfos = new List<Hdc.Mv.Inspection.MeasurementInfo>();
        private List<Hdc.Mv.Inspection.MeasurementInfo> c4MeasurementInfos = new List<Hdc.Mv.Inspection.MeasurementInfo>();
        private List<Hdc.Mv.Inspection.MeasurementInfo> c5MeasurementInfos = new List<Hdc.Mv.Inspection.MeasurementInfo>();
//        private List<Hdc.Mv.Inspection.MeasurementInfo> c5MeasurementInfos = new List<Hdc.Mv.Inspection.MeasurementInfo>();

        public InspectionInfo Inspect(Hdc.Mv.ImageInfo imageInfo)
        {
            var newSchema = GetInspectionSchema();
            GeneralInspector.ImportInspectionSchema(newSchema);
            GeneralInspector.Inspect(imageInfo);

            var inspectionInfo = new InspectionInfo();
            var originAResult = GeneralInspector.CircleSearchingResults[0];
            var originBResult = GeneralInspector.CircleSearchingResults[1];
            var baseLine = new Line(originAResult.Circle.GetCenterPoint(), originBResult.Circle.GetCenterPoint());
            var baseLineAngle = 90 - 56;

            for (int i = 1; i < GeneralInspector.CircleSearchingDefinitions.Count; i++)
            {
                var circleSearchingResult = GeneralInspector.CircleSearchingResults[i];
                if (!circleSearchingResult.HasError)
                {
                    var measurement = new Hdc.Mv.Inspection.MeasurementInfo()
                                      {
                                          StartPointX = originAResult.Circle.CenterX,
                                          StartPointY = originAResult.Circle.CenterY,
                                          EndPointX = circleSearchingResult.Circle.CenterX,
                                          EndPointY = circleSearchingResult.Circle.CenterY,
                                      };

                    Point targetPoint = circleSearchingResult.Circle.GetCenterPoint();
                    var relativePoint = targetPoint.GetRelativePoint(baseLine, baseLineAngle);
                    var length = relativePoint.ToVector().Length;

                    //                    measurement.StartPointXActualValue = relativePoint.X;
                    //                    measurement.StartPointXActualValue = relativePoint.X;
                    measurement.Index = i;
                    measurement.GroupIndex = i;
                    measurement.Value = length;
                    measurement.StartPointXActualValue = relativePoint.X * 16 / 1000.0;
                    measurement.StartPointYActualValue = relativePoint.Y * 16 / 1000.0;
                    //                                measurement.EndPointXActualValue = relativePoint.X * 16 / 1000.0;
                    //                                measurement.EndPointYActualValue = relativePoint.Y * 16 / 1000.0;
                    measurement.ValueActualValue = length * 16 / 1000.0;
                    measurement.ValueActualValue = circleSearchingResult.Circle.Radius * 16 / 1000.0;

                    counter++;

//                    DateTime dateTime = DateTime.Now;
//                    Debug.WriteLine("begin -------------" + dateTime + "--------------");

                    switch (i)
                    {
                        case 1:
                            measurement.EndPointXActualValue = measurement.StartPointXActualValue - 31.85;
                            measurement.EndPointYActualValue = measurement.StartPointYActualValue - (-21.29);
                            c2MeasurementInfos.Add(measurement);
                            break;
                        case 2:
                            measurement.EndPointXActualValue = measurement.StartPointXActualValue - 40.77;
                            measurement.EndPointYActualValue = measurement.StartPointYActualValue - (-44.45);
                            c3MeasurementInfos.Add(measurement);

                            break;
                        case 3:
                            measurement.EndPointXActualValue = measurement.StartPointXActualValue - 42.43;
                            measurement.EndPointYActualValue = measurement.StartPointYActualValue - 83.06;
                            c4MeasurementInfos.Add(measurement);

                            break;
                        case 4:
                            measurement.EndPointXActualValue = measurement.StartPointXActualValue - (-17.28);
                            measurement.EndPointYActualValue = measurement.StartPointYActualValue - 84.04;
                            c5MeasurementInfos.Add(measurement);

                            break;
                    }

//                    Debug.WriteLine("end -------------" + dateTime + "--------------");

                    inspectionInfo.MeasurementInfos.Add(measurement);
                }
                else
                {
                    //                    innerCircle.Stroke = Brushes.Red;
                    //                    outerCircle.Stroke = Brushes.Red;
                }


            }

            DateTime dateTime = DateTime.Now;
            Debug.WriteLine("begin -------------" + dateTime + "--------------");

            Debug.WriteLine("C2 ---------------------");
            OutputResults(c2MeasurementInfos);
            Debug.WriteLine("C3 ---------------------");
            OutputResults(c3MeasurementInfos);
            Debug.WriteLine("C4 ---------------------");
            OutputResults(c4MeasurementInfos);
            Debug.WriteLine("C5 ---------------------");
            OutputResults(c5MeasurementInfos);

            Debug.WriteLine("end -------------" + dateTime + "--------------");

            foreach (var searchingResult in GeneralInspector.CircleSearchingResults)
            {
                Debug.WriteLine(@"CircleSearchingResults: x={0}, y={1}", searchingResult.Circle.CenterX, searchingResult.Circle.CenterY);
            }


            return inspectionInfo;
        }

        void OutputResults(IList<Hdc.Mv.Inspection.MeasurementInfo> mis)
        {
            if (mis.IsEmpty()) return;
            var xAverage = mis.Average(x => x.StartPointXActualValue);
            var yAverage = mis.Average(x => x.StartPointYActualValue);

            var xDiffer = mis.Last().StartPointXActualValue - xAverage;
            var yDiffer = mis.Last().StartPointYActualValue - yAverage;

            var xDifferAvg = mis.Select(x => Math.Abs(x.StartPointXActualValue - xAverage)).Average();
            var yDifferAvg = mis.Select(x => Math.Abs(x.StartPointYActualValue - yAverage)).Average();

            var xDifferSqrt = Math.Sqrt(mis.Select(x => Math.Pow(x.StartPointXActualValue - xAverage, 2)).Average());
            var yDifferSqrt = Math.Sqrt(mis.Select(x => Math.Pow(x.StartPointYActualValue - yAverage, 2)).Average());

            var xMax = mis.Max(x => x.StartPointXActualValue);
            var yMax = mis.Max(x => x.StartPointYActualValue);
            var xMin = mis.Min(x => x.StartPointXActualValue);
            var yMin = mis.Min(x => x.StartPointYActualValue);

            var xDifferMax = xMax - xAverage;
            var yDifferMax = yMax - yAverage;
            var xDifferMin = xMin - xAverage;
            var yDifferMin = yMin - yAverage;

            double xDiffer2 = (Math.Abs(xDifferMax) > Math.Abs(xDifferMin) ? xDifferMax : xDifferMin);
            double yDiffer2 = (Math.Abs(yDifferMax) > Math.Abs(yDifferMin) ? yDifferMax : yDifferMin);

            var xActualDifferAverage = mis.Average(x => x.EndPointXActualValue);
            var yActualDifferAverage = mis.Average(x => x.EndPointYActualValue);

            var xActualDifferDiffer = mis.Last().EndPointXActualValue - xActualDifferAverage;
            var yActualDifferDiffer = mis.Last().EndPointYActualValue - yActualDifferAverage;

            var xActualDifferDifferAvg = mis.Select(x => Math.Abs(x.EndPointXActualValue - xActualDifferAverage)).Average();
            var yActualDifferDifferAvg = mis.Select(x => Math.Abs(x.EndPointYActualValue - yActualDifferAverage)).Average();

            Debug.WriteLine("X AVG = " + (xAverage *1000).ToString("F3"));
            Debug.WriteLine("Y AVG = " + (yAverage * 1000).ToString("F3"));
            Debug.WriteLine("X Differ = " + (xDiffer * 1000).ToString("F3"));
            Debug.WriteLine("Y Differ = " + (yDiffer * 1000).ToString("F3"));
            Debug.WriteLine("X Differ AVG = " + (xDifferAvg * 1000).ToString("F3"));
            Debug.WriteLine("Y Differ AVG = " + (yDifferAvg * 1000).ToString("F3"));
            Debug.WriteLine("X Differ Sqrt = " + (xDifferSqrt * 1000).ToString("F3"));
            Debug.WriteLine("Y Differ Sqrt = " + (yDifferSqrt * 1000).ToString("F3"));
            //            Debug.WriteLine("X Differ Max = " + xDiffer2);
            //            Debug.WriteLine("Y Differ Max = " + yDiffer2);

            Debug.WriteLine("X Actual Differ AVG = " + (xActualDifferAverage * 1000).ToString("F3"));
            Debug.WriteLine("Y Actual Differ AVG = " + (yActualDifferAverage * 1000).ToString("F3"));
//            Debug.WriteLine("X Actual Differ Differ = " + (xActualDifferDiffer * 1000).ToString("F3"));
//            Debug.WriteLine("Y Actual Differ Differ = " + (yActualDifferDiffer * 1000).ToString("F3"));
//            Debug.WriteLine("X Actual Differ Differ AVG= " + (xActualDifferDifferAvg * 1000).ToString("F3"));
//            Debug.WriteLine("Y Actual Differ Differ AVG= " + (yActualDifferDifferAvg * 1000).ToString("F3"));
        }

        private InspectionSchema GetInspectionSchema()
        {
            var dir = this.GetType().Assembly.GetAssemblyDirectoryPath();
            var fileName = Path.Combine(dir, "InspectionSchema.xaml");
            if (!File.Exists(fileName))
            {
                var ds = InspectionSchemaFactory.CreateDefaultSchema();
                ds.SerializeToXamlFile(fileName);
            }
            var schema = fileName.DeserializeFromXamlFile<InspectionSchema>();
            return schema;
        }
    }
}