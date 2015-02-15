using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;
using Shared;

namespace ODM.Domain.Schemas
{
    // ReSharper disable InconsistentNaming
    [ContentProperty("ParameterEntries")]
    public class ProductionSchema
    {
        public ProductionSchema()
        {
            ModifiedDateTime = new DateTime(1900, 1, 1);
            DownloadDateTime = new DateTime(1900, 1, 1);

            ParameterEntries = new Collection<ParameterEntry>();
        }

        [Key]
        public long Id { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime DownloadDateTime { get; set; }

        /// <summary>
        /// ParameterEntries
        /// </summary>
        public virtual Collection<ParameterEntry> ParameterEntries { get; set; }

        public ParameterEntry GetParameterEntry(string parameterName)
        {
            return ParameterEntries.SingleOrDefault(x => x.Name == parameterName);
        }

        public int GetParameterValue(string parameterName)
        {
            var pe = GetParameterEntry(parameterName);
            if (pe == null)
                return -1;

            return pe.Value;
        }
    }


    // ReSharper restore InconsistentNaming
}