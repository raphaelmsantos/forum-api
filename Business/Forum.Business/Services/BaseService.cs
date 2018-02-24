using Forum.Business.Interfaces.Models;
using Forum.Business.Interfaces.Repositories;
using Forum.Business.Interfaces.Services;
using System;
using System.Linq;

namespace Forum.Business.Services
{
    public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class, IActivable
    {
        private readonly IBaseRepository<TEntity> BaseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this.BaseRepository = baseRepository;
        }

        public virtual bool Add(TEntity obj)
        {
            return BaseRepository.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return BaseRepository.GetById(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return BaseRepository.GetAll();
        }

        public virtual bool Update(TEntity obj)
        {
            return BaseRepository.Update(obj);
        }

        public bool Remove(int id)
        {
            var entity = GetById(id);

            if (entity == null)
                return false;

            return BaseRepository.Remove(entity);
        }

        public bool Activate(int id)
        {
            var entity = GetById(id);

            if (entity == null)
                return false;

            entity.Active = true;

            return Update(entity);
        }

        public bool Deactivate(int id)
        {
            var entity = GetById(id);

            if (entity == null)
                return false;

            entity.Active = false;

            return Update(entity);

        }

        public void Dispose()
        {
            BaseRepository.Dispose();
        }
    }
}
