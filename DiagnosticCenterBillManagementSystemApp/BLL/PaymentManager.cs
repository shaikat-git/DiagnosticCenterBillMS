using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemApp.DAL.Geteway;

namespace DiagnosticCenterBillManagementSystemApp.BLL
{
    public class PaymentManager
    {
        PaymentGateway aPaymentGateway= new PaymentGateway();
        public string Update(int requestId)
        {
            int effectedRow = aPaymentGateway.Update(requestId);
            if (effectedRow>0)
            {
                return "Payment completed successfully!";
            }
            else
            {
                return "Payment not completed!";
            }
        }
    }
}