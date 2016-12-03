using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DiagnosticCenterBillManagementSystemApp.DAL.Geteway;
using DiagnosticCenterBillManagementSystemApp.DAL.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;

namespace DiagnosticCenterBillManagementSystemApp.BLL
{
    public class ReportManager
    {
        ReportGateway aReportGateway = new ReportGateway();
        public string CreateReport(string billNo)
        {
            TestRequest aTestRequest = aReportGateway.GetTestRequestDataByBillNo(billNo);

            Document doc1 = new Document(PageSize.A4, 50, 50, 25, 25);
            string path = HttpContext.Current.Server.MapPath("../PDFReports/BillReport.pdf");
            string fileName = Path.GetFileName(path);
            PdfWriter.GetInstance(doc1, new FileStream(path, FileMode.Create));
            doc1.Open();
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);

            var logo = Image.GetInstance(HttpContext.Current.Server.MapPath("../Content/bitm-logo.png"));
            logo.ScaleAbsolute(75f, 50f);
            logo.SetAbsolutePosition(50, 750);
            doc1.Add(logo);
            Paragraph aParagraph = new Paragraph("Diagnostic Center Bill Management System", titleFont);
            aParagraph.Alignment = 2;
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(aParagraph);
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));

            doc1.Add(new Paragraph("Biil of Test Requist", titleFont));  //Report Heading
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("Print Date: " + DateTime.Now.ToString()));
            doc1.Add(new Paragraph("Bill No: " + aTestRequest.BillNo));
            doc1.Add(new Paragraph("Patient Name: " + aTestRequest.PatientName));
            doc1.Add(new Paragraph("Date of Birth: " + aTestRequest.Dob.ToString("dd/MM/yyyy")));
            doc1.Add(new Paragraph("Mobile No: " + aTestRequest.MobileNo));
            doc1.Add(new Paragraph("  "));

            PdfPTable table = new PdfPTable(3);
            table.SetWidths(new float[] { 1f, 15f, 3f });
            table.HorizontalAlignment = 0;
            table.WidthPercentage = 100;
            table.SpacingBefore = 10;
            table.SpacingAfter = 10;
            table.ExtendLastRow = false;

            PdfPCell cell = new PdfPCell();
            table.AddCell(new PdfPCell(new Phrase("SL")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("TEST")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("FEE")) { GrayFill = 0.7f });
            int index = 1;
            double total = 0;
            foreach (TestRequestDetails aTestRequestDetails in aTestRequest.RequestDetailseList)
            {
                cell = new PdfPCell(new Phrase(index.ToString()));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestRequestDetails.TestName));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestRequestDetails.TestFee.ToString()));
                cell.HorizontalAlignment = 2;
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                index++;
                total += aTestRequestDetails.TestFee;
            }

            cell = new PdfPCell(new Phrase("Total: " + total.ToString()));
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            doc1.Add(table);
            doc1.Close();
            return null;

        }

        public List<TestWiseSpM> GetTestWiseReportData(string formDate, string toDate)
        {
            return aReportGateway.GetTestWiseReportData(formDate, toDate);
        }

        public string CreateTestWiseReport(string formDate, string toDate)
        {
            List<TestWiseSpM> testWiseSpMs = aReportGateway.GetTestWiseReportData(formDate, toDate);

            DateTime formDt = DateTime.ParseExact(formDate, "dd/MM/yy", null);
            DateTime toDt = DateTime.ParseExact(toDate, "dd/MM/yy", null);
            formDate = formDt.ToString("yyyy-MM-dd");
            toDate = toDt.ToString("yyyy-MM-dd");

            Document doc1 = new Document(PageSize.A4, 50, 50, 25, 25);
            string path = HttpContext.Current.Server.MapPath("../PDFReports/TestWiseReport.pdf");
            string fileName = Path.GetFileName(path);
            PdfWriter.GetInstance(doc1, new FileStream(path, FileMode.Create));
            doc1.Open();
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);

            Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("../Content/bitm-logo.png"));
            logo.ScaleAbsolute(75f, 50f);
            logo.SetAbsolutePosition(50, 750);
            doc1.Add(logo);

            Paragraph aParagraph = new Paragraph("Diagnostic Center Bill Management System", titleFont);
            aParagraph.Alignment = 2;
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(aParagraph);
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));

            doc1.Add(new Paragraph("Test Wise Report", titleFont));//Report Heading
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("From Date: " + formDate));
            doc1.Add(new Paragraph("To Date: " + toDate));
            doc1.Add(new Paragraph("  "));

            PdfPTable table = new PdfPTable(4);
            table.SetWidths(new float[] { 1f, 15f, 2f, 3f });
            table.HorizontalAlignment = 0;
            table.WidthPercentage = 100;
            table.SpacingBefore = 10;
            table.SpacingAfter = 10;
            table.ExtendLastRow = false;

            PdfPCell cell = new PdfPCell();
            //table.AddCell("SL");
            table.AddCell(new PdfPCell(new Phrase("SL")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Test Name")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Total Test")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Total Amount")) { GrayFill = 0.7f });
            int index = 1;
            double total = 0;
            foreach (TestWiseSpM aTestWiseSpM in testWiseSpMs)
            {
                cell = new PdfPCell(new Phrase(index.ToString()));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestWiseSpM.TestName));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestWiseSpM.TotalTest.ToString()));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestWiseSpM.TotalAmount.ToString()));
                cell.HorizontalAlignment = 2;
                table.AddCell(cell);

                index++;
                total += aTestWiseSpM.TotalAmount;
            }

            cell = new PdfPCell(new Phrase("Total: " +  total.ToString()));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            doc1.Add(table);
            doc1.Close();
            return null;
        }

        public List<TypeWiseSpM> GetTypeWiseReportData(string formDate, string toDate)
        {
          return aReportGateway.GetTypeWiseReportData(formDate, toDate);
        }

        public string CreateTypeWiseReport(string formDate, string toDate)
        {
            List<TypeWiseSpM> typeWiseSpMs = aReportGateway.GetTypeWiseReportData(formDate, toDate);

            DateTime formDt = DateTime.ParseExact(formDate, "dd/MM/yy", null);
            DateTime toDt = DateTime.ParseExact(toDate, "dd/MM/yy", null);
            formDate = formDt.ToString("yyyy-MM-dd");
            toDate = toDt.ToString("yyyy-MM-dd");

            Document doc1 = new Document(PageSize.A4, 50, 50, 25, 25);
            string path = HttpContext.Current.Server.MapPath("../PDFReports/TypeWiseReport.pdf");
            string fileName = Path.GetFileName(path);
            PdfWriter.GetInstance(doc1, new FileStream(path, FileMode.Create));
            doc1.Open();
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);

            var logo = Image.GetInstance(HttpContext.Current.Server.MapPath("../Content/bitm-logo.png"));
            logo.ScaleAbsolute(75f, 50f);
            logo.SetAbsolutePosition(50, 750);
            doc1.Add(logo);
            Paragraph aParagraph = new Paragraph("Diagnostic Center Bill Management System", titleFont);
            aParagraph.Alignment = 2;
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(aParagraph);
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));

            doc1.Add(new Paragraph("Type Wise Report", titleFont));//Report Heading
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("From Date: " + formDate));
            doc1.Add(new Paragraph("To Date: " + toDate));
            doc1.Add(new Paragraph("  "));

            PdfPTable table = new PdfPTable(4);
            table.SetWidths(new float[] { 1f, 5f, 5f, 3f });
            table.HorizontalAlignment = 0;
            table.WidthPercentage = 100;
            table.SpacingBefore = 10;
            table.SpacingAfter = 10;
            table.ExtendLastRow = false;

            PdfPCell cell = new PdfPCell();
            //table.AddCell("SL");
            table.AddCell(new PdfPCell(new Phrase("SL")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Test Type Name")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Total No Of Test")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Total Amount")) { GrayFill = 0.7f });
            int index = 1;
            double total = 0;
            foreach (TypeWiseSpM aTestWiseSpM in typeWiseSpMs)
            {
                cell = new PdfPCell(new Phrase(index.ToString()));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestWiseSpM.TestTypeName));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestWiseSpM.TotalNoOfTest.ToString()));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aTestWiseSpM.TotalAmount.ToString()));
                cell.HorizontalAlignment = 2;
                table.AddCell(cell);

                index++;
                total += aTestWiseSpM.TotalAmount;
            }

            cell = new PdfPCell(new Phrase("Total: " + total.ToString()));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            doc1.Add(table);
            doc1.Close();
            return null;
        }

        public List<UnpaidBillSpM> GetUnpaidBillReportData(string formDate, string toDate)
        {
            return aReportGateway.GetUnpaidBillReportData(formDate, toDate);
        }

        public string CreateUnpaidBillReport(string formDate, string toDate)
        {
            List<UnpaidBillSpM> unpaidBillSpMs = aReportGateway.GetUnpaidBillReportData(formDate, toDate);

            Document doc1 = new Document(PageSize.A4, 50, 50, 25, 25);
            string path = HttpContext.Current.Server.MapPath("../PDFReports/UnpaidBillReport.pdf");
            string fileName = Path.GetFileName(path);
            PdfWriter.GetInstance(doc1, new FileStream(path, FileMode.Create));
            doc1.Open();
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);

            var logo = Image.GetInstance(HttpContext.Current.Server.MapPath("../Content/bitm-logo.png"));
            logo.ScaleAbsolute(75f, 50f);
            logo.SetAbsolutePosition(50, 750);
            doc1.Add(logo);
            Paragraph aParagraph = new Paragraph("Diagnostic Center Bill Management System", titleFont);
            aParagraph.Alignment = 2;
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(aParagraph);
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("  "));

            doc1.Add(new Paragraph("Unpaid Bill Report", titleFont));//Report Heading
            doc1.Add(new Paragraph("  "));
            doc1.Add(new Paragraph("From Date: " + formDate));
            doc1.Add(new Paragraph("To Date: " + toDate));
            doc1.Add(new Paragraph("  "));

            PdfPTable table = new PdfPTable(5);
            table.SetWidths(new float[] { 1f, 5f, 5f, 5f ,3f });
            table.HorizontalAlignment = 0;
            table.WidthPercentage = 100;
            table.SpacingBefore = 10;
            table.SpacingAfter = 10;
            table.ExtendLastRow = false;

            PdfPCell cell = new PdfPCell();
            //table.AddCell("SL");
            table.AddCell(new PdfPCell(new Phrase("SL")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Bill Number")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Contact No")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Patient Name")) { GrayFill = 0.7f });
            table.AddCell(new PdfPCell(new Phrase("Bill Amount")) { GrayFill = 0.7f });
            int index = 1;
            double total = 0;
            foreach (UnpaidBillSpM aUnpaidBillSpM in unpaidBillSpMs)
            {
                cell = new PdfPCell(new Phrase(index.ToString()));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aUnpaidBillSpM.BillNumber));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aUnpaidBillSpM.ContactNo));
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aUnpaidBillSpM.PatientName));
                cell.HorizontalAlignment = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(aUnpaidBillSpM.BillAmount.ToString()));
                cell.HorizontalAlignment = 2;
                table.AddCell(cell);

                index++;
                total += aUnpaidBillSpM.BillAmount;
            }

            cell = new PdfPCell(new Phrase("Total: " + total.ToString()));
            cell.Colspan = 5;
            cell.Border = 0;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            doc1.Add(table);
            doc1.Close();
            return null;
        }
    }
}