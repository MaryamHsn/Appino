using App_Data.Repositories;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App_Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();
        IRepository<UserTbl> UserTblRepository { get; }
        IRepository<UserTokenTbl> UserTokenTblRepository { get; }
        void Save();

    }
}
