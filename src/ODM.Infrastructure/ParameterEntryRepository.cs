using System.Data.Entity;
using Hdc.Patterns;
using Microsoft.Practices.Unity;
using ODM.Domain.Schemas;

namespace ODM.Infrastructure
{
    public class ParameterEntryRepository : EfRepository<ParameterEntry>, IParameterEntryRepository
    {
        [Dependency]
        public IDatasContext DatasContext { get; set; }

        [InjectionMethod]
        public void Init()
        {
        }

        protected override IQueryableUnitOfWork GetUnitOfWork()
        {
            return DatasContext;
        }

        protected override DbSet<ParameterEntry> GetDbSet()
        {
            return DatasContext.ParameterEntries;
        }
    }
}