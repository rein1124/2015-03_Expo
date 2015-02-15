using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Practices.ServiceLocation;
using ODM.Domain.Configs;
using Omu.ValueInjecter;
using Shared;

namespace ODM.Domain.Schemas
{
    public class ParameterEntry
    {
        public ParameterEntry()
        {
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        [NotMapped]
        public string GroupName { get; set; }

        [NotMapped]
        public string CatalogName { get; set; }

//        public long ProductionSchemaId { get; set; }

//        [ForeignKey("ProductionSchemaId")]
        [Required]
        public virtual ProductionSchema ProductionSchema { get; set; }

        public bool IsPlcDevice { get; set; }

        public void InitMetadata()
        {
            var mcp = ServiceLocator.Current.GetInstance<IMachineConfigProvider>().MachineConfig;
            var pm = mcp.GetParameterMetadata(Name);
            this.InjectFrom(pm);
        }
    }
}