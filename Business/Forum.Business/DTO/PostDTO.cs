using System;
using System.Collections.Generic;

namespace Forum.Business.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime InsertDate { get; set; }

        public IList<CommentDTO> Comments { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

        public int OwnerUserId { get; set; }
        public UserDTO OwnerUser { get; set; }
    }
}
