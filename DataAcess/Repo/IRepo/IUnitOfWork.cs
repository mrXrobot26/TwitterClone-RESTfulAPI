using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repo.IRepo
{
    public interface IUnitOfWork
    {
        IPostRepository Post { get; }
        IUserRepository User { get; }
        IPostLikeRepository PostLike { get; }
        Task SaveAsync();
    }
}
