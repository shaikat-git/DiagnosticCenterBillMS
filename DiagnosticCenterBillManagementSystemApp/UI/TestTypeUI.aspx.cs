using System;
using System.Collections.Generic;
using DiagnosticCenterBillManagementSystemApp.BLL;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;

namespace DiagnosticCenterBillManagementSystemApp.UI
{
    public partial class TestTypeUI : System.Web.UI.Page
    {
        TypeManager aTypeManager = new TypeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAllType();
        }

        private void GetAllType()
        {
            List<TestType> types = aTypeManager.GetAllType();
            typeGridView.DataSource = types;
            typeGridView.DataBind();
        }

        protected void typeSaveButton_Click(object sender, EventArgs e)
        {
            if (typeNameTextBox.Text != string.Empty)
            {
                TestType aType = new TestType();
                aType.TypeName = typeNameTextBox.Text;
                typeMessageLable.Text = aTypeManager.Save(aType);
                typeNameTextBox.Text = string.Empty;
                GetAllType();
            }
            else
            {
                typeMessageLable.Text = "Please enter test type";
            }

        }

    }
}