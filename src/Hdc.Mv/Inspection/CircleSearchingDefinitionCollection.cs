using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class CircleSearchingDefinitionCollection : Collection<CircleSearchingDefinition>
    {
        public CircleSearchingDefinitionCollection()
        {
        }

        public CircleSearchingDefinitionCollection(IList<CircleSearchingDefinition> list) : base(list)
        {
        }
    }
}