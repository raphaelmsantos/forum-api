using Forum.Business.Entities;
using Forum.Business.Interfaces.Repositories;
using Forum.Data.Contexts;

namespace Forum.Data.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {
        }
    }
}
