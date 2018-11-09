using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuriy.Core.Model;
using Yuriy.Core.Repositories;

namespace Yuriy.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IUser> GetUser(int? id) => id.HasValue ? await GetUserById(id.Value) : await GetRandomUser();

        private async Task<IUser> GetUserById(int id) => await _userRepository.GetById(id);

        private async Task<IUser> GetRandomUser() => (await _userRepository.GetAll()).First();

        public async Task<IEnumerable<IUser>> GetAll() => await _userRepository.GetAll();
    }
}