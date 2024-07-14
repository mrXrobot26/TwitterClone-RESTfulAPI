using DataAcess.Context;
using DataAcess.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _db;
        public PostLikeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> IsPostLikedByUser(int postId, string userId)
        {
            return await _db.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.ApplicationUserId == userId);
        }

        public async Task LikePost(PostLike postLike)
        {
            await _db.PostLikes.AddAsync(postLike);

            var post = await _db.posts.FirstOrDefaultAsync(p => p.PostId == postLike.PostId);
            if (post != null)
            {
                post.LikesCount++;
            }

            await _db.SaveChangesAsync();
        }
        public async Task UnlikePost(int postId, string userId)
        {
            var postLike = await _db.PostLikes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.ApplicationUserId == userId);
            if (postLike != null)
            {
                _db.PostLikes.Remove(postLike);

                var post = await _db.posts.FirstOrDefaultAsync(p => p.PostId == postId);
                if (post != null)
                {
                    post.LikesCount--;
                }

                await _db.SaveChangesAsync();
            }
        }
    }
}
