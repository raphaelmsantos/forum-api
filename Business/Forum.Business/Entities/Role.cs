using Microsoft.AspNetCore.Identity;

namespace Forum.Business.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role() { }
        public Role(string name)
         : this()
        {
            this.Name = name;
        }
    }
}
