using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;


namespace DiagnosticCenterBillManagementSystemApp.DAL.Geteway
{
    public class TestGateway : Gateway
    {
        public int Save(Test aTest)
        {
            Query = "INSERT INTO Test VALUES (@TestName, @TestFee, @TypeId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("TestName", SqlDbType.VarChar);
            Command.Parameters["TestName"].Value = aTest.TestName;
            Command.Parameters.Add("TestFee", SqlDbType.Money);
            Command.Parameters["TestFee"].Value = aTest.Fee;
            Command.Parameters.Add("TypeId", SqlDbType.Int);
            Command.Parameters["TypeId"].Value = aTest.TypeId;
            Connection.Open();
            int effectedRow = Command.ExecuteNonQuery();
            Connection.Close();
            return effectedRow;
        }

        public bool IsTestExist(string testName)
        {
            Query = "SELECT * FROM Test WHERE TestName=@TestName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("TestName", SqlDbType.NVarChar);
            Command.Parameters["TestName"].Value = testName;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            bool isExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isExist;
        }

        public List<TestSetupVM> GetAllTest()
        {
            Query = "SELECT * FROM TestView ORDER BY TestName";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TestSetupVM> testSetupVms = new List<TestSetupVM>();
            while (Reader.Read())
            {
                TestSetupVM aTestSetupVm = new TestSetupVM();
                aTestSetupVm.TestId = (int)Reader["TestId"];
                aTestSetupVm.TestName = Reader["TestName"].ToString();
                aTestSetupVm.TestFee = Convert.ToDouble(Reader["TestFee"]);
                aTestSetupVm.TypeName = Reader["TypeName"].ToString();
                testSetupVms.Add(aTestSetupVm);
            }
            Reader.Close();
            Connection.Close();
            return testSetupVms;
        }

        public double GetTestFeeById(int testId)
        {
            Query = "SELECT Fee FROM Test WHERE Id=@Id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("Id", SqlDbType.Int);
            Command.Parameters["Id"].Value = testId;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            double fee=0;
            if (Reader.HasRows)
            {
                fee = Convert.ToDouble(Reader["Fee"]);
            }
            Reader.Close();
            Connection.Close();
            return fee;
        }
    }
}