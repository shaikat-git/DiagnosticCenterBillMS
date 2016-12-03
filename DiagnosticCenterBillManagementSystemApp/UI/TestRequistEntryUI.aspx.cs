using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemApp.BLL;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticCenterBillManagementSystemApp.UI
{
    public partial class TestRequistEntryUI : System.Web.UI.Page
    {
        TestManager aTestManager = new TestManager();
        TestRequestManager aTestRequestManager = new TestRequestManager();
        ReportManager aReportManager = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAllTest();
                GetFeeByTestId();
                testRequistGridView.DataSource = null;
                testRequistGridView.DataBind();

                if (ViewState["RequestDetails"] == null)
                {
                    ViewState["RequestDetails"] = new List<TestRequestDetails>();
                }
                BindGidViewList();
            }
        }

        private void GetAllTest()
        {
            List<TestSetupVM> testSetupVMs = aTestManager.GetAllTest();
            testDropDownList1.DataSource = testSetupVMs;
            testDropDownList1.DataTextField = "TestName";
            testDropDownList1.DataValueField = "TestId";
            testDropDownList1.DataBind();
        }

        protected void testDropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFeeByTestId();
        }

        private void GetFeeByTestId()
        {
            int testID = Convert.ToInt32(testDropDownList1.SelectedValue);
            requestFeeTextBox.Text = aTestManager.GetTestFeeById(testID).ToString();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            List<TestRequestDetails> testRequestDetailses = (List<TestRequestDetails>)ViewState["RequestDetails"];
            foreach (TestRequestDetails aTRD in testRequestDetailses)
            {
                if (aTRD.TestId == Convert.ToInt32(testDropDownList1.SelectedValue))
                {
                    testRequistMessageLable.Text = "Test name is already exist";
                    return;
                }
            }

            TestRequestDetails aTestRequestDetails = new TestRequestDetails();
            aTestRequestDetails.TestId = Convert.ToInt32(testDropDownList1.SelectedValue);
            aTestRequestDetails.TestName = testDropDownList1.SelectedItem.Text;
            aTestRequestDetails.TestFee = Convert.ToDouble(requestFeeTextBox.Text);
            testRequestDetailses.Add(aTestRequestDetails);
            BindGidViewList();
        }

        private void BindGidViewList()
        {
            testRequistGridView.DataSource = (List<TestRequestDetails>)ViewState["RequestDetails"];
            testRequistGridView.DataBind();
        }

        double totalFee = 0;
        protected void testRequistGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalFee = totalFee + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TestFee"));
                totalTextBox.Text = totalFee.ToString();
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            DateTime dob = DateTime.Now;
            if (dobTextBox.Text != string.Empty)
            {
                dob = DateTime.ParseExact(dobTextBox.Text, "dd/MM/yyyy", null);
            }

            testRequistMessageLable.Text = "";
            TestRequest aTestRequest = new TestRequest();
            aTestRequest.PatientName = patientNameTextBox.Text;
            aTestRequest.Dob = dob;
            aTestRequest.MobileNo = mobileNumberTextBox.Text;
            aTestRequest.RequestDetailseList = (List<TestRequestDetails>)ViewState["RequestDetails"];
            testRequistMessageLable.Text = aTestRequestManager.Save(aTestRequest);
            if (testRequistMessageLable.Text == "Save succesfully")
            {
                string billNo = aTestRequestManager.GetLastBillNoByMobileNo(aTestRequest.MobileNo);
                testRequistMessageLable.Text = aReportManager.CreateReport(billNo);
                View(null, null);
                ClearAllData();
            }
        }

        private void ClearAllData()
        {
            patientNameTextBox.Text = string.Empty;
            dobTextBox.Text = string.Empty;
            mobileNumberTextBox.Text = string.Empty;
            totalTextBox.Text = string.Empty;
            ViewState["RequestDetails"] = new List<TestRequestDetails>();
            testRequistGridView.DataSource = null;
            testRequistGridView.DataBind();
        }

        protected void View(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open ('ReportViewerUI.aspx?ReportName=BillReport','mywindow','menubar=1,resizable=1,width=500,height=500');", true);
        }
    }
}