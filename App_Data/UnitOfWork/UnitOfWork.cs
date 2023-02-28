using App_Data.Context;
using App_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using System.Threading;

namespace App_Data.UnitOfWork
{
    public class UnitOfWork :  IUnitOfWork, IDisposable
    {


        public UnitOfWork(UserContext context)
        {
            _context = context;
       
        }
     
        protected UserContext _context;
 

        public int SaveChanges()
        {
            CheckDisposed();

            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            CheckDisposed();
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            CheckDisposed();
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {


            CheckDisposed();
            return new GenericRepository<TEntity>(this._context);


        }

        //public TRepository GetCustomRepository<TRepository>()
        //{
        //    CheckDisposed();
        //    var repositoryType = typeof(TRepository);
        //    var repository = (TRepository)_serviceProvider.GetService(repositoryType);
        //    if (repository == null)
        //    {
        //        throw new RepositoryNotFoundException(repositoryType.Name, String.Format("Repository {0} not found in the IOC container. Check if it is registered during startup.", repositoryType.Name));
        //    }

        //    ((IRepositoryInjection)repository).SetContext(_context);
        //    return repository;
        //}


        #region IDisposable Implementation

        protected bool _isDisposed;

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        #endregion

        private IRepository<UserTbl> _userTblRepository;
        public IRepository<UserTbl> UserTblRepository

        {
            get
            {

                if (this._userTblRepository == null)
                {
                    this._userTblRepository = GetRepository<UserTbl>();
                }
                return _userTblRepository;
            }
        }

        private IRepository<UserTokenTbl> _userTokenTblRepository;
        public IRepository<UserTokenTbl> UserTokenTblRepository

        {
            get
            {

                if (this._userTokenTblRepository == null)
                {
                    this._userTokenTblRepository = GetRepository<UserTokenTbl>();
                }
                return _userTokenTblRepository;
            }
        }
       

        public void Save()
        {
           _context.SaveChanges();
        }

    }
}
