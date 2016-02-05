using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.CareGivers;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.CareGivers;
using Xunit;

namespace Xcendant.HASL.Services.Tests.CareGivers
{
    public class CareGiverManagarTests
    {
        [Fact]
        public async Task RegisterNewOrUpdateDetailsAsyncAndShouldReturnOne()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iEntityFacade = Substitute.For<ICareGiverFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns((CareGiver)null);
                iEntityFacade.AddNew(Arg.Any<IHaslContext>(), Arg.Any<CareGiver>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<CareGiverFacade>().As<ICareGiverManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<ICareGiverManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new Entities.CareGiver()
            {
                RegisteredUserId = 1,
                Description = "Test Caregiver",
            });
            Assert.Equal<int>(1, modifiedCount);
        }


        [Fact]
        public async Task RegisterNewOrUpdateDetailsAsyncEditExsitingAndShouldReturnOne()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iEntityFacade = Substitute.For<ICareGiverFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns(new CareGiver { RegisteredUserId = 5 });
                iEntityFacade.UpdateAsync(Arg.Any<IHaslContext>(), Arg.Any<CareGiver>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<CareGiverManager>().As<ICareGiverManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<ICareGiverManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new CareGiver()
            {
                RegisteredUserId = 5,
                Description = "Test Caregiver",
            });
            Assert.Equal<int>(1, modifiedCount);
        }

        [Fact]
        public async Task FindAsyncReturnTheRelatedEntity()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iEntityFacade = Substitute.For<ICareGiverFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>())
                .Returns(new CareGiver
                {
                    RegisteredUserId = 123456,
                    Description = "Test Caregiver",
                });

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<CareGiverManager>().As<ICareGiverManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<ICareGiverManager>();
            var entity = await iCurdLogicManager.FindAsync("professorX@xmen.com");
            Assert.NotNull(entity);
            Assert.Equal<int>(5, entity.RegisteredUserId);
        }
    }
}
