using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Services")
                    .Pick()
                    .Unless(t => t.Namespace != null && t.Namespace.EndsWith("Handlers"))
                    .WithService.DefaultInterfaces());
        }
    }
}