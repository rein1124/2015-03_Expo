using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Mercury
{
    public static class FacilityNodeExtensions
    {
/*        public static IList<TChild> CreateChildren<TChild>(IFacilityNode me,
                                                           Func<IDeviceGroup, IEnumerable<IDeviceGroup>>
                                                               getChildDeviceGroups)
            where TChild : IFacilityNode
        {
            var childContexts = getChildDeviceGroups(me.Context);

            int counter = 0;

            var children = new List<TChild>();

            foreach (var childContext in childContexts)
            {
                var child = ServiceLocator.Current.GetInstance<TChild>();

                children.Add(child);
                child.Parent = me;
                child.Index = counter;
                child.Initialize(childContext);

                counter++;
            }

            return children;
        }*/
    }
}