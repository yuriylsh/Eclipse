using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Yuriy.Web.Services.UserIdValidation;

namespace Yuriy.Web
{
    public static class ControllerExtensions
    {
        public static async Task<IActionResult> IfInputIsValid<T, TResult>(this ControllerBase controller,
            T input,
            Predicate<T> validation,
            Func<T, Task<TResult>> ifValid,
            IActionResult ifInvalid)
        {
            if (validation(input))
            {
                return controller.Ok(await ifValid(input));
            }
            return ifInvalid;
        }

        public static async Task<IActionResult> IfValidUserId<TResult>(this ControllerBase controller,
            int userId, 
            Func<int, Task<TResult>> ifValid) 
            => await IfInputIsValid(controller, userId, controller.ControllerContext.HttpContext.ValidateAgainstJwt, ifValid, new ForbidResult());

    }
}