using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.TreatmentCenters;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.TreatmentCenters;
using Xunit;

namespace Xcendant.HASL.Services.Tests.TreatmentCenters
{
    public class TreatmentCenterManagerTests
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
                var iEntityFacade = Substitute.For<ITreatmentCenterFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns((TreatmentCenter)null);
                iEntityFacade.AddNew(Arg.Any<IHaslContext>(), Arg.Any<TreatmentCenter>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<ITreatmentCenterFacade>().As<ITreatmentCenterManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<ITreatmentCenterManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new TreatmentCenter()
            {
                Id = 1,
                Email = "familycare@familycare.com",
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
                var iEntityFacade = Substitute.For<ITreatmentCenterFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns(new TreatmentCenter { Id = 5 });
                iEntityFacade.UpdateAsync(Arg.Any<IHaslContext>(), Arg.Any<TreatmentCenter>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<TreatmentCenterManager>().As<ITreatmentCenterManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<ITreatmentCenterManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new TreatmentCenter()
            {
                Id = 5,
                Email = "familycare@familycare.com",
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
                var iEntityFacade = Substitute.For<ITreatmentCenterFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>())
                .Returns(new TreatmentCenter
                {
                    Id = 5,
                    Email = "familycare@familycare.com",

                });

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<TreatmentCenterManager>().As<ITreatmentCenterManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<ITreatmentCenterManager>();
            var entity = await iCurdLogicManager.FindAsync("professorX@xmen.com");
            Assert.NotNull(entity);
            Assert.Equal<int>(5, entity.Id);
        }
    }
}
