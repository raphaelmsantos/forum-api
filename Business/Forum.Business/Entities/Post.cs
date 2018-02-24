using System.Collections.Generic;

namespace Forum.Business.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int OwnerUserId { get; set; }
        public User OwnerUser { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}
