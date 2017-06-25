using MageWarsWebSite.Domain.Abstract;
using MageWarsWebSite.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MageWarsWebSite.Domain.Concrete
{
    public class SchoolRepository : ISchoolRepository
    {
        #region Init
        private IBaseRepository _repo;

        public SchoolRepository()
        {
            _repo = new BaseRepository();
        }
        public SchoolRepository(IBaseRepository repo)
        {
            _repo = repo;
        }

        #endregion Init

        public IQueryable<School> Schools
        {
            get { return _repo.DataContext.Schools; }
        }

        public School Get(Expression<Func<School, bool>> predicate)
        {
            return _repo.GetFirst<School>(predicate);
        }

        public School GetById(int id)
        {
            return _repo.GetFirst<School>(t => t.Id == id);
        }

        public IQueryable<School> GetAll(Expression<Func<School, bool>> predicate)
        {
            return _repo.GetAll<School>(predicate);
        }
        public IQueryable<School> GetAll()
        {
            return _repo.GetAll<School>();
        }

        public School Add(School entity)
        {
            return _repo.Add<School>(entity);
        }

        public void Attach(School entity)
        {
            _repo.Attach(entity);
        }

        public bool Update(School entity)
        {
            var r = _repo.GetFirst<School>(t => t.Id == entity.Id);
            if (r == null) return false;

            _repo.Attach(r);

            r.Name = entity.Name;
            r.Cards = entity.Cards;

            return SaveChanges();
        }

        public bool Delete(School entity)
        {
            return _repo.Delete<School>(entity);
        }

        public bool SaveChanges()
        {
            return _repo.SaveChanges();
        }
    }
}
