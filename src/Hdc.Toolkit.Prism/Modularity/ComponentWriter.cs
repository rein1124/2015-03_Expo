using System.Linq;
using Hdc.Serialization;

namespace Hdc.Modularity
{
    public class ComponentWriter : IComponentWriter
    {
        public void Write(string fileName, ComponentValueEntry[] entries)
        {
            var config = new ComponentConfig();
            entries.ForEach(entry => config.Entries.Add(entry));
            config.SerializeToXamlFile(fileName);
//
//            var xmlFileSaving = new ComponentValueEntry[]
//                                    {
//                                        new ComponentValueEntry()
//                                            {
//                                                ItemName = "InkRollerFunction", 
//                                                RegionName = "InkRollerRegion",
//                                            },
//                                        new ComponentValueEntry()
//                                            {
//                                                ItemName = "WaterRollerFunction", 
//                                                RegionName = "WaterRollerRegion",
//                                            },
//                                        new ComponentValueEntry()
//                                            {
//                                                ItemName = "CLRegisterFunction", 
//                                                RegionName = "CLRegisterRegion",
//                                            },
//                                        new ComponentValueEntry()
//                                            {
//                                                ItemName = "DRegisterFunction", 
//                                                RegionName = "DRegisterRegion",
//                                            },
//                                        new ComponentValueEntry()
//                                            {
//                                                ItemName = "CLRegisterFunction", 
//                                                RegionName = "AllRegisterRegion",
//                                            },
//                                        new ComponentValueEntry()
//                                            {
//                                                ItemName = "DRegisterFunction", 
//                                                RegionName = "AllRegisterRegion",
//                                            },
//                                    };
        }
    }
}