using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Http;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace DiagnosticCenterBillManagementSystemApp.UI
{
    public partial class ReportViewerUI : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //------1---------------------- Open PDF File In Browser----------------------------------------
            string querString = Request.QueryString["ReportName"];

            if (querString != String.Empty)
            {
                string filePath = Server.MapPath("../PDFReports/") + querString + ".pdf";
                if (File.Exists(filePath))
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    byte[] ar = new byte[(int)fs.Length];
                    fs.Read(ar, 0, (int)fs.Length);
                    fs.Close();

                    Response.Clear();
                    Response.ContentType = "Application/pdf";
                    Response.AddHeader("content-disposition", "inline;filename=" + querString + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.TransmitFile(filePath);
                    Response.BinaryWrite(ar);
                    Response.End();
                }
                else
                {
                    Response.Write("Sorry! Report file not found!");
                }

            }


            //----2---------------------------------------
            //Document doc1 = new Document();
            //string path = Server.MapPath("../PDFReports");
            //PdfWriter.GetInstance(doc1, new FileStream(path + "/BillReport.pdf", FileMode.Create));
            //doc1.Open();
            //doc1.Add(new Paragraph("My first PDF report " + querString));  //Report Heading
            //doc1.Add(new Paragraph("  "));
            //PdfPTable table = new PdfPTable(3);
            //PdfPCell cell = new PdfPCell(new Phrase("Test details: "));  //Table Heading
            //cell.Colspan = 3;
            //cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            //table.AddCell(cell);


            //table.AddCell("Col 1 Row 1");
            //table.AddCell("Col 2 Row 1");
            //table.AddCell("Col 3 Row 1");
            //table.AddCell("Col 1 Row 2");
            //table.AddCell("Col 2 Row 2");
            //table.AddCell("Col 3 Row 2");
            //doc1.Add(table);
            //doc1.Close();

            //FileStream fs = new FileStream(path + "/BillReport.pdf", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //byte[] ar = new byte[(int)fs.Length];
            //fs.Read(ar, 0, (int)fs.Length);
            //fs.Close();

            //Response.Clear();
            //Response.ContentType = "Application/pdf";
            //Response.AddHeader("content-disposition", "inline;filename=BillReport.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.TransmitFile(path + "/BillReport.pdf");
            //Response.BinaryWrite(ar);
            //Response.End();

















            //-----------------------------Open PDF File in a Object-------------------------------------
            //Document doc1 = new Document();
            //string path = Server.MapPath("../PDFReports");
            //PdfWriter.GetInstance(doc1, new FileStream(path + "/BillReport.pdf", FileMode.Create));
            //doc1.Open();
            //doc1.Add(new Paragraph("PDF in Litaeral Control in a page hhhhhhhhhhhhhhhh"));
            //doc1.Close();
            //embedLiteral.Text = string.Empty;
            //string embed = @"<object data='{0}' type='application/pdf' width='100%' height='500px'></object>";
            //embedLiteral.Text = string.Format(embed, ResolveUrl("../PDFReports/BillReport.pdf"));


            //--------------------------------Vew HTML tages To PDF in Browser----------------------------------

            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "inline;filename=BillReport.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //panel1.RenderControl(hw);

            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //Response.Write(pdfDoc);
            //pdfDoc.Close();
            //Response.End();

            //sr.Close();
            //hw.Close();
            //sw.Close();



        }



        protected void View(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open ('ReportViewerUI.aspx','mywindow','menubar=1,resizable=1,width=800,height=600');", true);



        }

    }
}