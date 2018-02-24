using Forum.Business.Entities;
using Forum.Business.Interfaces.Repositories;
using Forum.Data.Contexts;

namespace Forum.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
