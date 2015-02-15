using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Hdc.Collections.Generic;
using Hdc.Mercury;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class DomainFacilityNodeMiddle<TThis,
                                             TParent,
                                             TChild> :
                                                 ContextNodeMiddle<
                                                     TThis,
                                                     IDeviceGroup,
                                                     TParent,
                                                     IDeviceGroup,
                                                     TChild,
                                                     IDeviceGroup>,
                                                 IFacilityNodeMiddle<
                                                     TThis,
                                                     TParent,
                                                     TChild>
        where TThis : class, IFacilityNodeMiddle<TThis, TParent, TChild>
        where TParent : IFacilityNodeParent<TParent, TThis>
        where TChild : IFacilityNodeChild<TChild, TThis>
    {
        public int DisplayIndex
        {
            get { return Index + 1; }
        }

        protected override void OnInitialized(IDeviceGroup context)
        {
            base.OnInitialized(context);

            var type = this.GetType();
/*            foreach (var prop in type.GetProperties())
            {
                var attri = prop.GetCustomAttributes(typeof(DeviceAttribute), false).FirstOrDefault() as DeviceAttribute;
                if (attri == null) continue;

                var dataType = prop.PropertyType.GetGenericArguments()[0];

                string deviceName;
                if (attri.Key == null || string.IsNullOrEmpty(attri.Key.ToString()))
                {
                    var i = prop.Name.LastIndexOf("Device", System.StringComparison.Ordinal);
                    if (i == -1)
                    {
                        throw new InvalidOperationException("DeviceAttribute must declare name end with [Device]");
                    }
                    deviceName = prop.Name.Remove(i, "Device".Count());
                }
                else
                {
                    deviceName = attri.Key.ToString();
                }

                var device = Context.GetDevice(deviceName, dataType);
                this.SetPropertyValue(prop.Name, device);
            }*/

            var attributePropertyInfos = type.GetAttributePropertyInfos(typeof(DeviceAttribute), false);
            foreach (var attributePropertyInfo in attributePropertyInfos)
            {
//                var attri = prop.GetCustomAttributes(typeof(DeviceAttribute), false).FirstOrDefault() as DeviceAttribute;
//                if (attri == null) continue;

                var attri = (DeviceAttribute)attributePropertyInfo.Attribute;
                var prop = attributePropertyInfo.PropertyInfo;
                var dataType = prop.PropertyType.GetGenericArguments()[0];

                string deviceName;
                if (attri.Key == null || string.IsNullOrEmpty(attri.Key.ToString()))
                {
                    var i = prop.Name.LastIndexOf("Device", System.StringComparison.Ordinal);
                    if (i == -1)
                    {
                        throw new InvalidOperationException("DeviceAttribute must declare name end with [Device]");
                    }
                    deviceName = prop.Name.Remove(i, "Device".Count());
                }
                else
                {
                    deviceName = attri.Key.ToString();
                }

                var device = Context.GetDevice(deviceName, dataType);
                this.SetPropertyValue(prop.Name, device);
            }
        }

    }
}