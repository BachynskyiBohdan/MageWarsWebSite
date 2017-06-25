using System;
using System.Linq;
using System.Linq.Expressions;
using MageWarsWebSite.Domain.Entities;

namespace MageWarsWebSite.Domain.Abstract
{
    public interface ICardRepository
    {
        IQueryable<Card> Cards { get; }
        Card Get(Expression<Func<Card, bool>> predicate);
        Card GetById(int id);
        IQueryable<Card> GetAll(Expression<Func<Card, bool>> predicate);
        IQueryable<Card> GetAll();

        Card Add(Card entity);
        bool Update(Card entity);
        bool Delete(Card entity);

        bool SaveChanges();
    }
}
