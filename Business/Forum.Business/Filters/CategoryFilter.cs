using Forum.Business.Entities;
using RaphaelSantos.Framework.Collections;

namespace Forum.Business.Filters
{
    public class CategoryFilter : EntityFilter<Category>
    {
        public CategoryFilter()
        {
            MapSort("Name", i => i.Name);
            MapSort("Active", i => i.Active);

            SortField = "Name";
        }

        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
