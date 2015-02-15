using System;
using System.Linq.Expressions;

namespace Hdc.Reactive
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class ValueMonitorAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public ValueMonitorAttribute()
        {
        }

        public ValueMonitorAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}