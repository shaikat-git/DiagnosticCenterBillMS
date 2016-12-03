using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemApp.BLL;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace DiagnosticCenterBillManagementSystemApp.UI
{
    public partial class TestWiseReportUI : System.Web.UI.Page
    {
        ReportManager aReportManager = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (fromDateTextBox.Text == string.Empty)
            {
                testWiseReportMessageLabel.Text = "Enter From date";
                return;
            }
            if (toDateTextBox.Text == string.Empty)
            {
                testWiseReportMessageLabel.Text = "Enter To date";
                return;
            }

            string formDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            List<TestWiseSpM> testWiseSpMs = aReportManager.GetTestWiseReportData(formDate, toDate);
            testWiseGridView.DataSource = testWiseSpMs;
            testWiseGridView.DataBind();
            double total = 0;
            foreach (TestWiseSpM aTestWiseSpM in testWiseSpMs)
            {
                total += aTestWiseSpM.TotalAmount;
            }
            totalTextBox.Text = total.ToString();
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            string formDate = fromDateTextBox.Text;
            string toDate = toDateTextBox.Text;
            aReportManager.CreateTestWiseReport(formDate, toDate);
            View(null, null);
        }

        protected void View(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open ('ReportViewerUI.aspx?ReportName=TestWiseReport','mywindow','menubar=1,resizable=1,width=500,height=500');", true);
        }
    }
}