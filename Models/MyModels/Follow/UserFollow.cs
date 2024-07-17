using Models.MyModels.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MyModels.Follow
{
    public class UserFollow
    {
        public int Id { get; set; }

        [ForeignKey("FollowerUser")]
        public string FollowerUserId { get; set; } // اللي بيتابع
        public ApplicationUser FollowerUser { get; set; }

        [ForeignKey("FollowedUser")]
        public string FollowedUserId { get; set; } // اللي توبع
        public ApplicationUser FollowedUser { get; set; }
        public DateTime FollowDate { get; set; }

    }
}
