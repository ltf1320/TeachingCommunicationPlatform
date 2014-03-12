<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/basic.master" AutoEventWireup="true" CodeFile="visitPath.aspx.cs" Inherits="publicFunction_visitCourse" %>

<%--列出该课程所有文件--%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 5px; text-align: left">
        <asp:Label ID="Label1" Font-Bold="true" runat="server" Font-Names="Verdana" Font-Size="12px" Text="路径："></asp:Label>
        <asp:Label ID="lblCurrentPath" Font-Bold="true" runat="server" Font-Names="Verdana" Font-Size="12px"></asp:Label>
        <asp:LinkButton ID="returnBtn" runat="server" OnClick="BackSpaceFolder" Text="返回上一级" Font-Size="10px"></asp:LinkButton>
    </div>

    <div>
        <asp:GridView ID="GridView1" runat="server" EmptyDataText="暂无文件" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView1_RowCommand" Font-Names="Verdana" Font-Size="12px" OnRowDataBound="GridView1_RowDataBound" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <input type="checkbox" name="checkedAll" id="checkedAll" />
                    </HeaderTemplate>
                    <ItemStyle Width="3%" Wrap="False" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="text-align:left">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Name") %>' Text='<%# Eval("Name") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreationDate" HeaderText="创建日期">
                    <ItemStyle Width="12%" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Size" HeaderText="大小" DataFormatString="{0} KB">
                    <ItemStyle HorizontalAlign="Right" Width="5%" Wrap="False" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>

