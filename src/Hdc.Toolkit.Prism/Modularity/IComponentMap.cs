using System.Collections.Generic;

namespace Hdc.Modularity
{
    public interface IComponentMap
    {
        IList<string> GetItems(string regionName);
    }
}