using System.Data.Entity;
using Hdc.Patterns;
using ODM.Domain;
using ODM.Domain.Inspection;
using ODM.Domain.Schemas;

namespace ODM.Infrastructure
{
    public class DatasContext : EfDbContext, IDatasContext
    {
//        private const string connectionStringForLocalDb =
//            @"Server=(localdb)\v11.0;
//Integrated Security=true;
//AttachDbFileName=|DataDirectory|Vins.Phone.LocalDb.mdf;
//initial catalog=Vins.Phone.DatasContext";
//
//        public DatasContext()
//            : base(connectionStringForLocalDb)
//        {
//        }

        public DatasContext(string connectionStringForLocalDb)
            : base(connectionStringForLocalDb)
        {
        }


        public class DatasContextCreateDatabaseIfNotExistsInitializer : CreateDatabaseIfNotExists<DatasContext>
        {
            protected override void Seed(DatasContext context)
            {
                base.Seed(context);

                context.SaveChanges();
            }
        }

        public class DatasContextDropCreateDatabaseIfModelChangesInitializer :
            DropCreateDatabaseIfModelChanges<DatasContext>
        {
            protected override void Seed(DatasContext context)
            {
                base.Seed(context);

                context.SaveChanges();
            }
        }

        public DbSet<WorkpieceInfo> WorkpieceInfos { get; set; }

        public DbSet<DefectInfo> DefectInfos { get; set; }
        public DbSet<MeasurementInfo> MeasurementInfos { get; set; }
        public DbSet<StoredImageInfo> StoredImageInfos { get; set; }

        public DbSet<ProductionSchema> ProductionSchemas { get; set; }

        public DbSet<ParameterEntry> ParameterEntries { get; set; }
    }
}