using Forum.Business.Entities;
using Forum.Business.Filters;
using RaphaelSantos.Framework.Collections;

namespace Forum.Business.Interfaces.Services
{
    public interface IPostService : IBaseService<Post>
    {
        PagedList<Post> List(PostFilter filter);
        bool Update(Post obj, int userId);
    }
}
