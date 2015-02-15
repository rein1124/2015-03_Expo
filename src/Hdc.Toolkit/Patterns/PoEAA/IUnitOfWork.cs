using System;
using System.Threading.Tasks;

namespace Hdc.Patterns
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task<int> CommitAsync();

        Task<int> CommitAsync(System.Threading.CancellationToken cancellationToken);

        void Rollback();

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins
        ///</remarks>
        void CommitAndRefreshChanges();
    }
}