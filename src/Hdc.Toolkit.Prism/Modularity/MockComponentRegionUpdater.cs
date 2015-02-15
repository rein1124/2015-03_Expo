using System;
using System.Collections.Generic;

namespace Hdc.Modularity
{
    public class MockComponentRegionUpdater : IComponentRegionUpdater
    {
        public IDictionary<string, bool> GetRegionStates(IDictionary<string, bool> componentStates)
        {
            throw new NotImplementedException();
        }

        public void UpdateRegionStates(IEnumerable<IComponentItem> items, IEnumerable<IComponentRegion> regions)
        {
            
        }
    }
}