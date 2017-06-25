using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<AspNetUser> Users { get; }
        AspNetUser Get(Expression<Func<AspNetUser, bool>> predicate);
        AspNetUser GetById(Guid id);
        IQueryable<AspNetUser> GetAll(Expression<Func<AspNetUser, bool>> predicate);
        IQueryable<AspNetUser> GetAll();

        AspNetUser Add(AspNetUser entity);
        bool Update(AspNetUser entity);
        bool Delete(AspNetUser entity);

        bool SaveChanges();
    }
}
