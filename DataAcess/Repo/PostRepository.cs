using AutoMapper;
using DataAcess.Context;
using DataAcess.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PostRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task UpdateAsync(Post post)
        {
            var postFromDb = await _db.posts.FirstOrDefaultAsync(x => x.PostId == post.PostId);
            if (postFromDb != null)
            {
                _mapper.Map(post, postFromDb);
                await _db.SaveChangesAsync();
            }
        }

    }
}
