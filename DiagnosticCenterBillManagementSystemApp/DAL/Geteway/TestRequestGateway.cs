using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Geteway
{
    public class TestRequestGateway : Gateway
    {

        public int Save(TestRequest aTestRequest)
        {
            if ((aTestRequest != null) && (aTestRequest.RequestDetailseList.Count != 0))
            {
                Query = "INSERT INTO Request (PatientName, Dob, MobileNo) VALUES (@PatientName, @Dob, @MobileNo);";
                Query += "DECLARE @maxRequestId int = (SELECT Max([Id]) FROM Request);";
                Command = new SqlCommand();
                Command.Parameters.Clear();
                Command.Parameters.Add("PatientName", SqlDbType.NVarChar);
                Command.Parameters["PatientName"].Value = aTestRequest.PatientName;
                Command.Parameters.Add("Dob", SqlDbType.Date);
                Command.Parameters["Dob"].Value = aTestRequest.Dob;
                Command.Parameters.Add("MobileNo", SqlDbType.NVarChar);
                Command.Parameters["MobileNo"].Value = aTestRequest.MobileNo;

                int index = 0;
                foreach (TestRequestDetails aTestRequestDetails in aTestRequest.RequestDetailseList)
                {
                    string testIdParam = "TestId" + index;
                    Command.Parameters.Add(testIdParam, SqlDbType.Int);
                    Command.Parameters[testIdParam].Value = aTestRequestDetails.TestId;
                    Query += string.Format("INSERT INTO RequestDetails VALUES (@maxRequestId, @{0});", testIdParam);
                    index++;
                }
                Command.CommandType = CommandType.Text;
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                int effectedRow = Command.ExecuteNonQuery();
                Connection.Close();
                return effectedRow;
            }
            else
            {
                return 0;
            }
        }

        public bool isMobileNoExist(string mobileNo)
        {
            Query = "SELECT * FROM Request WHERE MobileNo=@MobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("MobileNo", SqlDbType.NVarChar);
            Command.Parameters["MobileNo"].Value = mobileNo;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            bool isExist = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return isExist;
        }

        public string GetLastBillNoByMobileNo(string mobileNo)
        {
            Query = "SELECT BillNo FROM Request WHERE MobileNo=@MobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("MobileNo", SqlDbType.NVarChar);
            Command.Parameters["MobileNo"].Value = mobileNo;
            Connection.Open();
            Reader = Command.ExecuteReader();
            string billNo = String.Empty;
            Reader.Read();
            if (Reader.HasRows)
            {

                billNo = Reader["BillNo"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return billNo;
        }

        public TestRequest GetTestRequest(string billNo, string mobileNo)
        {
            Query = "TestRequestSP";
            Command = new SqlCommand();
            Command.CommandText = Query;
            Command.Connection = Connection;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Clear();
            Command.Parameters.Add("BillNo", SqlDbType.NVarChar);
            Command.Parameters["BillNo"].Value = billNo;
            Command.Parameters.Add("MobileNo", SqlDbType.NVarChar);
            Command.Parameters["MobileNo"].Value = mobileNo;
            Connection.Open();
            Reader = Command.ExecuteReader();
            TestRequest aTestRequest = new TestRequest();
            while (Reader.Read())
            {
                aTestRequest.Id = (int)Reader["RequestId"];
                aTestRequest.PatientName = Reader["PatientName"].ToString();
                aTestRequest.Dob = (DateTime)Reader["Dob"];
                aTestRequest.MobileNo = Reader["MobileNo"].ToString();
                aTestRequest.BillNo = Reader["BillNo"].ToString();
                aTestRequest.DueDate = (DateTime)Reader["DueDate"];
                aTestRequest.PaymentFlag = (bool)Reader["PaymentFlag"];

                TestRequestDetails aTestRequestDetails = new TestRequestDetails();
                aTestRequestDetails.TestFee = Convert.ToDouble(Reader["Fee"]);
                aTestRequestDetails.TestName = Reader["TestName"].ToString();
                aTestRequest.RequestDetailseList.Add(aTestRequestDetails);
            }
            aTestRequest.GetAmount();
            Reader.Close();
            Connection.Close();
            return aTestRequest;
        }

    }
}