using System.Web.UI.WebControls;

namespace DiagnosticCenterBillManagementSystemApp.DAL.Model
{
    public class TestSetupVM
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public double TestFee { get; set; }
        public string TypeName { get; set; }
    }
}