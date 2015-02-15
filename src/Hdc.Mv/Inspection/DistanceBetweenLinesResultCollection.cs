using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class DistanceBetweenLinesResultCollection : Collection<DistanceBetweenLinesResult>
    {
        public DistanceBetweenLinesResultCollection(IList<DistanceBetweenLinesResult> list) : base(list)
        {
        }

        public DistanceBetweenLinesResultCollection()
        {
        }
    }
}