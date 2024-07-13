using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Post
{
    public class PostDetailsDTO
    {
        public int PostId { get; set; }

        public string postContant { get; set; }

        public DateTime PublishedDate { get; set; }

        public int LikesCount { get; set; }

        public int RetweetCount { get; set; }

        public int ReplyCount { get; set; }

    }
}
