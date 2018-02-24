using Forum.Business.Interfaces.Models;
using System.Linq;

namespace Forum.Business.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class, IActivable
    {
        bool Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        bool Update(TEntity obj);
        bool Remove(int id);
        void Dispose();
        bool Activate(int id);
        bool Deactivate(int id);
    }
}
