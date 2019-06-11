using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharpArch.Domain.Commands;
using SharpArch.Domain.Events;

namespace Oulanka.Api
{
    public class HandlersInstaller : IWindsorInstaller
    {
        #region Public Methods and Operators

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssemblyNamed("Oulanka.Tasks")
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithService.FirstInterface());

            container.Register(
                Types.FromAssemblyNamed("Oulanka.Tasks")
                    .BasedOn(typeof(ICommandHandler<,>))
                    .WithService.FirstInterface());

            container.Register(
                Types.FromAssemblyNamed("Oulanka.Tasks")
                    .BasedOn(typeof(IHandles<>))
                    .WithService.FirstInterface());
        }

        #endregion
    }
}