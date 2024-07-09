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
        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; }

    }
}