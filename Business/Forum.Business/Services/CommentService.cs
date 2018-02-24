using Forum.Business.Entities;
using Forum.Business.Filters;
using Forum.Business.Interfaces.Repositories;
using Forum.Business.Interfaces.Services;
using RaphaelSantos.Framework.Business;
using RaphaelSantos.Framework.Collections;
using System.Linq;

namespace Forum.Business.Services
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        private ICommentRepository CommentRepository;

        public CommentService(ICommentRepository commentRepository)
            : base(commentRepository)
        {
            this.CommentRepository = commentRepository;
        }

        /// <summary>
		/// Comments list
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>The filtered and sorted list of records</returns>
		public PagedList<Comment> List(CommentFilter filter)
        {
            var query = base.GetAll();

            if (!string.IsNullOrWhiteSpace(filter.Content))
                query = query.Where(i => i.Content.Contains(filter.Content));

            var result = query.ToPaged(filter);

            return result;
        }

        public virtual bool Update(Comment obj, int userLoggedId)
        {
            var ownerId = base.GetAll().Where(x => x.Id == obj.Id).Select(x => x.OwnerUserId).FirstOrDefault();

            if (ownerId != userLoggedId)
            {
                var rule = new BrokenRule("Not allowed");
                rule.Add("Comment", "Not allowed");

                throw new RuleException(rule);
            }

            return base.Update(obj);
        }
    }
}
