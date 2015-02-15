using System.Collections.Generic;
using System.Linq;
using System.Xaml;
using Hdc.IO;
using Hdc.Reflection;
using Hdc.Serialization;

namespace Hdc.Modularity
{
    public class ComponentReader : IComponentReader
    {
        private readonly ComponentConfig _componentConfig;

        public ComponentReader()
        {
            _componentConfig = new ComponentConfig();
        }

        public ComponentReader(ComponentConfig componentConfig)
        {
            _componentConfig = componentConfig;
        }

        public ComponentReader(string componentConfigFilePath)
        {
            if (!componentConfigFilePath.IsFileExist())
                return;

            _componentConfig = componentConfigFilePath.DeserializeFromXamlFile<ComponentConfig>();
        }

        public ComponentConfig ComponentConfig
        {
            get { return _componentConfig; }
        }

        public IList<ComponentValueEntry> Read()
        {
            return _componentConfig.EntryCollection;
          /*  return new List<ComponentValueEntry>()
                       {
                           new ComponentValueEntry("r1", "i1"),
                           new ComponentValueEntry("r2", "i2"),
                           new ComponentValueEntry("r1", "i3"),
                           new ComponentValueEntry("r2", "i3"),
                           new ComponentValueEntry("CLReg", "CLReg"),
                           new ComponentValueEntry("DReg", "DReg"),
                           new ComponentValueEntry("WaterRoller", "WaterRoller"),
                           new ComponentValueEntry("WorkState", "WorkState"),
                           new ComponentValueEntry("JobReport", "JobReport"),
                       };*/
        }
    }
}