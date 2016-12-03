using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web;
using DiagnosticCenterBillManagementSystemApp.DAL.Geteway;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticCenterBillManagementSystemApp.BLL
{
    public class TestRequestManager
    {
        TestRequestGateway aTestRequestGateway = new TestRequestGateway();
        public string Save(TestRequest aTestRequest)
        {
            if (aTestRequest.MobileNo.Length == 11)
            {
                bool isMobileNoExist = aTestRequestGateway.isMobileNoExist(aTestRequest.MobileNo);
                if (!isMobileNoExist)
                {
                    int effectedRow = aTestRequestGateway.Save(aTestRequest);
                    if (effectedRow > 0)
                    {
                        return "Save succesfully";

                    }
                    else
                    {
                        return "Sorry, saved fail!";
                    }
                }
                else
                {
                    return "Mobile number already exist";
                }
            }
            else
            {
                return "Mobile number must be 11 character long";
            }
        }

        public TestRequest GetTestRequest(string billNo, string mobileNo)
        {
            return aTestRequestGateway.GetTestRequest(billNo, mobileNo);
        }
        public string GetLastBillNoByMobileNo(string mobileNo)
        {
            return aTestRequestGateway.GetLastBillNoByMobileNo(mobileNo);
        }

       
    }
}