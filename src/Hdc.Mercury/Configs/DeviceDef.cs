using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;
using Hdc.Mercury.Converters;

namespace Hdc.Mercury.Configs
{
    [ContentProperty("Items")]
    public class DeviceDef
    {
        private IList<OffsetDef> _offsetDefCollection = new Collection<OffsetDef>();
        private IList<IGenericValueConverter> _converterCollection = new Collection<IGenericValueConverter>();

        public DeviceDef()
        {
        }

        public DeviceDef(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public DeviceDataType DataType { get; set; }

        public string Path { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<OffsetDef> OffsetDefCollection
        {
            get { return _offsetDefCollection; }
            set { _offsetDefCollection = value; }
        }

        /// <summary>
        /// for xaml serialization
        /// </summary>
        public ICollection<OffsetDef> OffsetDefs
        {
            get { return _offsetDefCollection; }
        }

        public int TagIndexLength { get; set; }

        public int TagIndexPosition { get; set; }

        public bool IsSimulated { get; set; }

        public bool IsArray { get; set; }

        public string GroupName { get; set; }

        public string ModuleName { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Function { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Comment { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DefaultValue { get; set; }

        public bool IsConversionEnabled { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<IGenericValueConverter> ConverterCollection
        {
            get { return _converterCollection; }
            set { _converterCollection = value; }
        }

        /// <summary>
        /// for xaml serialization
        /// </summary>
        public ICollection<IGenericValueConverter> Converters
        {
            get { return _converterCollection; }
        }
    }
}