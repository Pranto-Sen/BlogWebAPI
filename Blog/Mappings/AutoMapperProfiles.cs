using AutoMapper;
using Blog.DTOs;
using Blog.Models;

namespace Blog.Mappings
{
	public class AutoMapperProfiles:Profile
	{
        public AutoMapperProfiles()
        {
            CreateMap<UserDTO,User>().ReverseMap();
			CreateMap<PostDTO, Post>().ReverseMap();
			CreateMap<CommentDTO, Comment>().ReverseMap();
			CreateMap<UserPostDTO, User>().ReverseMap();
			CreateMap<PostCommentDTO, Post>().ReverseMap();
		}
    }
}
