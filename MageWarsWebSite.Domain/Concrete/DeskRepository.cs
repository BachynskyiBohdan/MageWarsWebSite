using System;
using System.Linq;
using MageWarsWebSite.Domain.Abstract;
using System.Linq.Expressions;
using MageWarsWebSite.Domain.Entities;

namespace MageWarsWebSite.Domain.Concrete
{
    public class DeskRepository : IDeskRepository
    {
        #region Init
        private IBaseRepository _repo;

        public DeskRepository()
        {
            _repo = new BaseRepository();
        }
        public DeskRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init


        public IQueryable<Desk> Desks
        {
            get { return _repo.DataContext.Desks; }
        }

        public Desk Get(Expression<Func<Desk, bool>> predicate)
        {
            return _repo.GetFirst<Desk>(predicate);
        }

        public Desk GetById(int id)
        {
            return _repo.GetFirst<Desk>(t => t.Id == id);
        }

        public IQueryable<Desk> GetAll(Expression<Func<Desk, bool>> predicate)
        {
            return _repo.GetAll<Desk>(predicate);
        }
        public IQueryable<Desk> GetAll()
        {
            return _repo.GetAll<Desk>();
        }

        public Desk Add(Desk entity)
        {
            return _repo.Add<Desk>(entity);
        }

        public bool Update(Desk entity)
        {
            var r = _repo.GetFirst<Desk>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.UserId = entity.UserId;
            r.GameId = entity.GameId;
            r.MageId = entity.MageId;
            r.Cards = entity.Cards;

            return SaveChanges();
        }

        public bool Delete(Desk entity)
        {
            return _repo.Delete<Desk>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
