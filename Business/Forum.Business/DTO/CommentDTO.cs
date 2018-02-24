using System;

namespace Forum.Business.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int OwnerUserId { get; set; }
        public UserDTO OwnerUser { get; set; }

        public int PostId { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
