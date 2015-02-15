using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class SurfaceDefinitionCollection : Collection<SurfaceDefinition>
    {
        public SurfaceDefinitionCollection()
        {
        }

        public SurfaceDefinitionCollection(IList<SurfaceDefinition> list)
            : base(list)
        {
        }
    }
}