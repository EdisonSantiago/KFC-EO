using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Api
{
    public class ControllerInstaller : IWindsorInstaller
    {
        #region Public Methods and Operators

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.Named(c.Implementation.Name).LifestyleTransient()));
        }

        #endregion
    }
}