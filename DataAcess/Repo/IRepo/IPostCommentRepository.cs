using Models.MyModels.PostFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IPostCommentRepository : IRepository<PostComment>
    {
        Task AddCommentAsync(PostComment postComment);
        Task DeleteCommentAsync(int commentId, string userId);
    }
}
