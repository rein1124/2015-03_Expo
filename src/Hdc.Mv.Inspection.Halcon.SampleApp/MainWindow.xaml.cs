using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hdc.Collections.Generic;
using Hdc.Collections.ObjectModel;
using Hdc.Diagnostics;
using Hdc.Mv;
using Hdc.Mv.Halcon;
using Hdc.Mv.Inspection;
using Hdc.Mv.Inspection.Halcon;
using Hdc.Mv.Inspection.Halcon.SampleApp;
using Hdc.Mv.Inspection.Halcon.SampleApp.Annotations;
using Hdc.Reflection;
using Hdc.Serialization;
using Hdc.Windows.Media.Imaging;
using MeasurementTestApp;
using Microsoft.Win32;
using Line = Hdc.Mv.Line;
using Path = System.IO.Path;

// ReSharper disable InconsistentNaming

namespace ODM.Inspectors.Halcon.SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<RegionIndicatorViewModel> RegionIndicators { get; set; }
        public BindableCollection<RectangleIndicatorViewModel> DefectIndicators { get; set; }
        public ObservableCollection<RectangleIndicatorViewModel> ObjectIndicators { get; set; }
        public ObservableCollection<LineIndicatorViewModel> LineIndicators { get; set; }
        public ObservableCollection<CircleIndicatorViewModel> CircleIndicators { get; set; }

        private InspectionController InspectionController;

        public MainWindow()
        {
            InitializeComponent();

            RegionIndicators = new ObservableCollection<RegionIndicatorViewModel>();
            DefectIndicators = new BindableCollection<RectangleIndicatorViewModel>();
            ObjectIndicators = new ObservableCollection<RectangleIndicatorViewModel>();
            LineIndicators = new ObservableCollection<LineIndicatorViewModel>();
            CircleIndicators = new ObservableCollection<CircleIndicatorViewModel>();

            this.DataContext = this;
            this.Closing += MainWindow_Closing;

            InspectionController = new InspectionController();

            Refresh();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            InspectionController.Dispose();
        }

        private void Inspect(InspectionSchema schema)
        {
            //IndicatorViewer.Loaded += (sender, args) => IndicatorViewer.ZoomFit();
            //                        IndicatorViewer.Loaded += (sender, args) => IndicatorViewer.ZoomOut();

            RegionIndicators.Clear();
            LineIndicators.Clear();
            CircleIndicators.Clear();
            DefectIndicators.Clear();
            ObjectIndicators.Clear();

            BitmapSource bs;

            try
            {
                bs = new BitmapImage(new Uri(schema.TestImageFilePath, UriKind.RelativeOrAbsolute));
            }
            catch (FileNotFoundException e)
            {
                throw new HalconInspectorException("Image file not exist", e);
            }

            if (Math.Abs(bs.DpiX - 96) > 0.00001 || Math.Abs(bs.DpiY - 96) > 0.00001)
            {
                var sw1 = new NotifyStopwatch("BitmapSource convert to Dpi96");
                BitmapSourceInfo bsi = bs.ToGray8BppBitmapSourceInfo();
                bsi.DpiX = 96;
                bsi.DpiY = 96;
                var bitmapSourceDpi96 = bsi.GetBitmapSource();
                bs = bitmapSourceDpi96;
                sw1.Stop();
                sw1.Dispose();
            }

            IndicatorViewer.BitmapSource = bs;

            var sw2 = new NotifyStopwatch("BitmapSource.ToHImage()");
            var hImage = bs.ToHImage();
            sw2.Stop();
            sw2.Dispose();


            try
            {
                var sw = new NotifyStopwatch("InspectionController.Inspect()");
                InspectionController
                    .SetInspectionSchema()
                    .SetImage(hImage)
                    .CreateCoordinate()
                    .Inspect()
                    ;
                sw.Stop();
                sw.Dispose();

                // CoordinateCircles
                Show_CircleSearchingDefinitions(
                    InspectionController.InspectionResult.GetCoordinateCircleSearchingDefinitions());
                Show_CircleSearchingResults(InspectionController.InspectionResult.CoordinateCircles);

                // Circles
                Show_CircleSearchingDefinitions(
                    InspectionController.InspectionResult.GetCircleSearchingDefinitions(), Brushes.Orange);
                Show_CircleSearchingResults(InspectionController.InspectionResult.Circles, Brushes.DodgerBlue);

                // CoordinateEdges
                Show_EdgeSearchingDefinitions(InspectionController.InspectionResult.GetCoordinateEdges());
                Show_EdgeSearchingResults(InspectionController.InspectionResult.CoordinateEdges);

                // Edges
                Show_EdgeSearchingDefinitions(InspectionController.InspectionResult.GetEdgeSearchingDefinitions());
                Show_EdgeSearchingResults(InspectionController.InspectionResult.Edges);

                // DistanceBetweenPoints
                Show_DistanceBetweenPointsResults(InspectionController.InspectionResult.DistanceBetweenPointsResults);

                // Defects
                Show_DefectResults(InspectionController.InspectionResult.RegionDefectResults);

                // Parts
                Show_PartSearchingDefinitions(InspectionController.InspectionResult.GetPartSearchingDefinitions());
                Show_PartSearchingResults(InspectionController.InspectionResult.Parts);

                // RegionTargets
                Show_RegionTargetDefinitions(InspectionController.InspectionResult.GetRegionTargetDefinitions());
                Show_RegionTargetResults(InspectionController.InspectionResult.RegionTargets);
            }
            catch (CreateCoordinateFailedException e)
            {
                Show_CircleSearchingDefinitions(
                    InspectionController.InspectionResult.GetCoordinateCircleSearchingDefinitions());
                Show_CircleSearchingResults(InspectionController.InspectionResult.CoordinateCircles);

                Show_EdgeSearchingDefinitions(InspectionController.InspectionResult.GetCoordinateEdges());
                Show_EdgeSearchingResults(InspectionController.InspectionResult.CoordinateEdges);

                MessageBox.Show(e.Message);
            }
        }

        public void Show_DistanceBetweenLinesResults(
            IEnumerable<DistanceBetweenLinesResult> distanceBetweenLinesResults)
        {
            foreach (var result in distanceBetweenLinesResults)
            {
                var distanceLineIndicator = new LineIndicatorViewModel
                                            {
                                                StartPointX = result.FootPoint1.X,
                                                StartPointY = result.FootPoint1.Y,
                                                EndPointX = result.FootPoint2.X,
                                                EndPointY = result.FootPoint2.Y,
                                                Stroke = Brushes.Lime,
                                                StrokeThickness = 2,
                                                StrokeDashArray = new DoubleCollection() {2, 2},
                                            };
                LineIndicators.Add(distanceLineIndicator);
            }
        }

        public void Show_DistanceBetweenPointsResults(
            DistanceBetweenPointsResultCollection pointsResultCollection)
        {
            foreach (var result in pointsResultCollection)
            {
                var distanceLineIndicator = new LineIndicatorViewModel
                                            {
                                                StartPointX = result.Point1.X,
                                                StartPointY = result.Point1.Y,
                                                EndPointX = result.Point2.X,
                                                EndPointY = result.Point2.Y,
                                                Stroke = Brushes.Lime,
                                                StrokeThickness = 2,
                                                StrokeDashArray = new DoubleCollection() {2, 2},
                                            };
                LineIndicators.Add(distanceLineIndicator);
            }
        }

        public void Show_CircleSearchingResults(IList<CircleSearchingResult> CircleSearchingResults, Brush brush = null)
        {
            if (brush == null)
                brush = Brushes.Lime;

            foreach (var result in CircleSearchingResults)
            {
                var ci = new CircleIndicatorViewModel()
                         {
                             CenterX = result.Circle.CenterX,
                             CenterY = result.Circle.CenterY,
                             Radius = result.Circle.Radius,
                             Stroke = brush,
                             StrokeThickness = 2,
                             StrokeDashArray = null,
                         };
                if (result.IsNotFound)
                    ci.Stroke = Brushes.Red;

                CircleIndicators.Add(ci);



                if (result.Definition.Diameter_DisplayEnabled)
                {
                    var line = result.Circle.GetLine(45);

                    var lineIndicator = new LineIndicatorViewModel
                    {
                        StartPointX = line.X1,
                        StartPointY = line.Y1,
                        EndPointX = line.X2,
                        EndPointY = line.Y2,
                        Stroke = Brushes.DeepPink,
                        StrokeThickness = 2,
                    };
                    LineIndicators.Add(lineIndicator);
                }
            }

            if (CircleSearchingResults.Count >= 2)
            {
                var Circle1CenterX = CircleSearchingResults[0].Circle.CenterX;
                var Circle1CenterY = CircleSearchingResults[0].Circle.CenterY;
                var vector1 = new Vector(Circle1CenterX, Circle1CenterY);


                for (int i = 1; i < CircleSearchingResults.Count; i++)
                {
                    var Circle2CenterX = CircleSearchingResults[i].Circle.CenterX;
                    var Circle2CenterY = CircleSearchingResults[i].Circle.CenterY;

                    var vector2 = new Vector(Circle2CenterX, Circle2CenterY);

                    var diff = vector1 - vector2;
                    var distance = diff.Length;
                    var DistanceBetweenC1C2 = distance;


                    var distanceLineIndicator = new LineIndicatorViewModel
                                                {
                                                    StartPointX = Circle1CenterX,
                                                    StartPointY = Circle1CenterY,
                                                    EndPointX = Circle2CenterX,
                                                    EndPointY = Circle2CenterY,
                                                    Stroke = brush,
                                                    StrokeThickness = 2,
                                                    StrokeDashArray = new DoubleCollection() {4, 4},
                                                };
                    LineIndicators.Add(distanceLineIndicator);
                }
            }
        }

        public void Show_CircleSearchingDefinitions(IEnumerable<CircleSearchingDefinition> circleSearchingDefinitions,
                                                    Brush brush = null)
        {
            if (brush == null)
                brush = Brushes.Green;

            foreach (var circleSearchingDefinition in circleSearchingDefinitions)
            {
                var innerCircle = new CircleIndicatorViewModel()
                                  {
                                      CenterX = circleSearchingDefinition.CenterX,
                                      CenterY = circleSearchingDefinition.CenterY,
                                      Radius = circleSearchingDefinition.InnerRadius,
                                      Stroke = brush,
                                      StrokeThickness = 1,
                                      StrokeDashArray = new DoubleCollection() {4, 4},
                                  };
                CircleIndicators.Add(innerCircle);
                var outerCircle = new CircleIndicatorViewModel()
                                  {
                                      CenterX = circleSearchingDefinition.CenterX,
                                      CenterY = circleSearchingDefinition.CenterY,
                                      Radius = circleSearchingDefinition.OuterRadius,
                                      Stroke = brush,
                                      StrokeThickness = 1,
                                      StrokeDashArray = new DoubleCollection() {4, 4},
                                  };
                CircleIndicators.Add(outerCircle);
            }
        }

        public void Show_EdgeSearchingDefinitions(IEnumerable<EdgeSearchingDefinition> edgeSearchingDefinitions)
        {
            foreach (var ed in edgeSearchingDefinitions)
            {
                var regionIndicator = new RegionIndicatorViewModel
                                      {
                                          StartPointX = ed.StartX,
                                          StartPointY = ed.StartY,
                                          EndPointX = ed.EndX,
                                          EndPointY = ed.EndY,
                                          RegionWidth = ed.ROIWidth,
                                          Stroke = Brushes.Orange,
                                          StrokeThickness = 4,
                                          IsHidden = false,
                                      };
                RegionIndicators.Add(regionIndicator);
            }
        }

        public void Show_PartSearchingDefinitions(IEnumerable<PartSearchingDefinition> partSearchingDefinitions)
        {
            foreach (var ed in partSearchingDefinitions)
            {
                var regionIndicator = new RegionIndicatorViewModel
                                      {
                                          StartPointX = ed.RoiLine.X1,
                                          StartPointY = ed.RoiLine.Y1,
                                          EndPointX = ed.RoiLine.X2,
                                          EndPointY = ed.RoiLine.Y2,
                                          RegionWidth = ed.RoiHalfWidth,
                                          Stroke = Brushes.Orange,
                                          StrokeThickness = 4,
                                          IsHidden = false,
                                      };
                RegionIndicators.Add(regionIndicator);

                var regionIndicator2 = new RegionIndicatorViewModel
                                       {
                                           StartPointX = ed.AreaLine.X1,
                                           StartPointY = ed.AreaLine.Y1,
                                           EndPointX = ed.AreaLine.X2,
                                           EndPointY = ed.AreaLine.Y2,
                                           RegionWidth = ed.AreaHalfWidth,
                                           Stroke = Brushes.Orange,
                                           StrokeThickness = 4,
                                           IsHidden = false,
                                       };
                RegionIndicators.Add(regionIndicator2);
            }
        }

        public void Show_EdgeSearchingResults(IEnumerable<EdgeSearchingResult> edgeSearchingResults)
        {
            foreach (var edgeSearchingResult in edgeSearchingResults)
            {
                if (edgeSearchingResult.HasError) continue;

                var lineIndicator = new LineIndicatorViewModel
                                    {
                                        StartPointX = edgeSearchingResult.EdgeLine.X1,
                                        StartPointY = edgeSearchingResult.EdgeLine.Y1,
                                        EndPointX = edgeSearchingResult.EdgeLine.X2,
                                        EndPointY = edgeSearchingResult.EdgeLine.Y2,
                                        Stroke = Brushes.Lime,
                                        StrokeThickness = 2,
                                    };
                LineIndicators.Add(lineIndicator);
            }
        }

        public void Show_PartSearchingResults(IEnumerable<PartSearchingResult> partSearchingResults)
        {
            foreach (var edgeSearchingResult in partSearchingResults)
            {
                if (edgeSearchingResult.HasError) continue;

                var regionIndicator2 = new RegionIndicatorViewModel
                                       {
                                           StartPointX = edgeSearchingResult.PartLine.X1,
                                           StartPointY = edgeSearchingResult.PartLine.Y1,
                                           EndPointX = edgeSearchingResult.PartLine.X2,
                                           EndPointY = edgeSearchingResult.PartLine.Y2,
                                           RegionWidth = edgeSearchingResult.PartHalfWidth,
                                           Stroke = Brushes.Lime,
                                           StrokeThickness = 2,
                                           IsHidden = false,
                                       };
                RegionIndicators.Add(regionIndicator2);
            }
        }

        public void Show_RegionTargetDefinitions(IEnumerable<RegionTargetDefinition> definitions)
        {
            foreach (var ed in definitions)
            {
                var regionIndicator = new RegionIndicatorViewModel
                {
                    StartPointX = ed.RoiActualLine.X1,
                    StartPointY = ed.RoiActualLine.Y1,
                    EndPointX =   ed.RoiActualLine.X2,
                    EndPointY =   ed.RoiActualLine.Y2,
                    RegionWidth = ed.RoiHalfWidth,
                    Stroke = Brushes.Orange,
                    StrokeThickness = 4,
                    IsHidden = false,
                };
                RegionIndicators.Add(regionIndicator);
            }
        }

        public void Show_RegionTargetResults(IEnumerable<RegionTargetResult> results)
        {
            foreach (var result in results)
            {
                if (result.HasError) continue;

                var rect2 = result.TargetRegion.GetSmallestHRectangle2();
                var roiRect = rect2.GetRoiRectangle();

                if (result.Definition.Rect2Len1Line_DisplayEnabled)
                {
                    var line = roiRect.GetWidthLine();

                    var lineIndicator = new LineIndicatorViewModel
                    {
                        StartPointX = line.X1,
                        StartPointY = line.Y1,
                        EndPointX = line.X2,
                        EndPointY = line.Y2,
                        Stroke = Brushes.DeepPink,
                        StrokeThickness = 2,
                    };
                    LineIndicators.Add(lineIndicator);
                }

                if (result.Definition.Rect2Len2Line_DisplayEnabled)
                {
                    var line = roiRect.GetLine();

                    var lineIndicator = new LineIndicatorViewModel
                    {
                        StartPointX = line.X1,
                        StartPointY = line.Y1,
                        EndPointX = line.X2,
                        EndPointY = line.Y2,
                        Stroke = Brushes.DeepPink,
                        StrokeThickness = 2,
                    };
                    LineIndicators.Add(lineIndicator);
                }

                var regionIndicator2 = new RegionIndicatorViewModel
                                       {
                                           StartPointX = roiRect.StartX,
                                           StartPointY = roiRect.StartY,
                                           EndPointX = roiRect.EndX,
                                           EndPointY = roiRect.EndY,
                                           RegionWidth = roiRect.ROIWidth,
                                           Stroke = Brushes.Lime,
                                           StrokeThickness = 2,
                                           IsHidden = false,
                                       };
                RegionIndicators.Add(regionIndicator2);
            }
        }

        public void Show_DefectResults(IEnumerable<RegionDefectResult> regionDefectResults)
        {
            var defectResults = regionDefectResults.SelectMany(x => x.DefectResults);

            var RectangleIndicatorViewModels = new List<RectangleIndicatorViewModel>();
            foreach (var dr in defectResults)
            {
                var regionIndicator = new RectangleIndicatorViewModel
                                      {
                                          CenterX = dr.X,
                                          CenterY = dr.Y,
                                          Width = dr.Width,
                                          Height = dr.Height,
                                          IsHidden = false,
                                          Stroke = Brushes.Lime,
                                          StrokeThickness = 2,
                                      };
                RectangleIndicatorViewModels.Add(regionIndicator);
            }
            DefectIndicators.AddRange(RectangleIndicatorViewModels);
        }

        private void SaveImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog()
                         {
                             DefaultExt = ".tif",
                             FileName = "ExportImage_" + DateTime.Now.ToString("yyyy-MM-dd__HH-mm-ss"),
                             Filter = ".tif|TIFF"
                         };
            var r = dialog.ShowDialog();
            if (r == true)
            {
                IndicatorViewer.BitmapSource.SaveToTiff(dialog.FileName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.Refresh();

            Refresh();
        }

        private void Refresh()
        {
            InspectionSchema schema;
            try
            {
                schema = InspectionController.GetInspectionSchema();
            }
            catch (Exception e)
            {
                MessageBox.Show("GetInspectionSchema filed.\n\n" + e.Message);
                return;
            }

            Inspect(schema);
        }

        private void ZoomFitButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.ZoomFit();
        }

        private void ZoomActualButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.ZoomActual();
        }

        private void ZoomHalfButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.Scale = 0.5;
        }

        private void ZoomQuarterButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.Scale = 0.25;
        }

        private void ExportReportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "InspectionReports_" + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss");
            dialog.Filter = "*.csv|CSV";
            dialog.DefaultExt = ".csv";
            var ok = dialog.ShowDialog();
            if (ok != true)
                return;

            var fileName = dialog.FileName;
        }

        private void ZoomInButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.ZoomIn();
        }

        private void ZoomOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.ZoomOut();
        }

        private void ZoomActualToCenterButton_OnClick(object sender, RoutedEventArgs e)
        {
            IndicatorViewer.ZoomActual();
            IndicatorViewer.X = - IndicatorViewer.BitmapSource.PixelWidth/2.0 + IndicatorViewer.ActualWidth/2.0;
            IndicatorViewer.Y = - IndicatorViewer.BitmapSource.PixelHeight/2.0 + IndicatorViewer.ActualHeight/2.0;
            IndicatorViewer.ZoomOut();
//            IndicatorViewer.ZoomOut();
//            IndicatorViewer.Zoom(new Point(IndicatorViewer.BitmapSource.PixelWidth/2.0,IndicatorViewer.BitmapSource.PixelHeight/2.0 ),
//                0.5);
        }
    }
}

// ReSharper restore InconsistentNaming