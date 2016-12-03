using System;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Model
{
    public class UnpaidBillSpM
    {
        public string BillNumber { get; set; }
        public string ContactNo { get; set; }
        public string PatientName { get; set; }
        public double BillAmount { get; set; }

    }
}