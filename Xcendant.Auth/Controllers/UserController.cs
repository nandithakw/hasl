using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.Web.Http;
using Xcendent.Auth.ViewModels;

namespace Xcendent.Auth.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        [ClaimsPrincipalPermission(SecurityAction.Demand,Resource ="user",Operation ="get")]
        [Route("LoggedUserInfo")]
        public object GetLoggedInUserInfo()
        {
            LoginData loginData = LoginData.FromIdentity(User.Identity as ClaimsIdentity);
            return loginData;
        }
    }
}
