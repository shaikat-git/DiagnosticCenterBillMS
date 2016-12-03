using System.Data.SqlClient;
using System.Web.Configuration;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Geteway
{
    public class Gateway
    {
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        private string connectionString;
        
        public Gateway()
        {
          string serverName = System.Environment.MachineName;
          connectionString = WebConfigurationManager.ConnectionStrings["DCBMDBConnectionStringHome"].ConnectionString;
          Connection = new SqlConnection(connectionString);
        }
    }
}