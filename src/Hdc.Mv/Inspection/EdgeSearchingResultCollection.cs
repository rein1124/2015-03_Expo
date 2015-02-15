using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class EdgeSearchingResultCollection : Collection<EdgeSearchingResult>
    {
        public EdgeSearchingResultCollection()
        {
        }

        public EdgeSearchingResultCollection(IList<EdgeSearchingResult> list) : base(list)
        {
        }
    }
}