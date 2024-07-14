using Models.MyModels.App;
using Models.MyModels.PostFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.MyModels.ProfileModels
{
    public class Post
    {
        public int PostId { get; set; }
        public string postContant { get; set; }
        public DateTime PublishedDate { get; set; }
        public int LikesCount { get; set; }
        public int RetweetCount { get; set; }
        public int ReplyCount { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostComment> PostComments { get; set; }

    }
}