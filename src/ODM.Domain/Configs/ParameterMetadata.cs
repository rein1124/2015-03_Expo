using System;
using System.Xml.Serialization;
using Hdc;
using ODM.Domain.Schemas;

namespace ODM.Domain.Configs
{
    public class ParameterMetadata : ValueRange<int>
    {
        public string CatalogName { get; set; }

        public string CatalogDescription { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DefaultValue { get; set; }

        public bool IsPlcDevice { get; set; }
    }
}