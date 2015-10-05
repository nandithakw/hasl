using Autofac;
using Microsoft.AspNet.Identity.Owin;
using NSubstitute;
using System.Net.Http;
using System.Web.Http;
using Xcendant.Auth.Tests.Model.DAL;
using Xcendant.Auth.Tests.Model.Managers;
using Xcendant.Auth.Tests.Shared;
using Xcendent.Auth.Controllers;
using Xcendent.Auth.Models.DAL;
using Xcendent.Auth.Models.Entities;
using Xcendent.Auth.Models.Managers;
using Xcendent.Auth.ViewModels;
using Xunit;

namespace Xcendant.Auth.Tests.Controllers
{
    public class AccountControllerTests:IClassFixture<XcendantIdentityFixture>
    {
        public IContainer Container { get; set; }
        public AccountControllerTests(XcendantIdentityFixture fixture) {
            Container = fixture.Container;

        }
        [Theory]
        [InlineData("Hugh", "Jackman", "ProBook4540s_", "ProBook4540s_", "logan@xmen.com")]
        public void Register_ShouldReturnOK(string firstName, string lastName, string password, string confirmPassword, string email)
        {
           
            
            AccountController controller = new AccountController(Container.Resolve<AbstractXcendentUserManager<XcendentUser>>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var userViewModel = new UserViewModel { FirstName = firstName, LastName = lastName, Password = password, ConfirmPassword = confirmPassword, Email = email };
            controller.Register(userViewModel).Wait();
        }

    }
}
