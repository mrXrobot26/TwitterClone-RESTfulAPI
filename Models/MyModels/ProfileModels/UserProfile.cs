using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.MyModels.ProfileModels
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public string Birthday { get; set; }
        public DateTime JoinTime { get; set; }
        public string Gender { get; set; }
      //  [JsonIgnore]
        public List<Post> Posts { get; set; }

    }
}
