using System;
using System.Linq;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class RakeLineExtractor : ILineExtractor
    {
        public Line FindLine(HImage image, Line searchLine)
        {
            var lines = HDevelopExport.Singletone.RakeEdgeLine(image,
                line: searchLine,
                regionsCount: RegionsCount,
                regionHeight: RegionHeight,
                regionWidth: RegionWidth,
                sigma: Sigma,
                threshold: Threshold,
                transition: Transition,
                selectionMode: SelectionMode);
            if (lines.Any())
                return lines.First();
            return new Line();
        }

        public string Name { get; set; }
        public bool SaveCacheImageEnabled { get; set; }

        public int RegionsCount { get; set; }
        public int RegionHeight { get; set; }
        public int RegionWidth { get; set; }
        public double Sigma { get; set; }
        public double Threshold { get; set; }
        public SelectionMode SelectionMode { get; set; }
        public Transition Transition { get; set; }
    }
}