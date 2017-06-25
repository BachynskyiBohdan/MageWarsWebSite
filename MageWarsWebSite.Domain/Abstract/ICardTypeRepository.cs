using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface ICardTypeRepository
    {
        IQueryable<CardType> CardTypes { get; }
        CardType Get(Expression<Func<CardType, bool>> predicate);
        CardType GetById(int id);
        IQueryable<CardType> GetAll(Expression<Func<CardType, bool>> predicate);
        IQueryable<CardType> GetAll();

        CardType Add(CardType entity);
        bool Update(CardType entity);
        bool Delete(CardType entity);

        bool SaveChanges();
    }
}
