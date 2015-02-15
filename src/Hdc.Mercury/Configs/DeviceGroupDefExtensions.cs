using System.Collections.Generic;
using System.Linq;
using Hdc.Collections;

namespace Hdc.Mercury.Configs
{
    public static class DeviceGroupDefExtensions
    {
        public static DeviceGroupDef AddGroup(this DeviceGroupDef group, DeviceGroupDef childGroup)
        {
            group.GroupDefCollection.Add(childGroup);
//            childGroup.ParentName = group.Name;
            return group;
        }

        public static DeviceGroupDef GetGroup(this DeviceGroupDef group, string childGroupName)
        {
            return group.GroupDefCollection.SingleOrDefault(gd => gd.Name == childGroupName);
        }

        public static DeviceGroupDef AddAndGetGroup(this DeviceGroupDef group, DeviceGroupDef childGroup)
        {
            return group.AddGroup(childGroup).GetGroup(childGroup.Name);
        }

        public static IList<DeviceGroupDef> TraverseFromTopLeft(this DeviceGroupDef group)
        {
            return group.TraverseFromTopLeft(x => x.GroupDefCollection).ToList();
        }
    }
}