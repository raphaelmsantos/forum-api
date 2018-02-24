using AutoMapper;
using Forum.Business.DTO;
using Forum.Business.Entities;

namespace Forum.Api.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();            
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
