using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemApp.BLL;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;
using Microsoft.SqlServer.Server;

namespace DiagnosticCenterBillManagementSystemApp.UI
{
    public partial class PaymentUI : System.Web.UI.Page
    {

        TestRequestManager aTestRequestManager = new TestRequestManager();
        PaymentManager aPaymentManager = new PaymentManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string billNo = billNoTextBox.Text;
            string mobileNo = mobileNoTextBox.Text;
            requestIdHiddenField.Value = "0";
            TestRequest aTestRequest = aTestRequestManager.GetTestRequest(billNo, mobileNo);
            if (aTestRequest != null)
            {
                if (aTestRequest.PaymentFlag == false)
                {
                    amountTextBox.Text = aTestRequest.Amount.ToString();
                    dueDateTextBox.Text = aTestRequest.DueDate.ToString("dd-MM-yyyy");
                    requestIdHiddenField.Value = aTestRequest.Id.ToString();
                }
                else
                {
                    amountTextBox.Text = string.Empty;
                    dueDateTextBox.Text = string.Empty;
                    requestIdHiddenField.Value = "0";
                    paymentMessageLable.Text = "Payment is already done!";
                }
            }

        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            int requestId = Convert.ToInt32(requestIdHiddenField.Value);
            paymentMessageLable.Text = aPaymentManager.Update(requestId);
            requestIdHiddenField.Value = "0";
        }
    }
}