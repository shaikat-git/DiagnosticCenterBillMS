<%@ Page Title="Test request entry" Language="C#" MasterPageFile="~/MainUI.Master" AutoEventWireup="true" CodeBehind="TestRequistEntryUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemApp.UI.TestRequistEntryUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContainerCPH" runat="server">
    <div>
        <h1>Test Request Entry</h1>
        <table>
            <tr>
                <td style="text-align: right">
                    <asp:Label runat="server" Text="Name Of the Patient"></asp:Label></td>
                <td style="text-align: right">
                    <asp:TextBox ID="patientNameTextBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label runat="server" Text="Date Of Birth"></asp:Label></td>
                <td style="text-align: right">
                    <asp:TextBox ID="dobTextBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label runat="server" Text="Mobile Number"></asp:Label></td>
                <td style="text-align: right">
                    <asp:TextBox ID="mobileNumberTextBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label runat="server" Text="Select Test"></asp:Label></td>
                <td style="text-align: right">
                    <asp:DropDownList ID="testDropDownList1" runat="server" OnSelectedIndexChanged="testDropDownList1_SelectedIndexChanged" AutoPostBack="True" Width="173"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    </td>
                <td style="text-align: right"> <asp:Label runat="server" Text="Fee"></asp:Label>&nbsp;
                    <asp:TextBox ID="requestFeeTextBox" runat="server" Enabled="False" Width="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right"></td>
                <td style="text-align: right">
                    <asp:Label ID="testRequistMessageLable" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: right"></td>
                <td style="text-align: right">
                    <asp:Button ID="addButton" runat="server" Text="Add" OnClick="addButton_Click"  /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="testRequistGridView" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowDataBound="testRequistGridView_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Test">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TestFee") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
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
                <td></td>
                <td style="text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                    <asp:TextBox ID="totalTextBox" runat="server" Width="50" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><asp:LinkButton ID="pdfViewLinkButton" runat="server" Text="View PDF" OnClick="View" Visible="False" ></asp:LinkButton></td>
                <td style="text-align: right">
                    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
                </td>
            </tr>
        </table>
    </div>
    
    
      <script src="../Scripts/jquery-1.12.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.js"></script>
    <script>
        $(function () {
            $("#<%=dobTextBox.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
