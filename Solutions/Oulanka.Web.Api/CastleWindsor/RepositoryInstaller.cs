using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Oulanka.Web.Api.CastleWindsor
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        #region Public Methods and Operators

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
        }

        #endregion

        #region Methods

        private static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Infrastructure")
                    .BasedOn(typeof(IRepositoryWithTypedId<,>))
                    .WithService.DefaultInterfaces());
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                    .ImplementedBy(typeof(NHibernateRepository<>))
                    .Named("nhibernateRepositoryType")
                    .Forward(typeof(IRepository<>)));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                    .Named("nhibernateRepositoryWithTypedId")
                    .Forward(typeof(IRepositoryWithTypedId<,>)));
        }

        #endregion
    }
}