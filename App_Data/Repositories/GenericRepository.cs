using App_Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly UserContext _context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(UserContext context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }
        //  private readonly OrderBy<TEntity> DefaultOrderBy = new OrderBy<TEntity>(qry => qry.OrderBy(e => e.Id));



        public virtual IEnumerable<TEntity> GetAll(bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, noTracking, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, noTracking, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, noTracking, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, noTracking, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> GetPage(int startRow, int pageLength, bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            //  if (orderBy == null) orderBy = DefaultOrderBy.Expression;

            var result = QueryDb(null, noTracking, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPageAsync(int startRow, int pageLength, bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            //    if (orderBy == null) orderBy = DefaultOrderBy.Expression;

            var result = QueryDb(null, noTracking, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual TEntity Get(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }
            throw new NotImplementedException();

            //  return query.SingleOrDefault(x => x.Id == id);
        }

        public virtual Task<TEntity> GetAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }
            throw new NotImplementedException();

            //  return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, noTracking, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, noTracking, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Expression<Func<TEntity, bool>> filter, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, noTracking, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Expression<Func<TEntity, bool>> filter, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, noTracking, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> QueryPage(int startRow, int pageLength, bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            // if (orderBy == null) orderBy = DefaultOrderBy.Expression;

            var result = QueryDb(filter, noTracking, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryPageAsync(int startRow, int pageLength, bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            //  if (orderBy == null) orderBy = DefaultOrderBy.Expression;

            var result = QueryDb(filter, noTracking, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            _context.Set<TEntity>().Add(entity);
        }



        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(entity);
        }

       

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.AnyAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.CountAsync();
        }

        protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter, bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (noTracking)
                return query.AsNoTracking();
            return query;
        }

        public void SetUnchanged(TEntity entity)
        {
            this._context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

        public TEntity GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> getUserProcess(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
