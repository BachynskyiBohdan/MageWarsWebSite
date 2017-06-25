using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface IDeskRepository
    {
        IQueryable<Desk> Desks { get; }
        Desk Get(Expression<Func<Desk, bool>> predicate);
        Desk GetById(int id);
        IQueryable<Desk> GetAll(Expression<Func<Desk, bool>> predicate);
        IQueryable<Desk> GetAll();

        Desk Add(Desk entity);
        bool Update(Desk entity);
        bool Delete(Desk entity);

        bool SaveChanges();
    }
}
