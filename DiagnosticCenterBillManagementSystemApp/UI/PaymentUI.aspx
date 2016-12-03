<%@ Page Title="" Language="C#" MasterPageFile="~/MainUI.Master" AutoEventWireup="true" CodeBehind="PaymentUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemApp.UI.PaymentUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContainerCPH" runat="server">
    <div>
        <h1>Payment</h1>
        <table>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="Bill No"></asp:Label></td>
                <td>
                    <asp:TextBox ID="billNoTextBox" runat="server"></asp:TextBox></td>
                <td style="text-align: left">OR</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="Mobile No"></asp:Label></td>
                <td>
                    <asp:TextBox ID="mobileNoTextBox" runat="server"></asp:TextBox></td>
                <td style="text-align: left">
                    <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click" /></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label3" runat="server" Text="Amount"></asp:Label></td>
                <td>
                    <asp:TextBox ID="amountTextBox" runat="server"></asp:TextBox></td>
                <td style="text-align: left"></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label4" runat="server" Text="Due Date"></asp:Label></td>
                <td>
                    <asp:TextBox ID="dueDateTextBox" runat="server"></asp:TextBox></td>
                <td style="text-align: left">
                    <asp:HiddenField ID="requestIdHiddenField" runat="server" Value="0" />
                    <asp:Button ID="payButton" runat="server" Text="Pay" OnClick="payButton_Click" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="paymentMessageLable" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>
