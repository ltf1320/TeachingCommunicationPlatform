<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="newThings.aspx.cs" Inherits="teacher_newThings" %>

<%-- 在users文件夹下找到newThings文件，按couId和id查找消息/作业并显示（提供链接） --%>
<%--页面设计--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:GridView ID="GridView_nt" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView1_RowCommand" Font-Names="Verdana" Font-Size="12px" OnRowDataBound="GridView1_RowDataBound" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="couName" runat="server" Text="课程:"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
</asp:Content>
