using System.Text;

namespace Oulanka.Providers.Helpers
{
    public static class BslSqlGenerator
    {
        public static string GetOverdraftListSqlString()
        {
            var sqlString = new StringBuilder();

            sqlString.Append("SELECT AGENCIA, TIPO = 'C', NOMBREAG = ");
            sqlString.Append(" CASE AGENCIA ");
            sqlString.Append("WHEN '01' THEN 'MATRIZ' ");
            sqlString.Append("WHEN '02' THEN 'PRENSA' ");
            sqlString.Append("WHEN '03' THEN 'CENTRO' ");
            sqlString.Append("WHEN '04' THEN 'SUR' ");
            sqlString.Append("WHEN '10' THEN 'GUARANDA' ");
            sqlString.Append("WHEN '13' THEN 'MANTA' ");
            sqlString.Append("WHEN '16' THEN 'PUYO' ");
            sqlString.Append("WHEN '17' THEN 'STO DOMINGO' ");
            sqlString.Append("WHEN '18' THEN 'AMBATO' ");
            sqlString.Append("WHEN '19' THEN 'BAÑOS' ");
            sqlString.Append("WHEN '20' THEN 'LATACUNGA' ");
            sqlString.Append("WHEN '23' THEN 'CUENCA' ");
            sqlString.Append("WHEN '26' THEN 'IBARRA' ");
            sqlString.Append("WHEN '29' THEN 'TULCAN' ");
            sqlString.Append("WHEN '50' THEN 'GUAYAQUIL' ");
            sqlString.Append(" END , '0' as CM2CUENTA, ' ' as CM2DCNOBR, '9999999' as CM2SALSOB");
            sqlString.Append(" FROM SOBREGIROS ");
            sqlString.Append(" UNION ");
            sqlString.Append(" SELECT AGENCIA, TIPO = 'D', ESTADO, CM2CUENTA, CM2DCNOBR, CM2SALSOB ");
            sqlString.Append(" FROM SOBREGIROS ");
            sqlString.Append(" UNION ");
            sqlString.Append(" SELECT AGENCIA, 'T', NOMBREAG = ");
            sqlString.Append(" CASE AGENCIA ");
            sqlString.Append(" WHEN '01' THEN 'MATRIZ' ");
            sqlString.Append(" WHEN '02' THEN 'PRENSA' ");
            sqlString.Append(" WHEN '03' THEN 'CENTRO' ");
            sqlString.Append(" WHEN '04' THEN 'SUR' ");
            sqlString.Append(" WHEN '10' THEN 'GUARANDA' ");
            sqlString.Append(" WHEN '13' THEN 'MANTA' ");
            sqlString.Append(" WHEN '16' THEN 'PUYO' ");
            sqlString.Append(" WHEN '17' THEN 'STO DOMINGO' ");
            sqlString.Append(" WHEN '18' THEN 'AMBATO' ");
            sqlString.Append(" WHEN '19' THEN 'BAÑOS' ");
            sqlString.Append(" WHEN '20' THEN 'LATACUNGA' ");
            sqlString.Append(" WHEN '23' THEN 'CUENCA' ");
            sqlString.Append(" WHEN '26' THEN 'IBARRA' ");
            sqlString.Append(" WHEN '29' THEN 'TULCAN' ");
            sqlString.Append(" WHEN '50' THEN 'GUAYAQUIL' ");
            sqlString.Append(" END , '0', 'TOTAL AGENCIA', SUM(CM2SALSOB) ");
            sqlString.Append(" FROM SOBREGIROS GROUP BY AGENCIA ORDER BY AGENCIA, TIPO, 6 DESC ");

            return sqlString.ToString();
        }

        public static string GetClientListSqlString()
        {
            var sqlString = new StringBuilder();

            sqlString.Append("SELECT TIPOD, IDENTIFICACION, NOMBRE, CM2CUENTA ");
            sqlString.Append(" FROM clientes ");

            return sqlString.ToString();
        }

    }
}