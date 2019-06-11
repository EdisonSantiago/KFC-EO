using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Api.CastleWindsor
{
    public class QueryInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Api")
                    .InNamespace("Oulanka.Api.Controllers.Queries", true)
                    .WithService.DefaultInterfaces());
        }

    }
}