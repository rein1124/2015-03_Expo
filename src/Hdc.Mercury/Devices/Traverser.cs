using System;
using System.Collections.Generic;
using Hdc.Collections;
using Hdc.Mercury.Configs;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Mercury
{
    public class Traverser
    {
        public IServiceLocator ServiceLocator { get; set; }

//        private Stack<IDeviceTree> _stack;

        public IDeviceGroup Init(DeviceGroupConfig deviceGroupConfig)
        {
            var dt = Traverse(deviceGroupConfig);

            return dt;
        }

        private IDeviceGroup Traverse(DeviceGroupConfig deviceGroupConfig, Stack<IDeviceGroup> stack = null)
        {
            if (stack == null)
                stack = new Stack<IDeviceGroup>();

            var dt = ServiceLocator.GetInstance<IDeviceGroup>();

            //TODO
            //dt.Init(deviceTreeConfig)

            stack.Push(dt);


            foreach (var childDeviceTreeConfig in deviceGroupConfig.GroupConfigCollection)
            {
                var childDt = Traverse(childDeviceTreeConfig, stack);
                stack.Peek().DeviceGroups.Add(childDt);
            }

            stack.Pop();

            return dt;
        }

        private IDeviceGroup Traverse(DeviceGroupConfig deviceGroupConfig)
        {
            return TraverseEx.TraverseMap(deviceGroupConfig,
                            x =>
                                {
                                    var dt = ServiceLocator.GetInstance<IDeviceGroup>();

                                    //TODO
                                    //dt.Init(deviceTreeConfig)
                                    return dt;
                                },
                            x => x.GroupConfigCollection,
                            (s, t) => s.DeviceGroups.Add(t));
        }

       
    }
}