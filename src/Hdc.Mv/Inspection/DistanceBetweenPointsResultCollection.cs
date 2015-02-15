using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class DistanceBetweenPointsResultCollection : Collection<DistanceBetweenPointsResult>
    {
        public DistanceBetweenPointsResultCollection()
        {
        }

        public DistanceBetweenPointsResultCollection(IList<DistanceBetweenPointsResult> list) : base(list)
        {
        }
    }
}