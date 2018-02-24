using Forum.Business.Entities;
using Forum.Business.Interfaces.Repositories;
using Forum.Data.Contexts;

namespace Forum.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
