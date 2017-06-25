using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface ISubTypeRepository
    {
        IQueryable<SubType> SubTypes { get; }
        SubType Get(Expression<Func<SubType, bool>> predicate);
        SubType GetById(int id);
        IQueryable<SubType> GetAll(Expression<Func<SubType, bool>> predicate);
        IQueryable<SubType> GetAll();

        SubType Add(SubType entity);
        bool Update(SubType entity);
        bool Delete(SubType entity);

        bool SaveChanges();
    }
}
