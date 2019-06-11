using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class QueryInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Web.Api")
                    .InNamespace("Oulanka.Web.Api.Controllers.Queries", true)
                    .WithService.DefaultInterfaces());
        }

    }
}