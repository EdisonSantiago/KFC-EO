using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class TasksInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Tasks")
                    .Pick()
                    .Unless(t => t.Namespace != null && t.Namespace.EndsWith("Handlers"))
                    .WithService.DefaultInterfaces());
        }

    }
}