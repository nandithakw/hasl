using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Hospitals;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Hospitals;
using Xunit;

namespace Xcendant.HASL.Services.Tests.Hospitals
{
    public class HospitalManagerTests
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
                var iEntityFacade = Substitute.For<IHospitalFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns((Hospital)null);
                iEntityFacade.AddNew(Arg.Any<IHaslContext>(), Arg.Any<Hospital>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<IHospitalFacade>().As<IHospitalManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<IHospitalManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new Hospital()
            {
                Id = 1,
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
                var iEntityFacade = Substitute.For<IHospitalFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns(new Hospital { Id = 5 });
                iEntityFacade.UpdateAsync(Arg.Any<IHaslContext>(), Arg.Any<Hospital>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<HospitalManager>().As<IHospitalManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<IHospitalManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new Hospital()
            {
                Id = 5,
                Email = "durdans@durdans.com"
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
                var iEntityFacade = Substitute.For<IHospitalFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>())
                .Returns(new Hospital
                {
                    Id = 5,
                    Email = "durdans@durdans.com",
                });

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<HospitalManager>().As<IHospitalManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<IHospitalManager>();
            var entity = await iCurdLogicManager.FindAsync("professorX@xmen.com");
            Assert.NotNull(entity);
            Assert.Equal<int>(5, entity.Id);
        }
    }
}
