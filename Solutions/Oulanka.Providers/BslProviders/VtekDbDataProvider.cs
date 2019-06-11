using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Providers.Models;

namespace Oulanka.Providers.BslProviders
{
    public abstract class VtekDbDataProvider
    {
        public const string ProviderName = "VtekDbDataProvider";

        private static VtekDbDataProvider _defaultInstance;

        static VtekDbDataProvider()
        {
            CreateDefaultVtekDbDataProvider();
        }

        public static VtekDbDataProvider Instance()
        {
            return _defaultInstance;
        }

        public static VtekDbDataProvider Instance(Provider dataProvider)
        {
            // TODO: Caching of providers

            var dp = DataProviders.Invoke(dataProvider) as VtekDbDataProvider;

            return dp;

        }

        private static void CreateDefaultVtekDbDataProvider()
        {
            var configurationSettings = ServiceLocator.Current.GetInstance<IConfigurationSettings>();

            var sqlProvider = new Provider(configurationSettings.GetProviderTable().Providers[ProviderName]);
            _defaultInstance = DataProviders.CreateInstance(sqlProvider) as VtekDbDataProvider;


        }


        #region ROTEF

        public abstract IList<IVtRotefMovement> GetRotefList(int month, int year, VtRotefMovementType movementType);


        public static IVtRotefMovement PopulateMovementFromDataRecord(IDataRecord dataRecord)
        {
            if (dataRecord == null) throw new ArgumentNullException("dataRecord");

            var movement = new VtRotefMovement
            {
                TipoIdCliente = (string)dataRecord["TipoDocumento"],
                IdCliente = (string)dataRecord["Documento"],
                RazonSocial = (string)dataRecord["Cliente"],
                PaisNacionalidad = "593",
                Dir = (string)dataRecord["Direccion"],
                DirProv = (string)dataRecord["Provincia"],
                DirCanton = (string)dataRecord["Canton"],
                TipoProducto = (string)dataRecord["Producto"],
                NumProducto = (string)dataRecord["Cuenta"],
                ValDebito = decimal.Parse(dataRecord["Val_deb"].ToString()),
                ValCredito = decimal.Parse(dataRecord["Val_Cre"].ToString()),
                ValEfectivo = decimal.Parse(dataRecord["Val_Efe"].ToString()),
                ValCheque = decimal.Parse(dataRecord["Val_Chq"].ToString()),

                TipoOperacion = (string)dataRecord["Tipo_Operacion"],
                CodMoneda = (string)dataRecord["Moneda"],
                PaisTrx = (string)dataRecord["PAISTRX"],
                RazonSocialTrx = (string)dataRecord["RAZONSOCIALTRX"],
                IfiTrx = (string)dataRecord["IFITRX"],
                NumProductoTrx = (string)dataRecord["NUMPRODTRX"],
                NumOperaciones = 1,

            };

            return movement;
        }

        #endregion

        #region OVERDRAFTS

        public abstract IList<IVtOverdraft> GetOverdraftList();

        public static IVtOverdraft PopulateOverdraftFromDataRecord(IDataRecord dataRecord)
        {
            if (dataRecord == null) throw new ArgumentNullException("dataRecord");

            var overdraft = new VtOverdraft
            {
                Agency = (string)dataRecord["AGENCIA"].ToString(),
                OverdraftType = (string)dataRecord["TIPO"].ToString(),
                AccountNumber = (string)dataRecord["CM2CUENTA"].ToString(),
                ClientName = (string)dataRecord["CM2DCNOBR"].ToString(),
                TotalAmmount = (decimal.Parse((string)dataRecord["CM2SALSOB"].ToString())),
                Identification = string.Empty
            };

            return overdraft;
        }

        #endregion

        #region CLIENTS

        public abstract IList<IVtClient> GetClientList();

        public static IVtClient PopulateIVtClientFromDataRecord(IDataRecord dataRecord)
        {
            if (dataRecord == null) throw new ArgumentNullException("dataRecord");
            var client = new VtClient
            {
                ClientType = (string)dataRecord["TIPOD"].ToString(),
                Identification = (string)dataRecord["IDENTIFICACION"].ToString(),
                CompleteName = (string)dataRecord["NOMBRE"].ToString(),
                AccountNumber = (string)dataRecord["CM2CUENTA"].ToString()
            };


            return client;
        }

        #endregion

        #region CREDIT PAYMENTS

        public abstract IList<IVtCreditPayment> GetCreditPayments(string month, string year);

        public static IVtCreditPayment PopulateCreditPaymentFromIDataRecord(IDataRecord dataRecord)
        {
            if (dataRecord == null) throw new ArgumentNullException(nameof(dataRecord));

            var payment = new VtCreditPayment();

            payment.Identification = (string)dataRecord["CEDULA"].ToString();
            payment.ClientName = (string)dataRecord["NOMBRE"].ToString();
            payment.Product = (string)dataRecord["PRODUCTO"].ToString();
            payment.OperationNumber = (string)dataRecord["OPERACION"].ToString().Remove(0,9);

            var paymentDate = (string)dataRecord["FECHA_PAGO"].ToString();
            payment.PaymentDate = DateTime.Parse($"{paymentDate.Substring(0, 4)}-{paymentDate.Substring(4, 2)}-{paymentDate.Substring(6, 2)}");

            payment.Dividend = (string)dataRecord["DIVIDENDO"].ToString();
            payment.Capital = Math.Round(decimal.Parse((string)dataRecord["CAPITAL"].ToString()), 2);
            payment.Interest = Math.Round(decimal.Parse((string)dataRecord["INTERES"].ToString()), 2);
            payment.Arrears = Math.Round(decimal.Parse((string)dataRecord["MORA"].ToString()), 2);
            payment.CashPaymentValue = Math.Round(decimal.Parse((string)dataRecord["EFECTIVO_PAGADO"].ToString()), 2);
            payment.CheckPaymentValue = Math.Round(decimal.Parse((string)dataRecord["CHEQUE_PAGADO"].ToString()), 2);
            payment.Comments = (string)dataRecord["OBSERVACION"].ToString();


            return payment;
        }

        #endregion

        #region OVERDRAFT PAYMENTS

        public abstract IList<IVtOverdraftPayment> GetOverdraftPayments(string month, string year);

        public static IVtOverdraftPayment PopulateVtOverdraftPaymentFromIDataRecord(IDataRecord dataRecord)
        {
            if (dataRecord == null) throw new ArgumentNullException(nameof(dataRecord));

            var payment = new VtOverdraftPayment();
            payment.Identification = (string)dataRecord["CEDULA"].ToString();
            payment.ClientName = (string)dataRecord["NOMBRE"].ToString();
            payment.AccountNumber = (string)dataRecord["CUENTA"].ToString();
            payment.PaymentValue = Math.Round(decimal.Parse((string)dataRecord["PAGO_SOBREGIRO"].ToString()), 2);

            var paymentDate = (string)dataRecord["FECHA"].ToString();
            payment.PaymentDate = DateTime.Parse($"{paymentDate.Substring(0, 4)}-{paymentDate.Substring(4, 2)}-{paymentDate.Substring(6, 2)}");


            return payment;
        }



        #endregion

    }
}