﻿using System;
using System.Linq;
using System.Linq.Expressions;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;

namespace MageWarsWebSite.Domain.Concrete
{
    public class BaseRepository : IBaseRepository
    {
        public EFDbContext DataContext { get; private set; }

        public BaseRepository()
        {
            DataContext = new EFDbContext();
            //DataContext.Database.Log = Logger.Log;
        }

        public TEntity GetFirst<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            try
            {
                return DataContext.Set<TEntity>().First(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DataContext.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return DataContext.Set<TEntity>().AsQueryable();
        }

        public TEntity Attach<TEntity>(TEntity entity) where TEntity : class
        {
            return DataContext.Set<TEntity>().Attach(entity);
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                DataContext.Set<TEntity>().Add(entity);
                var b = SaveChanges();
                if (b)
                    return entity;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                DataContext.Set<TEntity>().Remove(entity);
                return SaveChanges();
            }
            catch
            {
                return false;
            }
        }

        public bool SaveChanges()
        {
            var result = 0;
            try
            {
                result = DataContext.SaveChanges();
            }
            catch (Exception e)
            {
               // Logger.Log(e.Message);
            }
            return result > 0;
        }

        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }

    }
}
