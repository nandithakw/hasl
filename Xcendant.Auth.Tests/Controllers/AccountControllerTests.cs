using Autofac;
using Microsoft.AspNet.Identity.Owin;
using NSubstitute;
using System.Net.Http;
using System.Web.Http;
using Xcendant.Auth.Tests.Model.DAL;
using Xcendant.Auth.Tests.Model.Managers;
using Xcendant.Auth.Tests.Shared;
using Xcendant.Auth.Controllers;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;
using Xcendant.Auth.Models.Managers;
using Xcendant.Auth.ViewModels;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web.Http.Results;
using Microsoft.Owin.Security;
using Xcendent.Auth.Extensions;
using System.Security.Claims;
using System.Collections.Generic;

namespace Xcendant.Auth.Tests.Controllers
{
    public class AccountControllerTests : IClassFixture<XcendantIdentityFixture>
    {
        public IContainer Container { get; set; }
        public AccountControllerTests(XcendantIdentityFixture fixture)
        {
            Container = fixture.Container;

        }
        [Theory]
        [InlineData("Hugh", "Jackman", "testpassword", "testpassword", "logan@xmen.com")]
        public async Task RegisterShouldReturnOK(string firstName, string lastName, string password, string confirmPassword, string email)
        {

            AccountController controller = new AccountController(Container.Resolve<AbstractXcendentUserManager<XcendentUser>>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var userViewModel = new UserViewModel { FirstName = firstName, LastName = lastName, Password = password, ConfirmPassword = confirmPassword, Email = email };
            var userManager = Container.Resolve<AbstractXcendentUserManager<XcendentUser>>();
            userManager.CreateAsync(new XcendentUser(), "testpassword").
                ReturnsForAnyArgs(Task.FromResult(IdentityResult.Success));
            IHttpActionResult actionResult = await controller.Register(userViewModel);

            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.NotNull(contentResult);
            Assert.Equal("Successfully created a new user", contentResult.Content);
        }

        [Theory]
        [InlineData("Hugh", "Jackman", "testpassword", "testpassword", "logan@xmen.com")]
        public async Task RegisterShouldReturnErrorAllReadyRegistered(string firstName, string lastName, string password, string confirmPassword, string email)
        {

            AccountController controller = new AccountController(Container.Resolve<AbstractXcendentUserManager<XcendentUser>>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var userViewModel = new UserViewModel { FirstName = firstName, LastName = lastName, Password = password, ConfirmPassword = confirmPassword, Email = email };
            var userManager = Container.Resolve<AbstractXcendentUserManager<XcendentUser>>();
            userManager.CreateAsync(new XcendentUser(), "testpassword").
                ReturnsForAnyArgs(Task.FromResult(IdentityResult.Failed(new string[] { "Username is already regiseterd." })));
            IHttpActionResult actionResult = await controller.Register(userViewModel);

            var contentResult = actionResult as BadRequestErrorMessageResult;
            Assert.NotNull(contentResult);
            Assert.Equal("Username is already regiseterd.", contentResult.Message);
        }

        [Theory]
        [InlineData("logan@xmen.com")]
        public async Task RegisterExternalShouldReturnOK(string email)
        {

            AccountController controller = new AccountController(Container.Resolve<AbstractXcendentUserManager<XcendentUser>>());
            HttpRequestMessage request= new HttpRequestMessage();
            
            controller.Request =request;
           
            controller.Configuration = new HttpConfiguration();
            var stubAuthenticationManager = Substitute.For<IAuthenticationManager>();
            ICollection<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "Hugh Jackman"));
            claims.Add(new Claim(ClaimTypes.Name, "Hugh Jackman"));

            stubAuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalBearer).ReturnsForAnyArgs(
                Task.FromResult<AuthenticateResult>(
                    new AuthenticateResult(new ClaimsIdentity(claims) { }, new AuthenticationProperties(), new AuthenticationDescription()))
                    );
            controller.Authentication = stubAuthenticationManager;
            var userManager = Container.Resolve<AbstractXcendentUserManager<XcendentUser>>();
            userManager.CreateAsync(new XcendentUser() { UserName =email, Email = email }).
                ReturnsForAnyArgs(Task.FromResult(IdentityResult.Success));
            userManager.AddLoginAsync("",null).
                ReturnsForAnyArgs(Task.FromResult(IdentityResult.Success));

            IHttpActionResult actionResult = await controller.RegisterExternal(new RegisterExternalBindingModel() { Email = email });
            Assert.NotNull(actionResult);
            Assert.IsType(typeof(OkResult), actionResult);
        }




    }
}
