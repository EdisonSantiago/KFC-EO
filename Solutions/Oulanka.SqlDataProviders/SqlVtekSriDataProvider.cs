using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Oulanka.Data.Helpers;
using Oulanka.Providers.BslProviders;
using Oulanka.Providers.Models;

namespace Oulanka.Data
{
    public class SqlVtekSriDataProvider : VtekSriDataProvider
    {
        private readonly string _databaseOwner = "dbo";
        private readonly Database _db;
        private string _connectionString;

        public SqlVtekSriDataProvider(string databaseOwner, string connectionString, string connectionStringName)
        {
            // Read the connection string for this provider
            //
            _connectionString = connectionString;
            _databaseOwner = databaseOwner;
            var factory = new DatabaseProviderFactory();

            _db = factory.Create(connectionStringName);
        }


        public override IList<IVtRotefMovement> GetRotefMovementList(string month, string year, VtRotefMovementType movementType)
        {
            var sqlQueryName = "";

            switch (movementType)
            {
                    case VtRotefMovementType.FromAccounts:
                    sqlQueryName = "GetRotefMovementsFromAccounts.sql";
                    break;

                    case VtRotefMovementType.FromCredits:
                    sqlQueryName = "GetRotefMovementsFromCredits.sql";
                    break;
            }

            var sqlString = ResourceHelper.GetEmbeddedResource(sqlQueryName);

            var dbc = _db.GetSqlStringCommand(sqlString);
            dbc.CommandType = CommandType.Text;

            var startDate = int.Parse($"{year}{month}01");
            var endDate = int.Parse($"{year}{month}31");

            _db.AddInParameter(dbc, "@StartDate", DbType.Int32, startDate);
            _db.AddInParameter(dbc, "@EndDate", DbType.Int32, endDate);

            var movementList = new List<IVtRotefMovement>();
            using (var dataReader = _db.ExecuteReader(dbc))
            {
                while (dataReader.Read())
                {
                    movementList.Add(PopulateMovementFromDataRecord(dataReader));
                }

                dataReader.Close();
            }

            

            return movementList;
        }
    }
}