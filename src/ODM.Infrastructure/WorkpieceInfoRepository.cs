using System.Data.Entity;
using Hdc.Patterns;
using Microsoft.Practices.Unity;
using ODM.Domain;
using ODM.Domain.Inspection;

namespace ODM.Infrastructure
{
    public class WorkpieceInfoRepository : EfRepository<WorkpieceInfo>, IWorkpieceInfoRepository
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

        protected override DbSet<WorkpieceInfo> GetDbSet()
        {
            return DatasContext.WorkpieceInfos;
        }
    }
}