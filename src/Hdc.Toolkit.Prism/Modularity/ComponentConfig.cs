using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;

namespace Hdc.Modularity
{
    [ContentProperty("Entries")]
    public class ComponentConfig
    {
        private IList<ComponentValueEntry> _entryCollection = new Collection<ComponentValueEntry>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<ComponentValueEntry> EntryCollection
        {
            get { return _entryCollection; }
            set { _entryCollection = value; }
        }

        /// <summary>
        /// for xaml serialization
        /// </summary>
        public ICollection<ComponentValueEntry> Entries
        {
            get { return _entryCollection; }
        }
    }
}