using System;
using System.Linq;
using ODM.Domain.Configs;

namespace ODM.Domain.Schemas
{
    public static class ProductionSchemaFactory
    {
        public static ProductionSchema CreateDefaultProductionSchema()
        {
            var pms = ParameterMetadataSchema.CreateDefaultSchema();

            var ps = new ProductionSchema();

            foreach (var parameterMetadata in pms.ParameterMetadatas)
            {
                var parameterEntry = new ParameterEntry()
                                         {
                                             Name = parameterMetadata.Name,
                                             IsPlcDevice = parameterMetadata.IsPlcDevice,
                                         };

                ps.ParameterEntries.Add(parameterEntry);
            }

            return ps;
        }
    }
}