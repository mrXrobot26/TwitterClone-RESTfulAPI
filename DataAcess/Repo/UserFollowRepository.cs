using DataAcess.Context;
using DataAcess.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using Models.MyModels.App;
using Models.MyModels.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo
{
    public class UserFollowRepository : Repository<UserFollow>, IUserFollowRepository
    {
        private readonly ApplicationDbContext _db;

        public UserFollowRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task FollowUser(string followerId, string followedId)
        {
            if (await IsFollowing(followerId, followedId)) return;

            var userFollow = new UserFollow
            {
                FollowerUserId = followerId,
                FollowedUserId = followedId,
                FollowDate = DateTime.Now,
                 
            };

            await _db.UserFollows.AddAsync(userFollow);
            await _db.SaveChangesAsync();
        }

        public async Task UnfollowUser(string followerId, string followedId)
        {
            var userFollow = await _db.UserFollows
                                      .FirstOrDefaultAsync(uf => uf.FollowerUserId == followerId && uf.FollowedUserId == followedId);

            if (userFollow != null)
            {
                _db.UserFollows.Remove(userFollow);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> IsFollowing(string followerId, string followedId)
        {
            return await _db.UserFollows.AnyAsync(uf => uf.FollowerUserId == followerId && uf.FollowedUserId == followedId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetMutualFollowers(string userId, string otherUserId)
        {
            var userFollowers = _db.UserFollows
                                .Where(x=>x.FollowerUserId == userId)
                                .Select(x=>x.FollowedUserId);
            var otherUserFollowers = _db.UserFollows
                                  .Where(x => x.FollowerUserId == otherUserId)
                                  .Select(x => x.FollowedUserId);

            var mutualFollowers = await _db.Users
                                           .Where(u => userFollowers.Contains(u.Id) && otherUserFollowers.Contains(u.Id))
                                           .ToListAsync();
            return mutualFollowers;
        }

        public async Task<IEnumerable<ApplicationUser>> GetFollowers(string userId)
        {
            var userFollowers = await _db.UserFollows
                                .Where(x => x.FollowedUserId == userId)
                                .Select(x => x.FollowerUserId)
                                .ToListAsync();

            var userIFollowedProfile = await _db.Users
                                       .Where(x => userFollowers.Contains(x.Id))
                                       .ToListAsync();
            return userIFollowedProfile;
        }


        public int GetFollowersCount(string userId)
        {
            var userFollowersCount = _db.UserFollows
                                .Where(x => x.FollowedUserId == userId)
                                .Select(x => x.FollowerUserId)
                                .Count();

            return userFollowersCount;
        }
        
        public int GetFollowingCount(string userId)
        {
            var userFollowedCount = _db.UserFollows
                                .Where(x => x.FollowerUserId == userId)
                                .Select(x => x.FollowedUserId)
                                .Count();

            return userFollowedCount;
        }





        public async Task<IEnumerable<ApplicationUser>> GetFolloweing(string userId)
        {
            var userFollowers = await _db.UserFollows
                                .Where(x => x.FollowerUserId == userId)
                                .Select(x => x.FollowedUserId)
                                .ToListAsync();

            var userIFollowedProfile = await _db.Users
                                       .Where(x => userFollowers.Contains(x.Id))
                                       .ToListAsync();
            return userIFollowedProfile;
        }

    }
}

