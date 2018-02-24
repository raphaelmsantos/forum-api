using Forum.Business.Entities;
using RaphaelSantos.Framework.Collections;

namespace Forum.Business.Filters
{
    public class CommentFilter : EntityFilter<Comment>
    {
        public CommentFilter()
        {
            MapSort("Content", i => i.Content);
            MapSort("Active", i => i.Active);

            SortField = "Content";
        }

        public string Content { get; set; }
        public bool Active { get; set; }
    }
}
