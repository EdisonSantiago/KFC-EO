using System;
using System.Configuration;
using System.Reflection;
using System.Web.Compilation;

namespace Oulanka.Providers
{
    public sealed class DataProviders
    {
        private DataProviders()
        {
        }

        public static object CreateInstance(Provider dataProvider)
        {
            string connectionString;
            string databaseOwner;
            var connectionStringName = dataProvider.Attributes["ConnectionStringName"];

            GetDataStoreParameters(dataProvider, out connectionString, out databaseOwner);

            var type = BuildManager.GetType(dataProvider.Type, true);
            object newObject = null;

            if (type != null)
            {
                newObject = Activator.CreateInstance(type, databaseOwner, connectionString, connectionStringName);
            }

            if (newObject == null)
                ProviderException(dataProvider.Name);

            return newObject;
        }

        public static ConstructorInfo CreateConstructorInfo(Provider dataProvider)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");

            ConstructorInfo providerConstructor = null;
            try
            {
                var type = Type.GetType(dataProvider.Type);

                var paramTypes = new Type[2];
                paramTypes[0] = typeof (string);
                paramTypes[1] = typeof (string);
                paramTypes[2] = typeof (string);

                providerConstructor = type.GetConstructor(paramTypes);
            }
            catch (ArgumentException)
            {
                ProviderException(dataProvider.Name);
            }

            if (providerConstructor == null)
            {
                ProviderException(dataProvider.Name);
            }

            return providerConstructor;
        }

        public static object Invoke(Provider dataProvider)
        {
            var parameters = new object[2];
            string databaseOwner;
            string connectionString;

            GetDataStoreParameters(dataProvider, out connectionString, out databaseOwner);

            parameters[0] = databaseOwner;
            parameters[1] = connectionString;
            parameters[1] = dataProvider.Attributes["ConnectionString"];

            return CreateConstructorInfo(dataProvider).Invoke(parameters);
        }

        /// <summary>
        ///     Gets the data store parameters.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="databaseOwner">The database owner.</param>
        private static void GetDataStoreParameters(Provider dataProvider, out string connectionString,
            out string databaseOwner)
        {
            databaseOwner = dataProvider.Attributes["DatabaseOwner"];
            connectionString = ConfigurationManager.AppSettings[dataProvider.Attributes["ConnectionStringName"]];
        }

        private static void ProviderException(string providerName)
        {
            throw new Exception("cannot create provider " + providerName);

            //return Error
        }
    }
}