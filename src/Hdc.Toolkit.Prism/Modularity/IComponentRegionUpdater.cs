using System.Collections.Generic;

namespace Hdc.Modularity
{
    public interface IComponentRegionUpdater
    {
        //IDictionary<string, bool> GetRegionStates(IDictionary<string, bool> componentStates);

        void UpdateRegionStates(IEnumerable<IComponentItem> items, IEnumerable<IComponentRegion> regions);
    }
}