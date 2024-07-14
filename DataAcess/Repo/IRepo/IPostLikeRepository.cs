using Models.MyModels.PostFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IPostLikeRepository : IRepository<PostLike>
    {
        Task<bool> IsPostLikedByUser(int postId, string userId);
        Task LikePost(PostLike postLike);
        Task UnlikePost(int postId, string userId);
    }
}
