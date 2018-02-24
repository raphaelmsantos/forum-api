using Forum.Business.Entities;
using Forum.Business.Filters;
using RaphaelSantos.Framework.Collections;

namespace Forum.Business.Interfaces.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        PagedList<Category> List(CategoryFilter filter);
    }
}
