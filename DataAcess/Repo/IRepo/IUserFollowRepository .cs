using Models.MyModels.App;
using Models.MyModels.Follow;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IUserFollowRepository : IRepository<UserFollow>
    {
        Task FollowUser(string followerId, string followedId);
        Task UnfollowUser(string followerId, string followedId);
        Task<bool> IsFollowing(string followerId, string followedId);
        Task<IEnumerable<ApplicationUser>> GetMutualFollowers(string userId, string otherUserId);
        Task<IEnumerable<ApplicationUser>> GetFollowers(string userId);
        Task<IEnumerable<ApplicationUser>> GetFolloweing(string userId);
        int GetFollowingCount(string userId);
        int GetFollowersCount(string userId);
    }
}
