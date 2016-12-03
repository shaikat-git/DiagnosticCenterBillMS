<%@ Page Title="Test Setup" Language="C#" MasterPageFile="~/MainUI.Master" AutoEventWireup="true" CodeBehind="TestSetupUI.aspx.cs" Inherits="DiagnosticCenterBillManagementSystemApp.UI.TestSetupUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContainerCPH" runat="server">
    <div>
        <h1>Test Setup</h1>
        <table>
            <tr>
                <td style="text-align: right"><asp:Label runat="server" Text="Test Name"></asp:Label></td>
                <td><asp:TextBox ID="testNameTextBox" runat="server"></asp:TextBox></td>
            </tr> 
            <tr>
                <td style="text-align: right"><asp:Label runat="server" Text="Fee"></asp:Label></td>
                <td><asp:TextBox ID="feeTextBox" runat="server" ></asp:TextBox>&nbsp;BDT</td>
            </tr> 
             <tr>
                <td style="text-align: right"><asp:Label runat="server" Text="Test Type"></asp:Label></td>
                <td><asp:DropDownList ID="testTypeDropDownList1" runat="server"></asp:DropDownList></td>
            </tr> 
            <tr>
                <td style="text-align: right"></td>
                <td><asp:Label ID="testMessageLable" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: right"></td>
                <td><asp:Button ID="testSaveButton" runat="server" Text="Save" OnClick="testSaveButton_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="testGridView" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Test Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Fee">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TestFee") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TypeName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
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
        </table>
    </div>
</asp:Content>
