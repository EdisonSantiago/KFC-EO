using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Providers.Models;

namespace Oulanka.Providers.BslProviders
{
    public abstract class VtekSriDataProvider
    {
        public const string ProviderName = "VtekSriDataProvider";
        private static VtekSriDataProvider _defaultInstance;

        #region Instance

        static VtekSriDataProvider()
        {
            CreateDefaultVtekSriDataProvider();
        }

        public static VtekSriDataProvider Instance()
        {
            return _defaultInstance;
        }

        public static VtekSriDataProvider Instance(Provider dataProvider)
        {
            // TODO: Caching of providers

            var dp = DataProviders.Invoke(dataProvider) as VtekSriDataProvider;

            return dp;

        }

        private static void CreateDefaultVtekSriDataProvider()
        {
            var configurationSettings = ServiceLocator.Current.GetInstance<IConfigurationSettings>();

            var sqlProvider = new Provider(configurationSettings.GetProviderTable().Providers[ProviderName]);
            _defaultInstance = DataProviders.CreateInstance(sqlProvider) as VtekSriDataProvider;


        }

        #endregion

        public abstract IList<IVtRotefMovement> GetRotefMovementList(string month, string year, VtRotefMovementType movementType);

        public static IVtRotefMovement PopulateMovementFromDataRecord(IDataRecord dataRecord)
        {
            if (dataRecord == null) throw new ArgumentNullException(nameof(dataRecord));

            var movement = new VtRotefMovement();
            movement.TipoIdCliente = (string)dataRecord["TIPODOCUMENTO"];
            movement.IdCliente = (string)dataRecord["DOCUMENTO"];
            movement.RazonSocial = (string)dataRecord["CLIENTE"];
            movement.PaisNacionalidad = "593";
            movement.Dir = (string)dataRecord["DIRECCION"];
            movement.DirProv = (string)dataRecord["PROVINCIA"];
            movement.DirCanton = (string)dataRecord["CANTON"];
            movement.TipoProducto = (string)dataRecord["PRODUCTO"];
            movement.NumProducto = (string)dataRecord["CUENTA"].ToString();
            movement.ValDebito = decimal.Parse(dataRecord["VAL_DEB"].ToString().Replace(".",","));
            movement.ValCredito = decimal.Parse(dataRecord["VAL_CRE"].ToString().Replace(".", ","));
            movement.ValEfectivo = decimal.Parse(dataRecord["VAL_EFE"].ToString().Replace(".", ","));
            movement.ValCheque = decimal.Parse(dataRecord["VAL_CHQ"].ToString().Replace(".", ","));
            movement.ValTotal = decimal.Parse(dataRecord["VAL_TOTAL"].ToString().Replace(".", ","));
            movement.TipoOperacion = (string)dataRecord["TIPO_OPERACION"];
            movement.CodMoneda = (string)dataRecord["MONEDA"].ToString();
            movement.PaisTrx = (string)dataRecord["PAISTRX"].ToString();
            movement.RazonSocialTrx = (string)dataRecord["RAZONSOCIALTRX"].ToString();
            movement.IfiTrx = (string)dataRecord["IFITRX"].ToString();
            movement.NumProductoTrx = (string)dataRecord["NUMPRODTRX"].ToString();
            movement.NumOperaciones = 1;

            return movement;
        }

    }
}