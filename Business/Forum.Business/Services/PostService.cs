using Forum.Business.Entities;
using Forum.Business.Filters;
using Forum.Business.Interfaces.Repositories;
using Forum.Business.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using RaphaelSantos.Framework.Business;
using RaphaelSantos.Framework.Collections;
using System.Linq;

namespace Forum.Business.Services
{
    public class PostService : BaseService<Post>, IPostService
    {
        private IPostRepository PostRepository;

        public PostService(IPostRepository postRepository)
            : base(postRepository)
        {
            this.PostRepository = postRepository;
        }

        /// <summary>
		/// Post list
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>The filtered and sorted list of records</returns>
		public PagedList<Post> List(PostFilter filter)
        {
            var query = base.GetAll();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(i => i.Title.Contains(filter.Title));
            }

            if (filter.CategoryId > 0)
            {
                query = query.Where(i => i.CategoryId.Equals(filter.CategoryId));
            }

            var result = query.Include(x=> x.OwnerUser).Include(x=>x.Category).Include(x=>x.Comments).ThenInclude(x=> x.OwnerUser).ToPaged(filter);

            return result;
        }

        public virtual bool Update(Post obj, int userLoggedId)
        {
            var ownerId = base.GetAll().Where(x => x.Id == obj.Id).Select(x => x.OwnerUserId).FirstOrDefault();

            if (ownerId != userLoggedId)
            {
                var rule = new BrokenRule("Not allowed");
                rule.Add("Post", "Not allowed");

                throw new RuleException(rule);
            }

            return base.Update(obj);
        }
    }
}
