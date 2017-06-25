using System;
using System.Linq;
using System.Linq.Expressions;
using MageWarsWebSite.Domain.Entities;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface IBaseRepository : IDisposable
    {
        EFDbContext DataContext { get; }

        TEntity GetFirst<TEntity> (Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IQueryable<TEntity> GetAll<TEntity> (Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

        TEntity Attach<TEntity>(TEntity entity) where TEntity : class;
        TEntity Add<TEntity> (TEntity entity) where TEntity : class;
        bool Delete<TEntity>(TEntity entity) where TEntity : class;

        bool SaveChanges();
    }
}
