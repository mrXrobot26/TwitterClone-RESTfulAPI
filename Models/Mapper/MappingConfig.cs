using AutoMapper;
using Models.DTOs;
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

            CreateMap<UserProfileDTO, UserProfile>().ReverseMap();
            CreateMap<UserProfile, UserProfileForGetAllUsersDTO>()
                       .ForMember(userDto => userDto.UserID, user => user.MapFrom(x => x.Id));
            CreateMap<PostDTO, Post>().ReverseMap();
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();

        }

    }
}
