using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Concrete
{
    public class MageRepository : IMageRepository
    {
        #region Init
        private IBaseRepository _repo;

        public MageRepository()
        {
            _repo = new BaseRepository();
        }
        public MageRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init


        public IQueryable<Mage> Mages
        {
            get { return _repo.DataContext.Mages; }
        }

        public Mage Get(Expression<Func<Mage, bool>> predicate)
        {
            return _repo.GetFirst<Mage>(predicate);
        }

        public Mage GetById(int id)
        {
            return _repo.GetFirst<Mage>(t => t.Id == id);
        }

        public IQueryable<Mage> GetAll(Expression<Func<Mage, bool>> predicate)
        {
            return _repo.GetAll<Mage>(predicate);
        }
        public IQueryable<Mage> GetAll()
        {
            return _repo.GetAll<Mage>();
        }

        public Mage Add(Mage entity)
        {
            return _repo.Add<Mage>(entity);
        }

        public bool Update(Mage entity)
        {
            var r = _repo.GetFirst<Mage>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Update(entity);

            return SaveChanges();
        }

        public bool Delete(Mage entity)
        {
            return _repo.Delete<Mage>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
