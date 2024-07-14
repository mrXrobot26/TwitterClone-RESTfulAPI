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
    public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
    {
        private readonly ApplicationDbContext _db;

        public PostCommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddCommentAsync(PostComment postComment)
        {
            await _db.PostComments.AddAsync(postComment);

            var post = await _db.Posts.FirstOrDefaultAsync(p => p.PostId == postComment.PostId);
            if (post != null)
            {
                post.ReplyCount++;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            var postComment = await _db.PostComments.FirstOrDefaultAsync(pc => pc.Id == commentId && pc.ApplicationUserId == userId);
            if (postComment == null)
            {
                throw new KeyNotFoundException("Comment not found or user is not authorized to delete this comment.");
            }

            var post = await _db.Posts.FirstOrDefaultAsync(p => p.PostId == postComment.PostId);
            if (post != null)
            {
                _db.PostComments.Remove(postComment);
                post.ReplyCount--;
                await _db.SaveChangesAsync();
            }
        }
    }
}

