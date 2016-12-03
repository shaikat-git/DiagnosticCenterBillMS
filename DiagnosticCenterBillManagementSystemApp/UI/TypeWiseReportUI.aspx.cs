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
    public partial class TypeWiseReportUI : System.Web.UI.Page
    {
        ReportManager aReportManager= new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (fromDateTextBox.Text == string.Empty)
            {
                typeWiseReportMessageLabel.Text = "Enter From date";
                return;
            }
            if (toDateTextBox.Text == string.Empty)
            {
                typeWiseReportMessageLabel.Text = "Enter To date";
                return;
            }

            string formDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            List<TypeWiseSpM> testWiseSpMs = aReportManager.GetTypeWiseReportData(formDate, toDate);
            typeWiseGridView.DataSource = testWiseSpMs;
            typeWiseGridView.DataBind();
            double total = 0;
            foreach (TypeWiseSpM aTestWiseSpM in testWiseSpMs)
            {
                total += aTestWiseSpM.TotalAmount;
            }
            totalTextBox.Text = total.ToString();
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            string formDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            aReportManager.CreateTypeWiseReport(formDate, toDate);
            View(null, null);
        }

        protected void View(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open ('ReportViewerUI.aspx?ReportName=TypeWiseReport','mywindow','menubar=1,resizable=1,width=500,height=500');", true);
        }
    }
}