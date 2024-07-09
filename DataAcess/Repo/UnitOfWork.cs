using AutoMapper;
using DataAcess.Context;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models.MyModels.App;
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
        public IUserRepository User { get; private set; }

        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            Profile = new UserProfileRepository(_db, _mapper);
            Post = new PostRepository(_db, _mapper);
            User = new UserRepository(db, configuration, userManager, mapper, roleManager);
        }

        public IUserProfileRepository Profile { get; private set; }
        public IPostRepository Post { get; private set; }


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
