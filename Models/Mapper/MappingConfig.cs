using AutoMapper;
using Models.DTOs;
using Models.DTOs.AppUsers;
using Models.DTOs.Post;
using Models.DTOs.User;
using Models.MyModels.App;
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
            CreateMap<ApplicationUser, UserProfileForGetAllUsersDTO>()
                      .ForMember(userDto => userDto.UserID, user => user.MapFrom(x => x.Id));
            CreateMap<ApplicationUserToAddUserDTO, ApplicationUser>().ReverseMap();
            CreateMap<PostDTO, Post>().ReverseMap();
            CreateMap<PostDetailsDTO, Post>().ReverseMap();
            CreateMap<PostUpdatesDTO, Post>().ReverseMap();
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();

        }

    }
}
