using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class SharpArchInstaller : IWindsorInstaller
    {
        #region Public Methods and Operators

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IEntityDuplicateChecker))
                    .ImplementedBy(typeof(EntityDuplicateChecker))
                    .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(ISessionFactoryKeyProvider))
                    .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
                    .Named("sessionFactoryKeyProvider"));

            container.Register(
                Component.For(typeof(ICommandProcessor))
                    .ImplementedBy(typeof(CommandProcessor))
                    .Named("commandProcessor"));
        }

        #endregion
    }
}