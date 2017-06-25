using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        #region Init
        private IBaseRepository _repo;

        public UserRepository()
        {
            _repo = new BaseRepository();
        }
        public UserRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init

        public IQueryable<AspNetUser> Users
        {
            get { return _repo.DataContext.Users; }
        }

        public AspNetUser Get(Expression<Func<AspNetUser, bool>> predicate)
        {
            return _repo.GetFirst<AspNetUser>(predicate);
        }

        public AspNetUser GetById(Guid id)
        {
            return _repo.GetFirst<AspNetUser>(t => t.Id == id);
        }

        public IQueryable<AspNetUser> GetAll(Expression<Func<AspNetUser, bool>> predicate)
        {
            return _repo.GetAll<AspNetUser>(predicate);
        }
        public IQueryable<AspNetUser> GetAll()
        {
            return _repo.GetAll<AspNetUser>();
        }

        public AspNetUser Add(AspNetUser entity)
        {
            return _repo.Add<AspNetUser>(entity);
        }

        public bool Update(AspNetUser entity)
        {
            var r = _repo.GetFirst<AspNetUser>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Update(entity);

            return SaveChanges();
        }

        public bool Delete(AspNetUser entity)
        {
            return _repo.Delete<AspNetUser>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
