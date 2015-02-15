using System.Collections.Generic;
using System.Windows;

namespace Hdc.Mv.Inspection
{
    public abstract class GeneralInspector : IGeneralInspector
    {
        public bool EdgeSearchingEnable { get; set; }

        public bool CircleSearchingEnable { get; set; }

        public abstract void Dispose();

        public abstract void Init();

        public abstract void Init(int width, int height);

        protected GeneralInspector()
        {
            CircleSearchingDefinitions = new List<CircleSearchingDefinition>();
            CircleSearchingResults = new List<CircleSearchingResult>();

            EdgeSearchingDefinitions = new List<EdgeSearchingDefinition>();
            EdgeSearchingResults = new List<EdgeSearchingResult>();

            DistanceBetweenLinesDefinitions = new List<DistanceBetweenLinesDefinition>();
            DistanceBetweenLinesResults = new List<DistanceBetweenLinesResult>();
            DistanceBetweenLinesResults = new List<DistanceBetweenLinesResult>();
        }

        public abstract void Inspect(ImageInfo imageInfo);

        public IList<CircleSearchingDefinition> CircleSearchingDefinitions { get; private set; }

        public IList<EdgeSearchingDefinition> EdgeSearchingDefinitions { get; private set; }

        public IList<CircleSearchingResult> CircleSearchingResults { get; private set; }

        public IList<EdgeSearchingResult> EdgeSearchingResults { get; private set; }

        public IList<DistanceBetweenLinesDefinition> DistanceBetweenLinesDefinitions { get; private set; }

        public IList<DistanceBetweenLinesResult> DistanceBetweenLinesResults { get; private set; }

        public abstract DistanceBetweenLinesResult GetDistanceBetweenLines(Line line1, Line line2);

        public abstract DistanceBetweenPointsResult GetDistanceBetweenPoints(Point point1, Point point2);

        public void ClearResults()
        {
            CircleSearchingResults.Clear();
            EdgeSearchingResults.Clear();
            DistanceBetweenLinesResults.Clear();
        }

        public void ClearDefinitions()
        {
            CircleSearchingDefinitions.Clear();
            EdgeSearchingDefinitions.Clear();
            DistanceBetweenLinesDefinitions.Clear();
        }

//        public abstract IEnumerable<EdgeSearchingResult> SearchEdges(IEnumerable<EdgeSearchingDefinition> edgeDefinitions);

        public InspectionSchema InspectionSchema { get; set; }


        public abstract SearchingResult Search(ImageInfo imageInfo, SearchingTask searchingTask);

        public abstract SearchingResult SearchEdges(ImageInfo imageInfo, SearchingTask searchingTask);
    }
}