using System;
using System.Linq;
using System.Linq.Expressions;
using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;

namespace MageWarsWebSite.Domain.Concrete
{
    public class CardRepository : ICardRepository
    {
        #region Init
        private IBaseRepository _repo;

        public CardRepository()
        {
            _repo = new BaseRepository();
        }
        public CardRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init

        public IQueryable<Card> Cards
        {
            get { return _repo.DataContext.Cards; }
        }

        public Card Get(Expression<Func<Card, bool>> predicate)
        {
            return _repo.GetFirst<Card>(predicate);
        }

        public Card GetById(int id)
        {
            return _repo.GetFirst<Card>(t => t.Id == id);
        }

        public IQueryable<Card> GetAll(Expression<Func<Card, bool>> predicate)
        {
            return _repo.GetAll<Card>(predicate);
        }
        public IQueryable<Card> GetAll()
        {
            return _repo.GetAll<Card>();
        }

        public Card Add(Card entity)
        {
            return _repo.Add<Card>(entity);
        }

        public bool Update(Card entity)
        {
            var r = _repo.GetFirst<Card>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Update(entity);

            return SaveChanges();
        }

        public bool Delete(Card entity)
        {
            return _repo.Delete<Card>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
