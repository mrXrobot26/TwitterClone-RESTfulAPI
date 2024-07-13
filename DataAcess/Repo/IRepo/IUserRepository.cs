using Models.DTOs.User;
using Models.MyModels.App;
using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<bool> IsUniqueUserName(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterRequestDTO registerRequestDTO);
    }
}

