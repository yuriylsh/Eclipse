using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuriy.Core.Model;
using Yuriy.Core.Services;
using Yuriy.Web.Services;
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

        [HttpGet("{id}/subscriptions")]
        public async Task<IActionResult> GetUserSubscriptions(int userId)
            => await IfValidUserId(userId, id => _subscriptionService.GetUserNotificationSubscriptions(id));
        

        private async Task<IActionResult> IfValidUserId<TResult>(int userId, Func<int, Task<TResult>> ifValid) 
            => await this.IfInputIsValid(userId, ControllerContext.HttpContext.ValidateAgainstJwt, ifValid, new ForbidResult());

    }
}