using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Yuriy.Core.Model;

namespace Yuriy.Web.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly SigningCredentials _signinCredentials;

        public JwtService(SecurityKey securityKey)
        {
            _signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string GetUserToken(IUser user)
        {
            var claims = new [] { new Claim("Id", user.Id.ToString("0")) };
            return _jwtSecurityTokenHandler.WriteToken(new JwtSecurityToken(
                claims: claims,
                signingCredentials: _signinCredentials));
        }
    }
}