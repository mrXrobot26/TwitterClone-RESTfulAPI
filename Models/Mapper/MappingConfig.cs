using AutoMapper;
using Models.DTOs;
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
            CreateMap<UserProfileDTO, UserProfile>();
            CreateMap<PostDTO, Post>();
        }

    }
}
