using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Concrete
{
    public class GameRepository : IGameRepository
    {
        #region Init
        private IBaseRepository _repo;

        public GameRepository()
        {
            _repo = new BaseRepository();
        }
        public GameRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init

        public IQueryable<Game> Games
        {
            get { return _repo.DataContext.Games; }
        }

        public Game Get(Expression<Func<Game, bool>> predicate)
        {
            return _repo.GetFirst<Game>(predicate);
        }

        public Game GetById(int id)
        {
            return _repo.GetFirst<Game>(t => t.Id == id);
        }

        public IQueryable<Game> GetAll(Expression<Func<Game, bool>> predicate)
        {
            return _repo.GetAll<Game>(predicate);
        }
        public IQueryable<Game> GetAll()
        {
            return _repo.GetAll<Game>();
        }

        public Game Add(Game entity)
        {
            return _repo.Add<Game>(entity);
        }

        public bool Update(Game entity)
        {
            var r = _repo.GetFirst<Game>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Type = entity.Type;
            r.Statistics = entity.Statistics;
            r.Date = entity.Date;
            r.Description = entity.Description;

            return SaveChanges();
        }

        public bool Delete(Game entity)
        {
            return _repo.Delete<Game>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
