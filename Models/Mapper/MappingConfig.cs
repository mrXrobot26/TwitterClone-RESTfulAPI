using AutoMapper;
using Models.DTOs;
using Models.DTOs.AppUsers;
using Models.DTOs.Follower;
using Models.DTOs.Post;
using Models.DTOs.PostComment;
using Models.DTOs.TimeLine;
using Models.DTOs.User;
using Models.MyModels.App;
using Models.MyModels.PostFolder;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Mapper
{
    public class MappingConfig : AutoMapper.Profile
    {
        public MappingConfig() 
        {

            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUserToAddUserDTO, ApplicationUser>().ReverseMap();
            CreateMap<PostDTO, Post>().ReverseMap();
            CreateMap<PostDetailsDTO, Post>().ReverseMap();
            CreateMap<PostUpdatesDTO, Post>().ReverseMap();
            CreateMap<PostComment, PostCommentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName)).ReverseMap();
            CreateMap<PostComment, AddPostCommentDTO>().ReverseMap();
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();

            CreateMap<ApplicationUser, UserLikeDTO>()
                .ForMember(dest=>dest.UserID , opt=> opt.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.UserName , opt=> opt.MapFrom(src=>src.UserName)).ReverseMap();

            CreateMap<ApplicationUser, FollowerDTO>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();



            CreateMap<Post, TimeLineDTO>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
           .ForMember(dest => dest.PostContent, opt => opt.MapFrom(src => src.postContant)).ReverseMap();

        }

    }
}
