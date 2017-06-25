using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Concrete
{
    public class SubTypeRepository : ISubTypeRepository
    {
        #region Init
        private IBaseRepository _repo;

        public SubTypeRepository()
        {
            _repo = new BaseRepository();
        }
        public SubTypeRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init

        public IQueryable<SubType> SubTypes
        {
            get { return _repo.DataContext.SubTypes; }
        }

        public SubType Get(Expression<Func<SubType, bool>> predicate)
        {
            return _repo.GetFirst<SubType>(predicate);
        }

        public SubType GetById(int id)
        {
            return _repo.GetFirst<SubType>(t => t.Id == id);
        }

        public IQueryable<SubType> GetAll(Expression<Func<SubType, bool>> predicate)
        {
            return _repo.GetAll<SubType>(predicate);
        }
        public IQueryable<SubType> GetAll()
        {
            return _repo.GetAll<SubType>();
        }

        public SubType Add(SubType entity)
        {
            return _repo.Add<SubType>(entity);
        }

        public bool Update(SubType entity)
        {
            var r = _repo.GetFirst<SubType>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Name = entity.Name;
            r.Cards = entity.Cards;

            return SaveChanges();
        }

        public bool Delete(SubType entity)
        {
            return _repo.Delete<SubType>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
