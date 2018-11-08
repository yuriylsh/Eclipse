using System.Collections.Generic;
using System.Threading.Tasks;
using Yuriy.Core.Model;

namespace Yuriy.Core.Services
{
    public interface IUserService
    {
        Task<IUser> GetUser(int? id);
        Task<IEnumerable<IUser>> GetAll();
    }
}