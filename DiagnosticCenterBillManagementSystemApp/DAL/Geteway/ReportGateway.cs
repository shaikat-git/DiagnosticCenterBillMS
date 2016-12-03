using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Geteway
{
    public class ReportGateway : Gateway
    {


        public TestRequest GetTestRequestDataByBillNo(string billNo)
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
            Command.Parameters["MobileNo"].Value = "";
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



        public List<TestWiseSpM> GetTestWiseReportData(string formDate, string toDate)
        {
            DateTime formDt = DateTime.ParseExact(formDate, "dd/MM/yy", null);
            DateTime toDt = DateTime.ParseExact(toDate, "dd/MM/yy", null);
            formDate = formDt.ToString("yyyy-MM-dd");
            toDate = toDt.ToString("yyyy-MM-dd");

            Query = "TestWiseReportSP";
            Command = new SqlCommand();
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = Query;
            Command.Connection = Connection;
            Command.Parameters.Clear();
            Command.Parameters.Add("FromDate", SqlDbType.Date);
            Command.Parameters["FromDate"].Value = formDate;
            Command.Parameters.Add("ToDate", SqlDbType.Date);
            Command.Parameters["ToDate"].Value = toDate;
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TestWiseSpM> testWiseSpMs = new List<TestWiseSpM>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    TestWiseSpM aTestWiseSpM = new TestWiseSpM();
                    aTestWiseSpM.TestName = Reader["TestName"].ToString();
                    aTestWiseSpM.TotalTest = (int)(Reader["TotalTest"]);
                    aTestWiseSpM.TotalAmount = Convert.ToDouble(Reader["TotalAmount"].ToString());
                    testWiseSpMs.Add(aTestWiseSpM);
                }
            }
            else
            {
                TestWiseSpM aTestWiseSpM = new TestWiseSpM();
                aTestWiseSpM.TestName = "";
                aTestWiseSpM.TotalTest = 0;
                aTestWiseSpM.TotalAmount = 0;
                testWiseSpMs.Add(aTestWiseSpM);
            }
            Reader.Close();
            Connection.Close();

            return testWiseSpMs;
        }

        public List<TypeWiseSpM> GetTypeWiseReportData(string formDate, string toDate)
        {
            DateTime formDt = DateTime.ParseExact(formDate, "dd/MM/yy", null);
            DateTime toDt = DateTime.ParseExact(toDate, "dd/MM/yy", null);
            formDate = formDt.ToString("yyyy-MM-dd");
            toDate = toDt.ToString("yyyy-MM-dd");

            Query = "TypeWiseReportSP";
            Command = new SqlCommand();
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = Query;
            Command.Connection = Connection;
            Command.Parameters.Clear();
            Command.Parameters.Add("FromDate", SqlDbType.Date);
            Command.Parameters["FromDate"].Value = formDate;
            Command.Parameters.Add("ToDate", SqlDbType.Date);
            Command.Parameters["ToDate"].Value = toDate;
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TypeWiseSpM> typeWiseSpMs = new List<TypeWiseSpM>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    TypeWiseSpM aTypeWiseSpM = new TypeWiseSpM();
                    aTypeWiseSpM.TestTypeName = Reader["TestTypeName"].ToString();
                    aTypeWiseSpM.TotalNoOfTest = (int)(Reader["TotalNoOfTest"]);
                    aTypeWiseSpM.TotalAmount = Convert.ToDouble(Reader["TotalAmount"].ToString());
                    typeWiseSpMs.Add(aTypeWiseSpM);
                }
            }
            else
            {
                TypeWiseSpM aTypeWiseSpM = new TypeWiseSpM();
                aTypeWiseSpM.TestTypeName = "";
                aTypeWiseSpM.TotalNoOfTest = 0;
                aTypeWiseSpM.TotalAmount = 0;
                typeWiseSpMs.Add(aTypeWiseSpM);
            }
            Reader.Close();
            Connection.Close();

            return typeWiseSpMs;
        }

        public List<UnpaidBillSpM> GetUnpaidBillReportData(string formDate, string toDate)
        {
            DateTime formDt = DateTime.ParseExact(formDate, "dd/MM/yy", null);
            DateTime toDt = DateTime.ParseExact(toDate, "dd/MM/yy", null);
            formDate = formDt.ToString("yyyy-MM-dd");
            toDate = toDt.ToString("yyyy-MM-dd");

            Query = "UnpaidBillReportSP";
            Command = new SqlCommand();
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = Query;
            Command.Connection = Connection;
            Command.Parameters.Clear();
            Command.Parameters.Add("FromDate", SqlDbType.Date);
            Command.Parameters["FromDate"].Value = formDate;
            Command.Parameters.Add("ToDate", SqlDbType.Date);
            Command.Parameters["ToDate"].Value = toDate;
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<UnpaidBillSpM> unpaidBillSpMs = new List<UnpaidBillSpM>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    UnpaidBillSpM aUnpaidBillSpM = new UnpaidBillSpM();
                    aUnpaidBillSpM.BillNumber = Reader["BillNumber"].ToString();
                    aUnpaidBillSpM.ContactNo = Reader["ContactNo"].ToString();
                    aUnpaidBillSpM.PatientName = Reader["PatientName"].ToString();
                    aUnpaidBillSpM.BillAmount = Convert.ToDouble(Reader["BillAmount"].ToString());
                    unpaidBillSpMs.Add(aUnpaidBillSpM);
                }
            }
            else
            {
                UnpaidBillSpM aUnpaidBillSpM = new UnpaidBillSpM();
                aUnpaidBillSpM.BillNumber = "";
                aUnpaidBillSpM.ContactNo = "";
                aUnpaidBillSpM.PatientName = "";
                aUnpaidBillSpM.BillAmount = 0;
                unpaidBillSpMs.Add(aUnpaidBillSpM);
            }
            Reader.Close();
            Connection.Close();

            return unpaidBillSpMs;
        }
    }
}