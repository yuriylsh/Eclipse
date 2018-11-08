using Yuriy.Core.Model;

namespace Yuriy.Web.Services
{
    public interface IJwtService
    {
        string GetUserToken(IUser user);
    }
}