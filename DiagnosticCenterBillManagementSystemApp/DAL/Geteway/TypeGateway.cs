using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Geteway
{
    public class TypeGateway : Gateway
    {
        public int Save(TestType aTestType)
        {
            Query = "INSERT INTO Type VALUES (@TypeName)";
            Command = new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("TypeName", SqlDbType.VarChar);
            Command.Parameters["TypeName"].Value = aTestType.TypeName;
            Connection.Open();
            int effectedRow = Command.ExecuteNonQuery();
            Connection.Close();
            return effectedRow;
        }

        public bool IsTypeExist(string typeName)
        {
            Query = "SELECT * FROM Type WHERE TypeName=@TypeName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("TypeName", SqlDbType.NVarChar);
            Command.Parameters["TypeName"].Value = typeName;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            bool IsExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return IsExist;
        }

        public List<TestType> GetAllType()
        {
            Query = "SELECT * FROM Type ORDER BY TypeName";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TestType> types = new List<TestType>();
            while (Reader.Read())
            {
                TestType aTestType = new TestType();
                aTestType.Id = (int) Reader["Id"];
                aTestType.TypeName = Reader["TypeName"].ToString();
                types.Add(aTestType);
            }
            Reader.Close();
            Connection.Close();
            return types;
        }
    }
}