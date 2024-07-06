using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UserProfileDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public string Birthday { get; set; }
        public DateTime JoinTime { get; set; }
        public string Gender { get; set; }

        public List<PostDTO> Posts { get; set; }

    }
}
