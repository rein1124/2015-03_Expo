using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Hdc.Modularity
{
    public class ComponentRegionUpdater : IComponentRegionUpdater
    {
        [Dependency]
        public IComponentMap ComponentMap { get; set; }

        public void UpdateRegionStates(IEnumerable<IComponentItem> items, IEnumerable<IComponentRegion> regions)
        {
            //bool isActive = false;

            foreach (var componentRegion in regions)
            {
                var assItemNames = ComponentMap.GetItems(componentRegion.Name);


                var itemActives = new List<bool>();


                if (assItemNames == null)
                    continue;


                for (int i = 0; i < assItemNames.Count; i++)
                {
                    var name = assItemNames[i];

                    var item = items.FirstOrDefault(x => x.Name == name);

                    if (item == null)
                        continue;

                    itemActives.Add(item.IsActive);


//                        isActive = items
//                            .Where(componentItem => componentItem.Name == assItemNames[i])
//                            .Aggregate(isActive, (current, componentItem) => (current || componentItem.IsActive));
                }
                

                componentRegion.IsActive = itemActives.Any(x => x);
            }
        }
    }
}