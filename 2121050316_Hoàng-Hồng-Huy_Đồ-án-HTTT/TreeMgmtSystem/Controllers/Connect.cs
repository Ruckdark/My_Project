using System;
using System.Data.SqlClient;

namespace bookMgmtADO
{
    internal class Connect
    {
        private SqlConnection conn;

        public Connect()
        {
            string serverName = "MRNGUYEN\\SQLEXPRESS";
            string userName = "sa";
            string password = "12345678";
            string db = "treemgmt";
            string connectionString = "Data Source=" + serverName + ";Initial Catalog=" + db + ";User ID=" + userName + ";Password=" + password + ";";
            conn = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return conn;
        }
    }
}
