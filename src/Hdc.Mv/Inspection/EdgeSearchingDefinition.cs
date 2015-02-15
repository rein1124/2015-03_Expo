using System;
using System.Windows;

// ReSharper disable InconsistentNaming
namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class EdgeSearchingDefinition : IRoiRectangle
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        // General
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
        public double ROIWidth { get; set; }
        public double ExpectAngle { get; set; }

        public bool CropDomainEnabled { get; set; }

        public bool Domain_SaveCacheImageEnabled { get; set; }
//        public 

        public IRegionExtractor RegionExtractor { get; set; }
        public bool RegionExtractor_Disabled { get; set; }
        public bool RegionExtractor_SaveCacheImageEnabled { get; set; }
        public bool RegionExtractor_CropDomainEnabled { get; set; }

        public IImageFilter ImageFilter { get; set; }
        public bool ImageFilter_Disabled { get; set; }
        public bool ImageFilter_SaveCacheImageEnabled { get; set; }
        public bool ImageFilter_CropDomainEnabled { get; set; }

        public ILineExtractor LineExtractor { get; set; }
        public bool LineExtractor_SaveCacheImageEnabled { get; set; }
        public bool LineExtractor_CropDomainEnabled { get; set; }

        public EdgeSearchingDefinition()
        {
        }

        public EdgeSearchingDefinition(Line line, double roiWidth = 0)
            : this(line.X1, line.Y1, line.X2, line.Y2, roiWidth)
        {
        }

        public EdgeSearchingDefinition(double startX, double startY, double endX, double endY, double roiWidth = 0)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            ROIWidth = roiWidth;
        }

        public EdgeSearchingDefinition(Point p1, Point p2, double roiWidth = 0) :
            this(p1.X, p1.Y, p2.X, p2.Y, roiWidth)
        {
        }

        public Line Line
        {
            get { return new Line(StartX, StartY, EndX, EndY); }
        }

        public Line RelativeLine { get; set; }
    }
}
// ReSharper restore InconsistentNaming