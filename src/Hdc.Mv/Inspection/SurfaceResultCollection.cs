using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class SurfaceResultCollection : Collection<SurfaceResult>
    {
        public SurfaceResultCollection()
        {
        }

        public SurfaceResultCollection(IList<SurfaceResult> list)
            : base(list)
        {
        }
    }
}