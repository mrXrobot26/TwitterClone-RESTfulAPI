using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IUnitOfWork
    {
        IUserProfileRepository Profile { get; }
        IPostRepository Post { get; }
        Task SaveAsync();
    }
}
