using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Forum.Business.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }

        public IList<Post> Posts { get; set; }
        public IList<Comment> Comments { get; set; }
    }    
}
