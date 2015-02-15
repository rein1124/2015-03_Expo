using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;

namespace Hdc.Mercury.Configs
{
    [ContentProperty("DeviceDefs")]
    public class DeviceDefSchema
    {
        private IList<DeviceDef> _deviceDefCollection = new Collection<DeviceDef>();

        private DeviceGroupDef _groupsDef;

        public DeviceGroupDef RootGroupDef
        {
            get { return _groupsDef; }
            set { _groupsDef = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<DeviceDef> DeviceDefCollection
        {
            get { return _deviceDefCollection; }
            set { _deviceDefCollection = value; }
        }

        /// <summary>
        /// for xaml serialization
        /// </summary>
        public ICollection<DeviceDef> DeviceDefs
        {
            get { return _deviceDefCollection; }
        }

        public bool IsSimulated { get; set; }

        public int GenerationCount { get; set; }
    }
}