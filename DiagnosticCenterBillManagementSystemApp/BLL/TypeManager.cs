
using System.Collections.Generic;
using DiagnosticCenterBillManagementSystemApp.DAL.Geteway;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;

namespace DiagnosticCenterBillManagementSystemApp.BLL
{
    public class TypeManager
    {
        TypeGateway aTypeGateway = new TypeGateway();
        public string Save(TestType aTestType)
        {
            bool isExist = aTypeGateway.IsTypeExist(aTestType.TypeName);
            if (!isExist)
            {
                int effectedRow = aTypeGateway.Save(aTestType);
                if (effectedRow > 0)
                {
                    return "Save successfully!";
                }
                else
                {
                    return "Sorry! Saved fail";
                }
            }
            else
            {
                return "TestType name already exist";
            }

        }

        public List<TestType> GetAllType()
        {
            return aTypeGateway.GetAllType();
        }
    }
}