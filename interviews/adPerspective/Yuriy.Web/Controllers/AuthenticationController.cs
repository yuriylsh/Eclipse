using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yuriy.Core.Services;
using Yuriy.Web.Services;

namespace Yuriy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthenticationController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm]int? id)
        {
            var user = await _userService.GetUser(id);
            return Ok(new {id = user.Id, token = _jwtService.GetUserToken(user)});
        }
    }
}
