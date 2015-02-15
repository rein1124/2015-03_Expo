using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class CircleSearchingResultCollection : Collection<CircleSearchingResult>
    {
        public CircleSearchingResultCollection()
        {
        }

        public CircleSearchingResultCollection(IList<CircleSearchingResult> list) : base(list)
        {
        }
    }
}