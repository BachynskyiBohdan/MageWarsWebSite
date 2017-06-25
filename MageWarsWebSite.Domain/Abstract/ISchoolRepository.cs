using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface ISchoolRepository
    {
        IQueryable<School> Schools { get; }
        School Get(Expression<Func<School, bool>> predicate);
        School GetById(int id);
        IQueryable<School> GetAll(Expression<Func<School, bool>> predicate);
        IQueryable<School> GetAll();

        School Add(School entity);
        void Attach(School entity);
        bool Update(School entity);
        bool Delete(School entity);

        bool SaveChanges();
    }
}
