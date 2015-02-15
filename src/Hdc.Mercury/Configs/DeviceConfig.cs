using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Hdc.Mercury.Converters;

namespace Hdc.Mercury.Configs
{
    public class DeviceConfig
    {
        public string Name { get; set; }

        public DeviceDataType DataType { get; set; }

        public int TagIndex { get; set; }

        public string Tag { get; set; }

        public bool IsSimulated { get; set; }

        public string Comment { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BooleanListLength { get; set; }

        private IList<IGenericValueConverter> _converterCollection = new Collection<IGenericValueConverter>();

        //[DefaultValue(false)]
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