using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuriy.Core.Services;
using Yuriy.Web.ViewModels;

namespace Yuriy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users.Select(user => new UserViewModel(user.Id, user.FirstName, user.LastName)));
        }
    }
}