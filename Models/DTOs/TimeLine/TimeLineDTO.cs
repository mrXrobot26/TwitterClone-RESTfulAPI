using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.TimeLine
{
    public class TimeLineDTO
    {
        public int PostId { get; set; }
        public string PostContent { get; set; }
        public DateTime PublishedDate { get; set; }
        public int LikesCount { get; set; }
        public int RetweetCount { get; set; }
        public int ReplyCount { get; set; }
        public string UserName { get; set; }
    }
}
