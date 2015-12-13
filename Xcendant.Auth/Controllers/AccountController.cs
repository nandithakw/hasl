using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Xcendant.Auth.Models.Entities;
using Xcendant.Auth.Models.Managers;
using Xcendant.Auth.Providers;
using Xcendant.Auth.Results;
using Xcendant.Auth.ViewModels;
using Xcendent.Auth.ViewModels;
using Xcendent.Auth.Extensions;
namespace Xcendant.Auth.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private AbstractXcendentUserManager<XcendentUser> userManager;

        

        public AccountController(AbstractXcendentUserManager<XcendentUser> userManager)
        {
            UserManager = userManager;
        }

        public AbstractXcendentUserManager<XcendentUser> UserManager
        {
            get
            {
                return userManager;
            }
            set
            {
                userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserViewModel userViewModel)
        {

            var user = new XcendentUser() { UserName = userViewModel.Email, Email = userViewModel.Email, FirstName = userViewModel.FirstName, LastName = userViewModel.LastName };

            IdentityResult result = await UserManager.CreateAsync(user, userViewModel.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok("Successfully created a new user");
        }


        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [HttpGet]
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            LoginData externalLogin = LoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            XcendentUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await UserManager.GenerateUserIdentityAsync(user,
                   OAuthDefaults.AuthenticationType, LoginData.GetClaimsFromExternalIdentityForLocalAuthority(externalLogin));
                AuthenticationProperties properties = XcendentOAuthProvider.CreateProperties(user);


                Authentication.SignIn(properties, oAuthIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public async Task<UserInfoViewModel> GetUserInfo()
        {
            LoginData externalLogin = LoginData.FromIdentity(User.Identity as ClaimsIdentity);
            XcendentUser xcendantUser = null;
            if (externalLogin != null)
            {
                xcendantUser = await UserManager.FindByEmailAsync(externalLogin.Email);
            }
            return new UserInfoViewModel
            {
                Name = externalLogin != null ? externalLogin.Name : null,
                Email = externalLogin != null ? externalLogin.Email : User.Identity.GetUserName(),
                UserName = User.Identity.GetUserName(),
                HasRegistered = xcendantUser != null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }
        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoWithBeaerAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new XcendentUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        private IAuthenticationManager authentication;

        #region Helpers

        public IAuthenticationManager Authentication
        {
            get
            {
                if (authentication == null)
                {
                    authentication = Request.GetOwinContext().Authentication;
                }
                return authentication;
            }
            set { authentication = value; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    StringBuilder errorMsg = new StringBuilder();
                    foreach (string error in result.Errors)
                    {
                        errorMsg.Append(error);
                    }
                    return BadRequest(errorMsg.ToString());

                }
                else
                {
                    return BadRequest();
                }

            }

            return null;
        }



        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
