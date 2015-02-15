using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Hdc.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Configs
{
    [Export(typeof (IDeviceDefSchemaToConfigConverter))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DeviceDefSchemaToConfigConverter : IDeviceDefSchemaToConfigConverter
    {
        private IDictionary<string, IEnumerable<DeviceDef>> _deviceDefsDic;
        private IDictionary<DeviceDef, IEnumerator<int>> _deviceOffsetsDic;

        public IEnumerable<DeviceGroupConfig> Convert(DeviceDefSchema deviceDefSchema)
        {
            //init offsets
            OffsetProvider.Init(deviceDefSchema);

            InitDeviceOffsetsDict(deviceDefSchema);

            InitDeviceDefsDict(deviceDefSchema);

            if (deviceDefSchema.GenerationCount == 0)
            {
                var top = GetDeviceGroupConfig(deviceDefSchema, null);
                yield return top;
            }
            else
            {
                for (int i = 0; i < deviceDefSchema.GenerationCount; i++)
                {
                    var top = GetDeviceGroupConfig(deviceDefSchema, i);

                    yield return top;
                }
            }
        }

        private DeviceGroupConfig GetDeviceGroupConfig(DeviceDefSchema deviceDefSchema, int? generationIndex)
        {
            DeviceGroupConfig top = GetTopNode(deviceDefSchema);

            DeviceGroupConfigExtensions.TraverseFromTopLeft(
                top,
                (n, p) =>
                    {
                        //var deviceDefs = deviceDefSchema.DeviceDefs.Where(x => x.OffsetDefs.Last().GroupName == n.Name);
                        if (!_deviceDefsDic.ContainsKey(n.Name))
                            return;

                        var deviceDefs = _deviceDefsDic[n.Name];
                        foreach (var deviceDef in deviceDefs)
                        {
                            var offsetE = _deviceOffsetsDic[deviceDef];
                            offsetE.MoveNext();
                            var offset = offsetE.Current;

                            var tagIndex = deviceDef.TagIndexPosition + n.GlobalIndex + offset;
                            var path = deviceDef.Path;
                            string tag;

                            if (generationIndex == null)
                            {
                                if (deviceDef.IsArray)
                                {
                                    tag = PathFactory.CreateArrayPath(path, tagIndex);
                                }
                                else
                                {
                                    tag = PathFactory.Create(path,
                                                             tagIndex,
                                                             deviceDef.TagIndexLength);
                                }
                            }
                            else
                            {
                                if (deviceDef.IsArray)
                                {
                                    tag = PathFactory.CreateArrayPath(path, tagIndex, generationIndex.Value);
                                }
                                else
                                {
                                    tag = PathFactory.Create(path,
                                                             tagIndex,
                                                             deviceDef.TagIndexLength,
                                                             generationIndex.Value);
                                }
                            }

                            var deviceConfig = new DeviceConfig
                                                   {
                                                       Name = deviceDef.Name,
                                                       DataType = deviceDef.DataType,
                                                       //Path = path,
                                                       TagIndex = tagIndex,
                                                       Tag = tag,
                                                       IsSimulated = deviceDef.IsSimulated,
                                                       IsConversionEnabled = deviceDef.IsConversionEnabled,
                                                       ConverterCollection = deviceDef.ConverterCollection.DeepClone(),
                                                    };
                            //deviceDef.ConverterCollection.ForEach(c=>deviceConfig.ConverterCollection.Add(c.DeepClone()));
                            n.DeviceConfigCollection.Add(deviceConfig);
                        }
                    });
            return top;
        }

        private void InitDeviceOffsetsDict(DeviceDefSchema deviceDefSchema)
        {
            _deviceOffsetsDic = new Dictionary<DeviceDef, IEnumerator<int>>();
            foreach (var deviceDef in deviceDefSchema.DeviceDefCollection)
            {
                var offsets = OffsetProvider.GetOffsets(deviceDef);
                _deviceOffsetsDic.Add(deviceDef, offsets.GetEnumerator());
            }
        }

        private void InitDeviceDefsDict(DeviceDefSchema deviceDefSchema)
        {
//init deviceDef map
            _deviceDefsDic = new Dictionary<string, IEnumerable<DeviceDef>>();

            var groups = deviceDefSchema.RootGroupDef.TraverseFromTopLeft();

            foreach (var deviceGroupDef in groups)
            {
                DeviceGroupDef def = deviceGroupDef;
                var deviceDefs = deviceDefSchema.DeviceDefCollection.Where(x => x.GroupName == def.Name);
                _deviceDefsDic.Add(def.Name, deviceDefs);
            }
        }

        private DeviceGroupConfig GetTopNode(DeviceDefSchema deviceDefSchema)
        {
//            var paras = deviceDefSchema.Groups.Select(x => new Tuple<int, DeviceGroupDef>(x.Total, x));
//            var topNode = DeviceGroupConfigExtensions.CreateNodes(paras,
//                                                     () => new DeviceGroupConfig() {Name = "Root"},
//                                                     Get);

//            var topNode = new DeviceGroupConfig()
//                              {
//                                  Name = "Root"
//                              };

//            var groups = deviceDefSchema.RootGroup.TraverseFromTopLeft();
            var topNode = GetGroup(deviceDefSchema.RootGroupDef, new ConcurrentDictionary<string, int>(), 0, 0);

            return topNode;
        }

        public DeviceGroupConfig GetGroup(DeviceGroupDef def, ConcurrentDictionary<string, int> counters,
                                          int globalIndex, int index)
        {
            var gc = new DeviceGroupConfig
                         {
                             Name = def.Name,
                             GlobalIndex = globalIndex,
                             Index = index,
                         };

            foreach (var group in def.GroupDefCollection)
            {
                var groupName = @group.Name;
                var collectionName = groupName + "Collection";

                var childGc = new DeviceGroupConfig()
                                  {
                                      Name = collectionName
                                  };

                gc.GroupConfigCollection.Add(childGc);

                var counter = counters.GetOrAdd(groupName, 0);
                for (int i = 0; i < group.Total; i++)
                {
                    var globalIndex2 = counter;
                    var index2 = i;

                    var grandChildGc = GetGroup(group, counters, globalIndex2, index2);
                    childGc.GroupConfigCollection.Add(grandChildGc);

                    counter++;
                    counters[groupName] = counter;
                }
            }

            return gc;
        }

        [Dependency]
        [Import(typeof (IOffsetProvider))]
        public IOffsetProvider OffsetProvider { get; set; }

        [Dependency]
        [Import(typeof (IPathFactory))]
        public IPathFactory PathFactory { get; set; }

        private DeviceGroupConfig Get(DeviceGroupDef deviceGroupDef, int globalIndex, int index)
        {
            /*            var deviceDefs = DeviceDefSchemaProvider.Schema.DeviceDefs
                            .Where(x => x.OffsetDefs.Last().GroupName == deviceGroupDef.Name);*/

            //            throw new NotImplementedException();
            return new DeviceGroupConfig()
                       {
                           Name = deviceGroupDef.Name,
                           GlobalIndex = globalIndex,
                           Index = index
                       };
        }
    }
}