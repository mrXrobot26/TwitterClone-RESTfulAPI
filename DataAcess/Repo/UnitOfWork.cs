using AutoMapper;
using DataAcess.Context;
using DataAcess.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UnitOfWork(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            Profile = new UserProfileRepository(_db, _mapper);
            Post = new PostRepository(_db, _mapper);
        }

        public IUserProfileRepository Profile { get; private set; }
        public IPostRepository Post { get; private set; }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
