using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Hdc.Modularity
{
    public class ComponentMap : IComponentMap
    {
        [Dependency]
        public IComponentReader ComponentReader { get; set; }

        private IList<ComponentValueEntry> mapDictionary;

        [InjectionMethod]
        public void Init()
        {
            mapDictionary = ComponentReader.Read();
        }

        public IList<string> GetItems(string regionName)
        {
            var itemNames = (from keyvalue in mapDictionary
                             where keyvalue.RegionName == regionName
                             select keyvalue.ItemName)
                .ToList();
            return itemNames;
        }
    }
}