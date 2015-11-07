using System.Security.Claims;
using System.Web.Http;
using Xcendent.Auth.ViewModels;

namespace Xcendent.Auth.Controllers
{
    public class UserController : ApiController
    {
        [Authorize]
        [Route("LoggedUserInfo")]
        public object GetLoggedInUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            return externalLogin;
        }
    }
}
