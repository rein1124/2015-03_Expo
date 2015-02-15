using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class InspectionResult
    {
        public int Index { get; set; }
        public string Comment { get; set; }

        public InspectionResult()
        {
            // Coordinate
            CoordinateCircles = new CircleSearchingResultCollection();
            CoordinateEdges = new EdgeSearchingResultCollection();

            //
            Circles = new CircleSearchingResultCollection();
            Edges = new EdgeSearchingResultCollection();
            DistanceBetweenLinesResults = new DistanceBetweenLinesResultCollection();
            DistanceBetweenPointsResults = new DistanceBetweenPointsResultCollection();
            RegionDefectResults = new List<RegionDefectResult>();
            ClosedRegionResults = new ClosedRegionResultCollection();
            Parts = new List<PartSearchingResult>();
            RegionTargets = new List<RegionTargetResult>();
        }

        public CircleSearchingResultCollection CoordinateCircles { get; set; }
        public EdgeSearchingResultCollection CoordinateEdges { get; set; }
        public CircleSearchingResultCollection Circles { get; set; }
        public EdgeSearchingResultCollection Edges { get; set; }
        public DistanceBetweenLinesResultCollection DistanceBetweenLinesResults { get; set; }
        public DistanceBetweenPointsResultCollection DistanceBetweenPointsResults { get; set; }
//        public DefectResultCollection DefectResults { get; set; }
        public IList<RegionDefectResult> RegionDefectResults { get; set; }
        public ClosedRegionResultCollection ClosedRegionResults { get; set; }
        public IList<PartSearchingResult> Parts { get; set; }
        public IList<RegionTargetResult> RegionTargets { get; set; }
    }
}