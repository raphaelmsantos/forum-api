namespace Forum.Business.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public int OwnerUserId { get; set; }
        public User OwnerUser { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
