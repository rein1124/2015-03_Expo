using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using Hdc.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    [ContentProperty("GroupDefs")]
    public class DeviceGroupDef
    {
        private IList<DeviceGroupDef> _groupDefCollection = new Collection<DeviceGroupDef>();

        public DeviceGroupDef()
        {
        }

        public DeviceGroupDef(string name, int total, params DeviceGroupDef[] groups)
        {
            Name = name;
            Total = total;

            GroupDefCollection.AddRange(groups);
//            Groups.ForEach(g => g.ParentName = Name);
        }

        public string Name { get; set; }

        public int Total { get; set; }

//        public string ParentName { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<DeviceGroupDef> GroupDefCollection
        {
            get { return _groupDefCollection; }
            set { _groupDefCollection = value; }
        }

        /// <summary>
        /// for xaml serialization
        /// </summary>
        public ICollection<DeviceGroupDef> GroupDefs
        {
            get { return _groupDefCollection; }
        }
    }
}