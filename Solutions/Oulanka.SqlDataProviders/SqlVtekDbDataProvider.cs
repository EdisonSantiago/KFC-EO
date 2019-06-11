using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Oulanka.Data.Helpers;
using Oulanka.Providers.BslProviders;
using Oulanka.Providers.Helpers;
using Oulanka.Providers.Models;

namespace Oulanka.Data
{
    public class SqlVtekDbDataProvider : VtekDbDataProvider
    {
        private readonly string _connectionStringName;
        private readonly string _databaseOwner = "dbo";
        private readonly Database _db;
        private string _connectionString;

        public SqlVtekDbDataProvider(string databaseOwner, string connectionString, string connectionStringName)
        {
            // Read the connection string for this provider
            //
            _connectionString = connectionString;
            _databaseOwner = databaseOwner;
            _connectionStringName = connectionStringName;
            var factory = new DatabaseProviderFactory();

            _db = factory.Create(_connectionStringName);
        }


        #region ROTEF

        public override IList<IVtRotefMovement> GetRotefList(int month, int year, VtRotefMovementType movementType)
        {
            //TODO: Get SqlQuery by movementType
            string sqlString = "";

            var dbc = _db.GetSqlStringCommand(sqlString);
            dbc.CommandType = CommandType.Text;

            var startDate = int.Parse($"{year}{month}01");
            var endDate = int.Parse($"{year}{month}31");

            _db.AddInParameter(dbc, "@StartDate",DbType.Int32,startDate);
            _db.AddInParameter(dbc, "@EbdDate",DbType.Int32,endDate);

            var movementList = new List<IVtRotefMovement>();
            using (var dataReader = _db.ExecuteReader(dbc))
            {
                while (dataReader.Read())
                {
                    movementList.Add(PopulateMovementFromDataRecord(dataReader));
                }
            }

            return movementList;
        }

        #endregion

        #region OVERDRAFTS

        public override IList<IVtOverdraft> GetOverdraftList()
        {
            var sqlString = BslSqlGenerator.GetOverdraftListSqlString();

            var dbc = _db.GetSqlStringCommand(sqlString);
            dbc.CommandType = CommandType.Text;

            var overdraftList = new List<IVtOverdraft>();
            using (var dataReader = _db.ExecuteReader(dbc))
            {
                while (dataReader.Read())
                {
                    overdraftList.Add(PopulateOverdraftFromDataRecord(dataReader));
                }
            }

            return overdraftList;
        }

        #endregion

        #region CLIENTS

        public override IList<IVtClient> GetClientList()
        {
            var sqlString = BslSqlGenerator.GetClientListSqlString();

            var dbc = _db.GetSqlStringCommand(sqlString);
            dbc.CommandType = CommandType.Text;

            var list = new List<IVtClient>();
            using (var dataReader = _db.ExecuteReader(dbc))
            {
                while (dataReader.Read())
                {
                    list.Add(PopulateIVtClientFromDataRecord(dataReader));
                }

                dataReader.Close();
            }

            return list;
        }

        #endregion

        public override IList<IVtCreditPayment> GetCreditPayments(string month, string year)
        {
            var sqlQueryName = "GetCreditPaymentsByDate.sql";
            var sqlString = ResourceHelper.GetEmbeddedResource(sqlQueryName);

            var dbc = _db.GetSqlStringCommand(sqlString);
            dbc.CommandType = CommandType.Text;

            var startDate = int.Parse($"{year}{month}01");
            var endDate = int.Parse($"{year}{month}31");

            _db.AddInParameter(dbc, "@StartDate", DbType.Int32, startDate);
            _db.AddInParameter(dbc, "@EndDate", DbType.Int32, endDate);

            var payments = new List<IVtCreditPayment>();
            using (var dataReader = _db.ExecuteReader(dbc))
            {
                while (dataReader.Read())
                {
                    payments.Add(PopulateCreditPaymentFromIDataRecord(dataReader));
                }

                dataReader.Close();
            }

            return payments;
        }

        public override IList<IVtOverdraftPayment> GetOverdraftPayments(string month, string year)
        {
            var sqlQueryName = "GetOverdraftPaymentsByDate.sql";
            var sqlString = ResourceHelper.GetEmbeddedResource(sqlQueryName);

            var dbc = _db.GetSqlStringCommand(sqlString);
            dbc.CommandType = CommandType.Text;

            var startDate = int.Parse($"{year}{month}01");
            var endDate = int.Parse($"{year}{month}31");

            _db.AddInParameter(dbc, "@StartDate", DbType.Int32, startDate);
            _db.AddInParameter(dbc, "@EndDate", DbType.Int32, endDate);

            var payments = new List<IVtOverdraftPayment>();
            using (var dataReader = _db.ExecuteReader(dbc))
            {
                while (dataReader.Read())
                {
                    payments.Add(PopulateVtOverdraftPaymentFromIDataRecord(dataReader));
                }

                dataReader.Close();
            }

            return payments;
        }
    }
}
