using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface IMageRepository
    {
        IQueryable<Mage> Mages { get; }
        Mage Get(Expression<Func<Mage, bool>> predicate);
        Mage GetById(int id);
        IQueryable<Mage> GetAll(Expression<Func<Mage, bool>> predicate);
        IQueryable<Mage> GetAll();

        Mage Add(Mage entity);
        bool Update(Mage entity);
        bool Delete(Mage entity);

        bool SaveChanges();
    }
}
