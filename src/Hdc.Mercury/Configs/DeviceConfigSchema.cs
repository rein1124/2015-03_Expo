using System;
using System.Windows.Markup;

namespace Hdc.Mercury.Configs
{
    [ContentProperty("RootGroupConfig")]
    public class DeviceConfigSchema
    {
        public string Version { get; set; }

        public long BuildNumber { get; set; }

        public Guid Guid { get; set; }

        public DateTime DateTime { get; set; }

        public string Author { get; set; }

        public DeviceGroupConfig RootGroupConfig { get; set; }
    }
}