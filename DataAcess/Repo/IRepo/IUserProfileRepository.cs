using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        void Update(UserProfile profile);

    }
}
