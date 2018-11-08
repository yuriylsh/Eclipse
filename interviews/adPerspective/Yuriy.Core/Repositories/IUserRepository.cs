using System.Collections.Generic;
using System.Threading.Tasks;
using Yuriy.Core.Model;

namespace Yuriy.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> GetById(int id);
        Task<IEnumerable<IUser>> GetAll();
    }
}