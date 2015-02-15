using System;
using System.Collections.Generic;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class RegionDefectResult
    {
//        public int Index { get; set; }
        public RegionResult RegionResult { get; set; }
        public IList<DefectResult> DefectResults { get; set; }

        public RegionDefectResult()
        {
            DefectResults = new List<DefectResult>();
        }
    }
}