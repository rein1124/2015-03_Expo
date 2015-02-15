using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hdc.Mv.Inspection
{
    public class PartSearchingDefinitionCollection : Collection<PartSearchingDefinition>
    {
        public PartSearchingDefinitionCollection()
        {
        }

        public PartSearchingDefinitionCollection(IList<PartSearchingDefinition> list)
            : base(list)
        {
        }
    }
}