using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Yuriy.Web.Services.UserIdValidation;

namespace Yuriy.Web
{
    public static class ControllerExtensions
    {
        public static async Task<IActionResult> IfValidUserId<TResult>(this ControllerBase controller,
            int userId, 
            Func<Task<TResult>> ifValid) 
            => await IfInputIsValid(controller, userId, controller.ControllerContext.HttpContext.ValidateAgainstJwt, ifValid, new ForbidResult());

        public static async Task<IActionResult> IfInputIsValid<T, TResult>(this ControllerBase controller,
            T input,
            Predicate<T> validation,
            Func<Task<TResult>> ifValid,
            IActionResult ifInvalid) 
            => await IfInputIsValid<T>(controller, input, validation, async () => controller.Ok(await ifValid()), ifInvalid);

        public static async Task<IActionResult> IfValidUserId(this ControllerBase controller,
            int userId, 
            Func<Task<IActionResult>> ifValid) 
            => await IfInputIsValid(controller, userId, controller.ControllerContext.HttpContext.ValidateAgainstJwt, ifValid, new ForbidResult());

        public static async Task<IActionResult> IfInputIsValid<T>(this ControllerBase controller,
            T input,
            Predicate<T> validation,
            Func<Task<IActionResult>> ifValid,
            IActionResult ifInvalid)
        {
            if (validation(input))
            {
                return await ifValid();
            }
            return ifInvalid;
        }
    }
}