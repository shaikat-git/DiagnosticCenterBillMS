using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemApp.BLL;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;

namespace DiagnosticCenterBillManagementSystemApp.UI
{
    public partial class UnpaidBillReportUI : System.Web.UI.Page
    {
        ReportManager aReportManager = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (fromDateTextBox.Text == string.Empty)
            {
                unpaidBillReportMessageLabel.Text = "Enter From date";
                return;
            }
            if (toDateTextBox.Text == string.Empty)
            {
                unpaidBillReportMessageLabel.Text = "Enter To date";
                return;
            }

            string formDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            List<UnpaidBillSpM> unpaidBillSpMs = aReportManager.GetUnpaidBillReportData(formDate, toDate);
            unpaidBillGridView.DataSource = unpaidBillSpMs;
            unpaidBillGridView.DataBind();
            double total = 0;
            foreach (UnpaidBillSpM aUnpaidBillSpM in unpaidBillSpMs)
            {
                total += aUnpaidBillSpM.BillAmount;
            }
            totalTextBox.Text = total.ToString();
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            string formDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            aReportManager.CreateUnpaidBillReport(formDate, toDate);
            View(null, null);
        }

        protected void View(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open ('ReportViewerUI.aspx?ReportName=UnpaidBillReport','mywindow','menubar=1,resizable=1,width=500,height=500');", true);
        }
    }
}