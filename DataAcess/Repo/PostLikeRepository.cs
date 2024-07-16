using DataAcess.Context;
using DataAcess.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using Models.MyModels.App;
using Models.MyModels.PostFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo
{
    public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
    {
        private readonly ApplicationDbContext db;
        public PostLikeRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersWhoLikedPost(int PostId)
        {
            return await db.PostLikes
                           .Where(x=> x.PostId == PostId)
                           .Select(x=>x.ApplicationUser)
                           .ToListAsync();
        }

        public async Task<bool> IsPostLikedByUser(int postId, string userId)
        {
            return await db.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.ApplicationUserId == userId);
        }

        public async Task LikePost(PostLike postLike)
        {
            await db.PostLikes.AddAsync(postLike);

            var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postLike.PostId);
            if (post != null)
            {
                post.LikesCount++;
            }

            await db.SaveChangesAsync();
        }
        public async Task UnlikePost(int postId, string userId)
        {
            var postLike = await db.PostLikes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.ApplicationUserId == userId);
            if (postLike != null)
            {
                db.PostLikes.Remove(postLike);

                var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
                if (post != null)
                {
                    post.LikesCount--;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
