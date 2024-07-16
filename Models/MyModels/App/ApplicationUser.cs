using Microsoft.AspNetCore.Identity;
using Models.MyModels.PostFolder;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MyModels.App
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Bio { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime JoinTime { get; set; }
        public string Gender { get; set; }
        //  [JsonIgnore]
        public List<Post> Posts { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostComment> PostComments { get; set; }

    }
}
