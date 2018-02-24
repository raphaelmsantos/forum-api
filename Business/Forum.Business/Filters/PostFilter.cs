using Forum.Business.Entities;
using RaphaelSantos.Framework.Collections;

namespace Forum.Business.Filters
{
    public class PostFilter : EntityFilter<Post>
    {
        public PostFilter()
        {
            MapSort("Title", i => i.Title);
            MapSort("Active", i => i.Active);
            MapSort("CategoryId", i => i.CategoryId);

            SortField = "Title";
        }

        public string Title { get; set; }
        public bool Active { get; set; }
        public int CategoryId { get; set; }
    }
}
