using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Geteway
{
    public class PaymentGateway : Gateway
    {
        public int Update(int requestId)
        {
            Query = "UPDATE Request SET PaymentFlag='True' WHERE Id=@Id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("Id", SqlDbType.Int);
            Command.Parameters["Id"].Value = requestId;
            Connection.Open();
            int effectedRow = Command.ExecuteNonQuery();
            Connection.Close();
            return effectedRow;
        }
    }
}