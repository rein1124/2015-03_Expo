using System;

namespace Hdc.Mercury
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DeviceAttribute : Attribute
    {
        public DeviceAttribute()
        {
        }

        public DeviceAttribute(object key)
        {
            Key = key;
        }

        public object Key { get; set; }
    }
}