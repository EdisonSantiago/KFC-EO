using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Oulanka.Web.Core.Identity;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class IdentityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IUserStore<>))
                .ImplementedBy(typeof(UserStore<>))
                .Named("userStore")
                .LifestylePerWebRequest());

            container.Register(Component.For(typeof(IRoleStore<>))
                .ImplementedBy(typeof(RoleStore<>))
                .Named("roleStore")
                .LifestylePerWebRequest());

            container.Register(
                Component.For(typeof(UserManager<>))
                .ImplementedBy(typeof(UserManager<>))
                .Named("userManager")
                .LifestylePerWebRequest());
        }
    }
}