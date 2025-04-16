using System.Configuration;
using System.Data.SqlClient;

namespace WordVaultAppMVC.Data
{
    public static class DatabaseContext
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["WordVaultDb"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
