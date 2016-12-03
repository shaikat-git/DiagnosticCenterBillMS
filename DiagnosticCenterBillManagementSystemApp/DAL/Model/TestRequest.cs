using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Model
{
    public class TestRequest
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime Dob { get; set; }
        public string MobileNo { get; set; }
        public string BillNo { get; set; }
        public DateTime DueDate { get; set; }
        public bool PaymentFlag { get; set; }
        public double Amount { get; set; }
        public List<TestRequestDetails> RequestDetailseList{ get; set; }

        public TestRequest()
        {
            RequestDetailseList = new List<TestRequestDetails>();

        }

        public double GetAmount()
        {
            foreach (TestRequestDetails aTestRequestDetails in RequestDetailseList)
            {
                Amount += aTestRequestDetails.TestFee;
            }
            return Amount;
        }
    }
}