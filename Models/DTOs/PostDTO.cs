using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string postContant { get; set; }
        public DateTime PublishedDate { get; set; }
        public int LikesCount { get; set; }
        public int RetweetCount { get; set; }
        public int ReplyCount { get; set; }
        public int ProfileId { get; set; }

    }
}
