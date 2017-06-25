using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface IGameRepository
    {
        IQueryable<Game> Games { get; }
        Game Get(Expression<Func<Game, bool>> predicate);
        Game GetById(int id);
        IQueryable<Game> GetAll(Expression<Func<Game, bool>> predicate);
        IQueryable<Game> GetAll();

        Game Add(Game entity);
        bool Update(Game entity);
        bool Delete(Game entity);

        bool SaveChanges();
    }
}
