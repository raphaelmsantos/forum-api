using Forum.Business.Entities;
using Forum.Business.Filters;
using RaphaelSantos.Framework.Collections;

namespace Forum.Business.Interfaces.Services
{
    public interface ICommentService : IBaseService<Comment>
    {
        PagedList<Comment> List(CommentFilter filter);
        bool Update(Comment obj, int userId);
    }
}
