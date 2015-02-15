//using Hdc.Patterns;

using System.Data.Entity;
using Hdc.Patterns;
using ODM.Domain;
using ODM.Domain.Inspection;
using ODM.Domain.Schemas;

namespace ODM.Infrastructure
{
    public interface IDatasContext : IQueryableUnitOfWork
    {
        DbSet<WorkpieceInfo> WorkpieceInfos { get; set; }

        DbSet<DefectInfo> DefectInfos { get; set; }

        DbSet<MeasurementInfo> MeasurementInfos { get; set; }

        DbSet<StoredImageInfo> StoredImageInfos { get; set; }

        DbSet<ProductionSchema> ProductionSchemas { get; set; }

        DbSet<ParameterEntry> ParameterEntries { get; set; }
    }
}