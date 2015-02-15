using System.Collections;
using System.Collections.Generic;

namespace ODM.Domain.Schemas
{
    public interface IParameterEntryLoader
    {
        IList<ParameterEntry> LoadParameterEntries(string fileName);
    }
}