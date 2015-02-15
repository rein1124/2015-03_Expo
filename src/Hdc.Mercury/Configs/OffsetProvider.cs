using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Hdc.Collections.ObjectModel;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Configs
{
    [Export(typeof (IOffsetProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OffsetProvider : IOffsetProvider
    {
        private readonly IDictionary<DeviceDef, IList<int>> _offsetsSet = new Dictionary<DeviceDef, IList<int>>();

        [Dependency]
        [Import(typeof (IOffsetResolver))]
        public IOffsetResolver OffsetResolver { get; set; }

//        [Dependency]
//        public IDeviceDefSchemaProvider DeviceDefSchemaProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
//            var schema = DeviceDefSchemaProvider.Schema;

//            Init(schema);
        }

        public void Init(DeviceDefSchema schema)
        {
            var groups = schema.RootGroupDef.TraverseFromTopLeft();
            foreach (var deviceDef in schema.DeviceDefCollection)
            {
                var offsetMarks = GetOffsetMarks(groups, deviceDef);

                var offsets = OffsetResolver.GetOffsets(offsetMarks);

                _offsetsSet.Add(deviceDef, offsets.ToList());
            }
        }

        private static IEnumerable<OffsetMark> GetOffsetMarks(IEnumerable<DeviceGroupDef> deviceGroupDefs,
                                                              DeviceDef deviceDef)
        {
            if (!deviceDef.OffsetDefCollection.Any())
            {
                var total = deviceGroupDefs.Single(x => x.Name == deviceDef.GroupName).Total;
                return new[]
                           {
                               new OffsetMark()
                                   {
                                       Total = total,
                                       Prefix = 0,
                                       Suffix = 0,
                                   }
                           };
            }

            var offsetMarks = from def in deviceDef.OffsetDefCollection
                              let total = deviceGroupDefs.Single(x => x.Name == def.GroupName).Total
                              select new OffsetMark()
                                         {
                                             Total = total,
                                             Prefix = def.Prefix,
                                             Suffix = def.Suffix,
                                         };
            return offsetMarks.ToList();
        }

        public int GetOffset(DeviceDef deviceDef, int globalIndex)
        {
            return _offsetsSet[deviceDef][globalIndex];
        }

        public IList<int> GetOffsets(DeviceDef deviceDef)
        {
            return _offsetsSet[deviceDef];
        }
    }
}