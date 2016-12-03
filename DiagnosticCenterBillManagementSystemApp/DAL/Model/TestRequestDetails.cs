using System;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Model
{
    [Serializable]
    public class TestRequestDetails
    {
        //public int Id { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public double TestFee { get; set; }

    }
}