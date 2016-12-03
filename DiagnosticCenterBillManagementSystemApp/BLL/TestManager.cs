using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemApp.DAL.Geteway;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;

namespace DiagnosticCenterBillManagementSystemApp.BLL
{
    public class TestManager
    {
        TestGateway aTestGateway = new TestGateway();
        public string Save(Test aTest)
        {
            bool IsTestExist = aTestGateway.IsTestExist(aTest.TestName);
            if (!IsTestExist)
            {
                int effectedRow = aTestGateway.Save(aTest);
                if (effectedRow > 0)
                {
                    return "Save successfuly";
                }
                else
                {
                    return "Sorry, save fail!";
                }
            }
            else
            {
                return "Test name already exist!";
            }
        }

        public List<TestSetupVM> GetAllTest()
        {
            return aTestGateway.GetAllTest();
        }

        public double GetTestFeeById(int testId)
        {
            return aTestGateway.GetTestFeeById(testId);
        }
    }
}