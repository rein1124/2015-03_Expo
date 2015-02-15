using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hdc.Mv.Inspection.Halcon.BatchInspector.Properties;
using Hdc.Serialization;

namespace Hdc.Mv.Inspection.Halcon.BatchInspector
{
    public class ReportManager
    {
        public static void SaveToCSV(List<CircleSearchingResultCollection> taskList, string dir, string suffix = null)
        {
            DateTime dateTime = DateTime.Now;
            var fileName = dateTime.ToString("yyyy-MM-dd_HH.mm.ss_") + suffix + "_GroupByWorkpiece" + ".csv";
            var fileFullName = Path.Combine(dir, fileName);

            SaveCsvGroupByWorkpiece(taskList, fileFullName);

            // 
            var fileName2 = dateTime.ToString("yyyy-MM-dd_HH.mm.ss_") + suffix + "_GroupByCircle" + ".csv";
            var fileFullName2 = Path.Combine(dir, fileName2);

            SaveCsvGroupByCircle(taskList, fileFullName2);
        }

        private static void SaveCsvGroupByWorkpiece(IList<CircleSearchingResultCollection> taskList, string fileFullName)
        {
            using (var csvWriterContainer = new CsvWriterContainer(fileFullName))
            {
                var writer = csvWriterContainer.CsvWriter;

                writer.WriteField("NO.");
                writer.WriteField("WPC NO.");
                writer.WriteField("Circle NO.");
                writer.WriteField("Circle Name");
                writer.WriteField("OBJ.X");
                writer.WriteField("OBJ.Y");
                writer.WriteField("OBJ.R_Inner");
                writer.WriteField("OBJ.R_Outer");
                writer.WriteField("ACT.X");
                writer.WriteField("ACT.Y");
                writer.WriteField("ACT.R");
                writer.WriteField("REL.X");
                writer.WriteField("REL.Y");
                writer.WriteField("REL.R");
                writer.NextRecord();

                for (int i = 0; i < taskList.Count; i++)
                {
                    var task = taskList[0];

                    for (int j = 0; j < task.Count; j++)
                    {
                        var circleDef = task[j].Definition;
                        var circleResult = task[j];

                        writer.WriteField(i * task.Count + j);
                        writer.WriteField(i);
                        writer.WriteField(j);
                        writer.WriteField(circleDef.Name);
                        writer.WriteField(circleDef.CenterX.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleDef.CenterY.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleDef.InnerRadius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleDef.OuterRadius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Circle.CenterX.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Circle.CenterY.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Circle.Radius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.RelativeCircle.CenterX.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.RelativeCircle.CenterY.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.RelativeCircle.Radius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField("mm");
                        writer.NextRecord();
                    }
                }
                csvWriterContainer.Dispose();
            }
        }

        private static void SaveCsvGroupByCircle(IList<CircleSearchingResultCollection> tasks, string fileName)
        {
            //            foreach (var task in tasks)
            //            {
            //                foreach (var t in task)
            //                {
            //                    Debug.WriteLine("t Circle " + t.Index + " X: " + t.Definition.CenterX);
            //                    Debug.WriteLine("t Circle " + t.Index + " Y: " + t.Definition.CenterY);
            //                }
            //            }

            using (var csvWriterContainer = new CsvWriterContainer(fileName))
            {
                var writer = csvWriterContainer.CsvWriter;


                var results = tasks.SelectMany(x => x);
                var resultsGroupBy = results.GroupBy(x => x.Index);

                int no = 0;
                foreach (IGrouping<int, CircleSearchingResult> group in resultsGroupBy)
                {
                    writer.WriteField("NO.");
                    writer.WriteField("WPC NO.");
                    writer.WriteField("Circle NO.");
                    writer.WriteField("Circle Name");
                    writer.WriteField("HasError");
                    writer.WriteField("IsNotFound");
                    writer.WriteField("EXP.X");
                    writer.WriteField("EXP.Y");
                    writer.WriteField("EXP.A");
                    writer.WriteField("OBJ.X");
                    writer.WriteField("OBJ.Y");
                    writer.WriteField("OBJ.RI");
                    writer.WriteField("OBJ.RO");
                    writer.WriteField("ACT.X");
                    writer.WriteField("ACT.Y");
                    writer.WriteField("ACT.R");
                    writer.WriteField("REL.X");
                    writer.WriteField("REL.Y");
                    writer.WriteField("REL.R");
                    writer.NextRecord();



                    int taskNo = 0;
                    foreach (var circleResult in @group)
                    {
                        Debug.WriteLine("Circle " + circleResult.Index + " X: " + circleResult.Definition.CenterX);
                        Debug.WriteLine("Circle " + circleResult.Index + " Y: " + circleResult.Definition.CenterY);

                        writer.WriteField(no);
                        writer.WriteField(taskNo);
                        writer.WriteField(circleResult.Index);
                        writer.WriteField(circleResult.Definition.Name);
                        writer.WriteField(circleResult.HasError);
                        writer.WriteField(circleResult.IsNotFound);
                        writer.WriteField(circleResult.Definition.BaselineX.ToNumbericString());
                        writer.WriteField(circleResult.Definition.BaselineY.ToNumbericString());
                        writer.WriteField(circleResult.Definition.BaselineAngle.ToNumbericString());
                        writer.WriteField(circleResult.Definition.CenterX.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Definition.CenterY.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Definition.InnerRadius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Definition.OuterRadius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Circle.CenterX.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Circle.CenterY.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Circle.Radius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.RelativeCircle.CenterX.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.RelativeCircle.CenterY.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.RelativeCircle.Radius.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField("mm");
                        writer.NextRecord();

                        no++;
                        taskNo++;
                    }
                    writer.NextRecord();

                    // STDEV
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.NextRecord();

                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField(@group.Select(x => x.Definition.CenterX).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.CenterY).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.InnerRadius).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.OuterRadius).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Circle.CenterX).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Circle.CenterY).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Circle.Radius).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.RelativeCircle.CenterX).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.RelativeCircle.CenterY).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.RelativeCircle.Radius).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField("um");


                    writer.NextRecord();
                    writer.NextRecord();

                    // Average
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.NextRecord();

                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField(@group.Select(x => x.Definition.CenterX).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.CenterY).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.InnerRadius).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.OuterRadius).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Circle.CenterX).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Circle.CenterY).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Circle.Radius).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.RelativeCircle.CenterX).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.RelativeCircle.CenterY).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.RelativeCircle.Radius).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField("mm");
                    writer.NextRecord();
                    writer.NextRecord();

                    for (int i = 0; i < 20; i++)
                    {
                        writer.WriteField("----------------");
                    }

                    writer.NextRecord();
                }
                csvWriterContainer.Dispose();
            }
        }

        private static void SaveCsvGroupByEdge(IList<EdgeSearchingResultCollection> tasks, string fileName)
        {
            using (var csvWriterContainer = new CsvWriterContainer(fileName))
            {
                var writer = csvWriterContainer.CsvWriter;

                var results = tasks.SelectMany(x => x);
                var resultsGroupBy = results.GroupBy(x => x.Index);

                int no = 0;
                foreach (IGrouping<int, EdgeSearchingResult> group in resultsGroupBy)
                {
                    writer.WriteField("NO.");
                    writer.WriteField("WPC NO.");
                    writer.WriteField("Edge NO.");
                    writer.WriteField("Edge Name");
                    writer.WriteField("HasError");
                    writer.WriteField("IsNotFound");
                    writer.WriteField("Line.X1");
                    writer.WriteField("Line.Y1");
                    writer.WriteField("Line.X2");
                    writer.WriteField("Line.Y2");
                    writer.WriteField("EdgeLine.X1");
                    writer.WriteField("EdgeLine.Y1");
                    writer.WriteField("EdgeLine.X2");
                    writer.WriteField("EdgeLine.Y2");
                    writer.WriteField("IntersectionPoint.X");
                    writer.WriteField("IntersectionPoint.Y");
                    writer.NextRecord();

                    int taskNo = 0;
                    foreach (var circleResult in @group)
                    {
                        writer.WriteField(no);
                        writer.WriteField(taskNo);
                        writer.WriteField(circleResult.Index);
                        writer.WriteField(circleResult.Name);
                        writer.WriteField(circleResult.HasError);
                        writer.WriteField(circleResult.IsNotFound);
                        writer.WriteField(circleResult.Definition.RelativeLine.X1.ToNumbericString());
                        writer.WriteField(circleResult.Definition.RelativeLine.Y1.ToNumbericString());
                        writer.WriteField(circleResult.Definition.RelativeLine.X2.ToNumbericString());
                        writer.WriteField(circleResult.Definition.RelativeLine.Y2.ToNumbericString());
                        writer.WriteField(circleResult.Definition.Line.X1.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Definition.Line.Y1.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Definition.Line.X2.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Definition.Line.Y2.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.EdgeLine.X1.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.EdgeLine.Y1.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.EdgeLine.X2.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.EdgeLine.Y2.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.IntersectionPoint.X.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.IntersectionPoint.Y.ToNumbericStringInMillimeterFromPixel(16));
                        writer.NextRecord();

                        no++;
                        taskNo++;
                    }

                    writer.WriteField("NO.");
                    writer.WriteField("Edge NO.");
                    writer.WriteField("Edge Name");
                    writer.WriteField("HasError");
                    writer.WriteField("IsNotFound");
                    writer.WriteField("RelativeLine.X1");
                    writer.WriteField("RelativeLine.Y1");
                    writer.WriteField("RelativeLine.X2");
                    writer.WriteField("RelativeLine.Y2");
                    writer.WriteField("Line.X1");
                    writer.WriteField("Line.Y1");
                    writer.WriteField("Line.X2");
                    writer.WriteField("Line.Y2");
                    writer.WriteField("EdgeLine.X1");
                    writer.WriteField("EdgeLine.Y1");
                    writer.WriteField("EdgeLine.X2");
                    writer.WriteField("EdgeLine.Y2");
                    writer.WriteField("IntersectionPoint.X");
                    writer.WriteField("IntersectionPoint.Y");
                    writer.NextRecord();

                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField(@group.Select(x => x.Definition.Line.X1).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.Line.Y1).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.Line.X2).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Definition.Line.Y2).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.EdgeLine.X1).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.EdgeLine.Y1).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.EdgeLine.X2).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.EdgeLine.Y2).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.IntersectionPoint.X).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.IntersectionPoint.Y).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.NextRecord();

                    writer.NextRecord();
                    writer.NextRecord();
                }
                csvWriterContainer.Dispose();
            }
        }

        public static void SaveToXaml(IEnumerable<CircleSearchingResultCollection> tasks, string dir,
                                      string suffix = null)
        {
            var report = new InspectionReport();

            foreach (var task in tasks)
            {
                report.Results.Add(new InspectionResult()
                                   {
                                       Circles = task,
                                   });
            }

            DateTime dateTime = DateTime.Now;
            var fileName = dateTime.ToString("yyyy-MM-dd_HH.mm.ss_") + suffix + "" + ".xaml";
            //            var reportsDir = Path.Combine(dir, "_reports" + dateTime.ToString("_yyyy-MM-dd_HH.mm.ss"));
            var fileFullName = Path.Combine(dir, fileName);
            report.SerializeToXamlFile(fileFullName);
        }


        public static void SaveToXaml(IEnumerable<InspectionResult> tasks, string dir,
                                      string suffix = null)
        {
            var report = new InspectionReport();

            foreach (var task in tasks)
            {
                report.Results.Add(task);
            }

            DateTime dateTime = DateTime.Now;
            var fileName = dateTime.ToString("yyyy-MM-dd_HH.mm.ss_") + suffix + "" + ".xaml";
            //            var reportsDir = Path.Combine(dir, "_reports" + dateTime.ToString("_yyyy-MM-dd_HH.mm.ss"));
            var fileFullName = Path.Combine(dir, fileName);
            report.SerializeToXamlFile(fileFullName);
        }

        public static void SaveCsvGroupByEdge(IList<DistanceBetweenPointsResultCollection> tasks, string dir, string suffix = null)
        {
            DateTime dateTime = DateTime.Now;
            var fileName = dateTime.ToString("yyyy-MM-dd_HH.mm.ss_") + suffix + "_GroupByEdge" + ".csv";
            var fileFullName = Path.Combine(dir, fileName);

            SaveCsvGroupByEdge(tasks, fileFullName);

        }

        private static void SaveCsvGroupByEdge(IList<DistanceBetweenPointsResultCollection> tasks, string fileName)
        {
            using (var csvWriterContainer = new CsvWriterContainer(fileName))
            {
                var writer = csvWriterContainer.CsvWriter;


                var results = tasks.SelectMany(x => x);
                var resultsGroupBy = results.GroupBy(x => x.Index);

                int no = 0;
                foreach (IGrouping<int, DistanceBetweenPointsResult> group in resultsGroupBy)
                {
                    writer.WriteField("NO.");
                    writer.WriteField("WPC NO.");
                    writer.WriteField("OBJ NO.");
                    writer.WriteField("Name");
                    writer.WriteField("HasError");
                    writer.WriteField("IsNotFound");
                    writer.WriteField("P1.X");
                    writer.WriteField("P1.Y");
                    writer.WriteField("P2.X");
                    writer.WriteField("P2.Y");
                    writer.WriteField("Dist.Pixel");
                    writer.WriteField("Dist.World");
                    writer.NextRecord();

                    int taskNo = 0;
                    foreach (var circleResult in @group)
                    {
                        writer.WriteField(no);
                        writer.WriteField(taskNo);
                        writer.WriteField(circleResult.Index);
                        writer.WriteField(circleResult.Name);
                        writer.WriteField(circleResult.HasError);
                        writer.WriteField(circleResult.IsNotFound);
                        writer.WriteField(circleResult.Point1.X.ToNumbericStringInMicrometerFromPixel(16));
                        writer.WriteField(circleResult.Point1.Y.ToNumbericStringInMicrometerFromPixel(16));
                        writer.WriteField(circleResult.Point2.X.ToNumbericStringInMicrometerFromPixel(16));
                        writer.WriteField(circleResult.Point2.Y.ToNumbericStringInMicrometerFromPixel(16));
                        writer.WriteField(circleResult.DistanceInPixel);
                        writer.WriteField(circleResult.DistanceInPixel.ToNumbericStringInMicrometerFromPixel(16));
                        writer.NextRecord();

                        no++;
                        taskNo++;
                    }
                    writer.NextRecord();

                    // STDEV
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.WriteField("STDEV");
                    writer.NextRecord();

                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField(@group.Select(x => x.Point1.X).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Point1.Y).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Point2.X).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Point2.Y).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField(@group.Select(x => x.DistanceInPixel).CalculateStdDev());
                    writer.WriteField(@group.Select(x => x.DistanceInPixel).CalculateStdDev().ToNumbericStringInMicrometerFromPixel(16));
                    writer.WriteField("um");


                    writer.NextRecord();
                    writer.NextRecord();

                    // Average
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.WriteField("AVG");
                    writer.NextRecord();

                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField("");
                    writer.WriteField(@group.Select(x => x.Point1.X).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Point1.Y).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Point2.X).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.Point2.Y).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.DistanceInPixel).Average());
                    writer.WriteField(@group.Select(x => x.DistanceInPixel).Average().ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField("mm");
                    writer.NextRecord();
                    writer.NextRecord();

                    // Average
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
                    writer.WriteField("EXP");
                    writer.WriteField("POS");
                    writer.WriteField("NEG");
                    writer.WriteField("TOL");
                    writer.WriteField("MIN");
                    writer.WriteField("MAX");
                    writer.WriteField("RAG");
                    writer.WriteField("AVG");
                    writer.WriteField("Diff");
                    writer.WriteField("AbsDiff");
                    writer.WriteField("StdDev");
                    writer.NextRecord();

//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");
//                    writer.WriteField("");

                    double expectValue = @group.Select(x => x.Definition.ExpectValue).First();
                    double expectValueInPixel = expectValue*1000.0/16.0;
                    double posTol = @group.Select(x => x.Definition.PositiveTolerance).First();
                    double negTol = @group.Select(x => x.Definition.NegativeTolerance).First();
                    double tol = posTol - negTol;
                    double min = @group.Select(x => x.DistanceInPixel).Min();
                    double max = @group.Select(x => x.DistanceInPixel).Max();
                    double avg = @group.Select(x => x.DistanceInPixel).Average();

                    writer.WriteField(expectValue);
                    writer.WriteField(posTol);
                    writer.WriteField(negTol);
                    writer.WriteField(tol);
                    writer.WriteField(min.ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(max.ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField((max - min).ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(avg.ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField((avg - expectValueInPixel).ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField((Math.Abs(avg - expectValueInPixel)).ToNumbericStringInMillimeterFromPixel(16));
                    writer.WriteField(@group.Select(x => x.DistanceInPixel).CalculateStdDev().ToNumbericStringInMillimeterFromPixel(16));

                    writer.WriteField("mm");
                    writer.NextRecord();
                    writer.NextRecord();


                    //
                    for (int i = 0; i < 20; i++)
                    {
                        writer.WriteField("----------------");
                    }

                    writer.NextRecord();
                }
                csvWriterContainer.Dispose();
            }
        }

        public static void SaveDefectResultsToCsvGroupByWorkpiece(IList<IList<DefectResult>> taskList,
                                                                   string dir, string suffix = null)
        {
            DateTime dateTime = DateTime.Now;
            var fileName = dateTime.ToString("yyyy-MM-dd_HH.mm.ss_") + suffix + "_GroupByWorkpiece" + ".csv";
            var fileFullName = Path.Combine(dir, fileName);

            SaveDefectResultsToCsvGroupByWorkpiece(taskList, fileFullName);
        }

        private static void SaveDefectResultsToCsvGroupByWorkpiece(IList<DefectResultCollection> taskList, string fileFullName)
        {
            using (var csvWriterContainer = new CsvWriterContainer(fileFullName))
            {
                var writer = csvWriterContainer.CsvWriter;

                writer.WriteField("NO.");
                writer.WriteField("WPC NO.");
                writer.WriteField("Defect NO.");
                writer.WriteField("Index");
                writer.WriteField("TypeCode");
                writer.WriteField("Name");
                writer.WriteField("X");
                writer.WriteField("Y");
                writer.WriteField("Width");
                writer.WriteField("Height");
                writer.WriteField("Size");
                writer.WriteField("X.mm");
                writer.WriteField("Y.mm");
                writer.WriteField("Width.mm");
                writer.WriteField("Height.mm");
                writer.WriteField("Size.mm");
                writer.NextRecord();

                for (int i = 0; i < taskList.Count; i++)
                {
                    var task = taskList[0];

                    for (int j = 0; j < task.Count; j++)
                    {
                        var circleResult = task[j];

                        writer.WriteField(i * task.Count + j);
                        writer.WriteField(i);
                        writer.WriteField(j);
                        writer.WriteField(circleResult.Index);
                        writer.WriteField(circleResult.TypeCode);
                        writer.WriteField(circleResult.Name);
                        writer.WriteField(circleResult.X);
                        writer.WriteField(circleResult.Y);
                        writer.WriteField(circleResult.Width);
                        writer.WriteField(circleResult.Height);
                        writer.WriteField(circleResult.Size);
                        writer.WriteField(circleResult.X.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Y.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Width.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Height.ToNumbericStringInMillimeterFromPixel(16));
                        writer.WriteField(circleResult.Size * 256.0 / 1000.0 / 1000.0);
                        writer.WriteField("mm");
                        writer.NextRecord();
                    }

                    writer.NextRecord();
                }

                csvWriterContainer.Dispose();
            }
        }
    }
}