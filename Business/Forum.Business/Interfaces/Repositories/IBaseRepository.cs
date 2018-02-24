using System.Linq;

namespace Forum.Business.Interfaces.Repositories
{
    public interface IBaseRepository<Tentity> where Tentity : class
    {
        bool Add(Tentity obj);
        Tentity GetById(int id);
        IQueryable<Tentity> GetAll();
        bool Update(Tentity obj);
        bool Remove(Tentity obj);
        void Dispose();

    }
}
