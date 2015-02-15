using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hdc.Patterns
{
    public abstract class EfDbContext : DbContext, IQueryableUnitOfWork
    {
        protected EfDbContext()
        {
        }

        protected EfDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public void Rollback()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = System.Data.Entity.EntityState.Unchanged);
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                        .ForEach(entry =>
                                 {
                                     entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                                     //entry.Reload();
                                 });
                }
            } while (saveFailed);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public IDbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if not is attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }
    }
}