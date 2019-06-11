using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Oulanka.Services.Jobs;

namespace Oulanka.Api
{
    public class JobsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof (EmailJob))
                    .ImplementedBy(typeof (EmailJob))
                    .Named("emailJob"));

            container.Register(
                    Component.For(typeof(UserSessionJob))
                        .ImplementedBy(typeof(UserSessionJob))
                        .Named("userSessionJob"));

        }
    }
}