using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Yuriy.Web.Services
{
    public static class UserIdValidation
    {
        public static bool ValidateAgainstJwt(this HttpContext context, int userId) 
            => TryGetUserIdFromJwt(context, out var idFromJwt) && userId != idFromJwt;

        private static bool TryGetUserIdFromJwt(HttpContext httpContext, out int id)
        {
            var idClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtService.IdClaim);
            if(idClaim != null && int.TryParse(idClaim.Value, out int parsedId))
            {
                id = parsedId;
                return true;
            }
            id = -1;
            return false;
        }
    }
}