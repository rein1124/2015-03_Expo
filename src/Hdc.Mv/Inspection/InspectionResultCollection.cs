using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class InspectionResultCollection : Collection<InspectionResult>
    {
        public InspectionResultCollection()
        {
        }

        public InspectionResultCollection(IList<InspectionResult> list) : base(list)
        {
        }
    }
}