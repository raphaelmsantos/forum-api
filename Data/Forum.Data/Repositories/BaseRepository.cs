using Forum.Business.Interfaces.Repositories;
using Forum.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Forum.Data.Repositories
{
    public class BaseRepository<Tentity> : IDisposable, IBaseRepository<Tentity> where Tentity : class
    {
        private readonly ApplicationContext Db;

        public BaseRepository(ApplicationContext dbContext)
        {
            Db = dbContext;
        }

        public bool Add(Tentity obj)
        {
            Db.Set<Tentity>().Add(obj);

            int affected = Db.SaveChanges();
            return affected > 0;

        }

        public Tentity GetById(int id)
        {
            return Db.Set<Tentity>().Find(id);
        }

        public IQueryable<Tentity> GetAll()
        {
            return Db.Set<Tentity>();
        }

        public bool Update(Tentity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;

            int affected = Db.SaveChanges();
            return affected > 0;

        }

        public bool Remove(Tentity obj)
        {
            Db.Set<Tentity>().Remove(obj);

            int affected = Db.SaveChanges();
            return affected > 0;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
