using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IEnumerable<TEntity> GetPage(int startRij, int aantal, bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> GetPageAsync(int startRij, int aantal, bool noTracking, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity Get(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<TEntity> GetAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IEnumerable<TEntity> QueryPage(int startRij, int aantal, bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> QueryPageAsync(int startRij, int aantal, bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        void Load(bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task LoadAsync(bool noTracking, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        void Insert(TEntity entity);

      
        void Delete(TEntity entity);

        bool Any(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);

        int Count(Expression<Func<TEntity, bool>> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        void SetUnchanged(TEntity entitieit);
        TEntity GetByID(object id);
        List<TEntity> getUserProcess(int userId);

    }
}
