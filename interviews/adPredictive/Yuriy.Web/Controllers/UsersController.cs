using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuriy.Core.Model;
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
        private readonly ISubscriptionService _subscriptionService;

        public UsersController(IUserService userService, ISubscriptionService subscriptionService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users.Select(user => new UserViewModel(user.Id, user.FirstName, user.LastName)));
        }

        [HttpGet("{userId}/subscriptions")]
        public async Task<IActionResult> GetUserSubscriptions(int userId)
            => await this.IfValidUserId(userId, () => _subscriptionService.GetUserNotificationSubscriptions(userId));

        [HttpPut("{userId}/subscriptions")]
        public async Task<IActionResult> UpdateUserSubscriptions(int userId, [FromBody] SubscriptionUpdate[] updates)
            => await this.IfValidUserId(userId, async () =>
            {
                if (!await _subscriptionService.ValidateUpdates(updates)) return BadRequest();
                await _subscriptionService.ApplyUpdates(userId, updates);
                return Ok();
            });
    }
}