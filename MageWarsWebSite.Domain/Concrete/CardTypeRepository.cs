using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Concrete
{
    public class CardTypeRepository : ICardTypeRepository
    {
        #region Init
        private IBaseRepository _repo;

        public CardTypeRepository()
        {
            _repo = new BaseRepository();
        }
        public CardTypeRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init

        public IQueryable<CardType> CardTypes
        {
            get { return _repo.DataContext.CardTypes; }
        }

        public CardType Get(Expression<Func<CardType, bool>> predicate)
        {
            return _repo.GetFirst<CardType>(predicate);
        }

        public CardType GetById(int id)
        {
            return _repo.GetFirst<CardType>(t => t.Id == id);
        }

        public IQueryable<CardType> GetAll(Expression<Func<CardType, bool>> predicate)
        {
            return _repo.GetAll<CardType>(predicate);
        }
        public IQueryable<CardType> GetAll()
        {
            return _repo.GetAll<CardType>();
        }

        public CardType Add(CardType entity)
        {
            return _repo.Add<CardType>(entity);
        }

        public bool Update(CardType entity)
        {
            var r = _repo.GetFirst<CardType>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Name = entity.Name;

            return SaveChanges();
        }

        public bool Delete(CardType entity)
        {
            return _repo.Delete<CardType>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
