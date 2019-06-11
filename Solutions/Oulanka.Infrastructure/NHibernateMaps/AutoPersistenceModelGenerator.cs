using System;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Oulanka.Infrastructure.NHibernateMaps.Conventions;
using SharpArch.Domain.DomainModel;
using SharpArch.NHibernate.FluentNHibernate;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    /// <summary>
    ///     Generates the automapping for the domain assembly
    /// </summary>
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        #region Public Methods and Operators

        public AutoPersistenceModel Generate()
        {
            var mappings = AutoMap.Assemblies(
                new AutomappingConfiguration(), 
                Assembly.Load("Oulanka.Domain"));
            mappings.IgnoreBase<Entity>();
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.Conventions.Setup(GetConventions());
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
            return mappings;
        }

        #endregion

        #region Methods

        private static Action<IConventionFinder> GetConventions()
        {
            return c =>
                {
                    c.Add<PrimaryKeyConvention>();
                    c.Add<CustomForeignKeyConvention>();
                    c.Add<HasManyConvention>();
                    c.Add<TableNameConvention>();
                };
        }

        #endregion
    }
}