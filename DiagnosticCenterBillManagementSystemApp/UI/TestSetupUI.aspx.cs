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
    public partial class TestSetupUI : System.Web.UI.Page
    {
        TypeManager aTypeManager = new TypeManager();
        TestManager aTestManager = new TestManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<TestType> types = aTypeManager.GetAllType();
                testTypeDropDownList1.DataSource = types;
                testTypeDropDownList1.DataTextField = "TypeName";
                testTypeDropDownList1.DataValueField = "Id";
                testTypeDropDownList1.DataBind();
            }
            GetAllTest();
        }

        private void GetAllTest()
        {
            List<TestSetupVM> testVMs = aTestManager.GetAllTest();
            testGridView.DataSource = testVMs;
            testGridView.DataBind();
        }

        protected void testSaveButton_Click(object sender, EventArgs e)
        {
            if (testNameTextBox.Text != "")
            {
                if (feeTextBox.Text != "")
                {
                    Test aTest = new Test();
                    aTest.TestName = testNameTextBox.Text;
                    aTest.Fee = Convert.ToDouble(feeTextBox.Text);
                    aTest.TypeId = Convert.ToInt32(testTypeDropDownList1.SelectedValue);
                    testMessageLable.Text = aTestManager.Save(aTest);
                    GetAllTest();

                    testNameTextBox.Text = string.Empty;
                    feeTextBox.Text = string.Empty;
                    testNameTextBox.Focus();
                }
                else
                {
                    testMessageLable.Text = "Please fill Fee";
                }
            }
            else
            {
                testMessageLable.Text = "Please fill Test Name";
            }
        }

    }
}