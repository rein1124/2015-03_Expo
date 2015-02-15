using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class DefectResultCollection : Collection<DefectResult>
    {
        public DefectResultCollection()
        {
        }

        public DefectResultCollection(IList<DefectResult> list) : base(list)
        {
        }
    }
}