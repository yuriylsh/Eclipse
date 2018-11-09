using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yuriy.Core.Model;
using Yuriy.Core.Repositories;
using Yuriy.Web.Data;

namespace Yuriy.Web.Repositories
{
    public sealed class UserRepository: IUserRepository
    {
        private readonly AdPredictiveContext _context;

        public UserRepository(AdPredictiveContext context)
        {
            _context = context;
        }

        public async Task<IUser> GetById(int id) => await _context.FindAsync<User>(id);

        public async Task<IEnumerable<IUser>> GetAll() => await _context.User.ToArrayAsync();
    }
}