using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class EdgeSearchingDefinitionCollection : Collection<EdgeSearchingDefinition>
    {
        public EdgeSearchingDefinitionCollection()
        {
        }

        public EdgeSearchingDefinitionCollection(IList<EdgeSearchingDefinition> list) : base(list)
        {
        }
    }
}