using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Oulanka.Api
{
    public class QueryInstaller : IWindsorInstaller
    {
        #region Public Methods and Operators

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Web.Mvc")
                    .InNamespace("Oulanka.Web.Mvc.Controllers.Queries", true)
                    .WithService.DefaultInterfaces());
        }

        #endregion
    }
}