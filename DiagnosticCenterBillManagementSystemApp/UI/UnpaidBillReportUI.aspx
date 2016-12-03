<%@ Page Title="Unpaid Bill Report" Language="C#" MasterPageFile="~/MainUI.Master" AutoEventWireup="true" CodeBehind="UnpaidBillReportUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemApp.UI.UnpaidBillReportUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">

    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }

        .auto-style2 {
            width: 169px;
        }

        .auto-style3 {
            height: 32px;
            width: 169px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContainerCPH" runat="server">
    <div>
        <h1>Unpaid Bill Report</h1>
        <table>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                    <asp:TextBox ID="fromDateTextBox" runat="server"></asp:TextBox></td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                    <asp:TextBox ID="toDateTextBox" runat="server"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:Button ID="showButton" runat="server" Text="Show" OnClick="showButton_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="unpaidBillGridView" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Number">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("BillNumber") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Patient Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("PatientName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <itemstyle horizontalalign="Right" />
                                    <asp:Label runat="server" Text='<%#Eval("BillAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="pdfButton" runat="server" Text="PDF" OnClick="pdfButton_Click" /></td>
                <td class="auto-style3" style="text-align: right">
                    <asp:Label ID="Label4" runat="server" Text="Total"></asp:Label>&nbsp;
                    <asp:TextBox ID="totalTextBox" runat="server" Width="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="unpaidBillReportMessageLabel" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>


    <script src="../Scripts/jquery-1.12.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.js"></script>
    <script>
        $(function () {
            $("#<%=fromDateTextBox.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/y"
            });

            $("#<%=toDateTextBox.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/y"
            });
        });
    </script>
</asp:Content>



