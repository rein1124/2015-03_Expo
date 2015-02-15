using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;
using Hdc.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    [ContentProperty("GroupConfigs")]
    public class DeviceGroupConfig
    {
        private IList<DeviceConfig> _deviceConfigs = new Collection<DeviceConfig>();

        private IList<DeviceGroupConfig> _groupConfigs = new Collection<DeviceGroupConfig>();

        public int Index { get; set; }

        public int GlobalIndex { get; set; }

        public string Name { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<DeviceGroupConfig> GroupConfigCollection
        {
            get { return _groupConfigs; }
            set { _groupConfigs = value; }
        }

        public ICollection<DeviceGroupConfig> GroupConfigs
        {
            get { return _groupConfigs; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<DeviceConfig> DeviceConfigCollection
        {
            get { return _deviceConfigs; }
            set { _deviceConfigs = value; }
        }

        public ICollection<DeviceConfig> DeviceConfigs
        {
            get { return _deviceConfigs; }
        }
    }
}