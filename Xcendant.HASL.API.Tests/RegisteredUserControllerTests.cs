using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Autofac;
using NSubstitute;
using Xcendant.HASL.API.Controllers;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Members;
using Xunit;

namespace Xcendant.HASL.API.Tests
{
    public class RegisteredUserControllerTests
    {
        [Fact]
        public async Task GetUserDetailsShouldReturnOKWithRegisteredUser()
        {


            ContainerBuilder autoFacBuilder = new ContainerBuilder();

            autoFacBuilder.Register(c =>
            {
                var iRegisteredUserManager = Substitute.For<RegisteredUserManager>();
                iRegisteredUserManager.FindAsync(Arg.Any<string>()).Returns<RegisteredUser>(new RegisteredUser { Email = "professorX@smen.com" });
                return iRegisteredUserManager;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<RegisteredUserManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();

            RegisteredUserController controller = new RegisteredUserController(iContainer.Resolve<IRegisteredUserManager>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = await controller.GetUserDetails("professorX@smen.com");

            var contentResult = actionResult as OkNegotiatedContentResult<RegisteredUser>;
            Assert.NotNull(contentResult);
            Assert.Equal("professorX@smen.com", contentResult.Content.Email);

        }


        [Fact]
        public async Task RegisterNewUserShouldReturnOK()
        {


            ContainerBuilder autoFacBuilder = new ContainerBuilder();

            autoFacBuilder.Register(c =>
            {
                var iRegisteredUserManager = Substitute.For<RegisteredUserManager>();
                iRegisteredUserManager.RegisterNewOrUpdateDetailsAsync(Arg.Any<RegisteredUser>()).Returns<int>(1);
                return iRegisteredUserManager;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<RegisteredUserManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();

            RegisteredUserController controller = new RegisteredUserController(iContainer.Resolve<IRegisteredUserManager>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = await controller.RegisterNewUser(new RegisteredUser());

            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.NotNull(contentResult);
            Assert.Equal("New user registration succeded", contentResult.Content);

        }



        [Fact]
        public async Task UpdateRegisteredUserShouldReturnOK()
        {


            ContainerBuilder autoFacBuilder = new ContainerBuilder();

            autoFacBuilder.Register(c =>
            {
                var iRegisteredUserManager = Substitute.For<RegisteredUserManager>();
                iRegisteredUserManager.RegisterNewOrUpdateDetailsAsync(Arg.Any<RegisteredUser>()).Returns<int>(1);
                return iRegisteredUserManager;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<RegisteredUserManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();

            RegisteredUserController controller = new RegisteredUserController(iContainer.Resolve<IRegisteredUserManager>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = await controller.UpdateRegisteredUser(new RegisteredUser());

            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.NotNull(contentResult);
            Assert.Equal("User details update succeded", contentResult.Content);

        }
    }
}
