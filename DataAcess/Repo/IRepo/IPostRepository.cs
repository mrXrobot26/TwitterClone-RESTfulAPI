using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetTimelinePostsAsync(string userId);
        Task UpdateAsync(Post post);


    }
}
