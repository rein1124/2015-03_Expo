using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hdc.Patterns
{
    public abstract class EfRepository<T> : IRepository<T> where T : class, new()
    {
        private IQueryableUnitOfWork _unitOfWork;

        private DbSet<T> _dbSet;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = GetUnitOfWork()); }
        }

        public DbSet<T> DbSet
        {
            get { return _dbSet ?? (_dbSet = GetDbSet()); }
        }

        protected abstract IQueryableUnitOfWork GetUnitOfWork();

        protected abstract DbSet<T> GetDbSet();

        public T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public Task<T> GetByIdAsync(long id)
        {
            return DbSet.FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return DbSet;
        }

        public T Get(Expression<Func<T, bool>> whereCondition)
        {
            return GetQuery().Where(whereCondition).FirstOrDefault();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> whereCondition)
        {
            return GetQuery().Where(whereCondition)
                .ToListAsync()
                .ContinueWith(t => t.Result.FirstOrDefault());
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> whereCondition)
        {
            return GetQuery().Where(whereCondition);
        }

        public Task<List<T>> GetManyAsync(Expression<Func<T, bool>> whereCondition)
        {
            return GetQuery().Where(whereCondition).ToListAsync();
        }

        public List<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return GetQuery().ToListAsync();
        }

        public void Add(T entity)
        {
            if (entity != (T)null)
            {
                DbSet.Add(entity);
            }
            else
            {
                throw new InvalidOperationException("cannot add null entity");
            }
        }

        public void Update(T entity)
        {
            if (entity != (T)null)
            {
                DbSet.Attach(entity);

                //((DbContext)QueryableUnitOfWork)..ChangeObjectState(entity, EntityState.Modified);
                var dbEntityEntry = ((DbContext)UnitOfWork).Entry(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            else
            {
                throw new InvalidOperationException("cannot update null entity");
            }
        }

        public void Delete(T entity)
        {
            if (entity != (T)null)
            {
                // attach item if not exist
                DbSet.Attach(entity); // or, _unitOfWork.Attach(removingEntity);

                // set as "removed"
                DbSet.Remove(entity);
            }
            else
            {
                throw new InvalidOperationException("cannot delete null entity");
            }
        }


        [Obsolete("cause the performance reason, rein, 2012-09-17")]
        public void DeleteById(long id)
        {
            // obsolete, cause the performance reason
            //var x = GetById(id);
            //Delete(x);

            dynamic removingEntity = new T();
            removingEntity.Id = id;
            DbSet.Attach(removingEntity);
            DbSet.Remove(removingEntity);
        }

        public IEnumerable<T> GetPagedElements<TProperty>(int pageIndex, int pageCount,
                                                          Expression<Func<T, TProperty>> orderByExpression,
                                                          bool ascending)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPagedElements<TProperty>(Expression<Func<T, bool>> where, int pageIndex, int pageCount,
                                                          Expression<Func<T, TProperty>> orderByExpression,
                                                          bool ascending)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetElements<TProperty>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetElements<TProperty>(Expression<Func<T, bool>> where,
                                                     Expression<Func<T, TProperty>> orderByExpression, bool ascending)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQuery(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery());
        }

        public IEnumerable<T> Find(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).AsEnumerable();
        }

        public T FindOne(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntityFrom(GetQuery());
        }

        public int Count(ISpecification<T> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).Count();
        }
    }
}