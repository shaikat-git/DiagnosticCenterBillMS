<%@ Page Title="Report Viewer UI" Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemApp.UI.ReportViewerUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer UI</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
<%--            <asp:Panel ID="panel1" runat="server" Width="300" Visible="False">
                <h1 style="color: red;">This From HTML PAge</h1>
            </asp:Panel>--%>
<%--            <asp:LinkButton ID="pdfViewLinkButton" runat="server" Text="View PDF" OnClick="View" ></asp:LinkButton>
            <hr />--%>
            <asp:Literal ID="embedLiteral" runat="server" />
        </div>
    </form>
</body>
</html>
