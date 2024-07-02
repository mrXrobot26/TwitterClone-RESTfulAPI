using AutoMapper;
using DataAcess.Context;
using DataAcess.Repo.IRepo;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserProfileRepository(ApplicationDbContext db , IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Update(UserProfile userProfile)
        {
            var objFromDb =  _db.profiles.FirstOrDefault(x=> x.Id == userProfile.Id);
            if (objFromDb != null)
            {
                _mapper.Map(userProfile, objFromDb);
                _db.SaveChanges();
            }
        }
    }

}
