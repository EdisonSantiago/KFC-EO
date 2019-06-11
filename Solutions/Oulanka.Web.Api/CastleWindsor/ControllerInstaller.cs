using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class ControllerInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.Named(c.Implementation.Name).LifestyleTransient()));
        }

    }
}