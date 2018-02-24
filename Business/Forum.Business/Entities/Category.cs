using System.Collections.Generic;

namespace Forum.Business.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public IList<Post> Posts { get; set; }
    }
}
