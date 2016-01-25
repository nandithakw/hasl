using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Members;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Members;
using Xunit;

namespace Xcendant.HASL.Services.Tests.Members
{
    public class RegisteredUserManagerTests
    {

        [Fact]
        public async Task RegisterNewUserOrUpdateDetailsCreateNewAndShouldReturnOne()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iRegisteredUserFacade = Substitute.For<IRegisteredUserFacade>();
                iRegisteredUserFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns((RegisteredUser)null);
                iRegisteredUserFacade.AddNew(Arg.Any<IHaslContext>(), Arg.Any<RegisteredUser>()).Returns(1);

                return iRegisteredUserFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<IRegisteredUserManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iRegisteredUserManager = iContainer.Resolve<IRegisteredUserManager>();
            var modifiedCount = await iRegisteredUserManager.RegisterNewOrUpdateDetailsAsync(new RegisteredUser()
            {
                FirstName = "James",
                LastName = "McAvoy",
                Email = "professorX@xmen.com"
            });
            Assert.Equal<int>(1, modifiedCount);
        }


        [Fact]
        public async Task RegisterNewUserOrUpdateDetailsUpdateExsitingAndShouldReturnOne()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iRegisteredUserFacade = Substitute.For<IRegisteredUserFacade>();
                iRegisteredUserFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns(new RegisteredUser { Id = 5 });
                iRegisteredUserFacade.UpdateAsync(Arg.Any<IHaslContext>(), Arg.Any<RegisteredUser>()).Returns(1);

                return iRegisteredUserFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<IRegisteredUserManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iRegisteredUserManager = iContainer.Resolve<IRegisteredUserManager>();
            var modifiedCount = await iRegisteredUserManager.RegisterNewOrUpdateDetailsAsync(new RegisteredUser()
            {
                Id = 5,
                FirstName = "James",
                LastName = "McAvoy",
                Email = "professorX@xmen.com"
            });
            Assert.Equal<int>(1, modifiedCount);
        }

        [Fact]
        public async Task FindRegisterdUserShouldReturnARegisteredUser()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iRegisteredUserFacade = Substitute.For<IRegisteredUserFacade>();
                iRegisteredUserFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>())
                .Returns(new RegisteredUser
                {
                    Id = 5,
                    FirstName = "James",
                    LastName = "McAvoy",
                    Email = "professorX@xmen.com"
                });

                return iRegisteredUserFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<IRegisteredUserManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iRegisteredUserManager = iContainer.Resolve<IRegisteredUserManager>();
            var registeredUser = await iRegisteredUserManager.FindAsync("professorX@xmen.com");
            Assert.NotNull(registeredUser);
            Assert.Equal<int>(5, registeredUser.Id);
        }





    }
}
