<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/basic.master" AutoEventWireup="true" CodeFile="allCourse.aspx.cs" Inherits="publicFunction_allCourse" %>

<%-- 选择学院->选择教师->选择课程->访问文件 --%>
<%--功能：添加关注（非匿名者）--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [acName], [acId] FROM [academy]"></asp:SqlDataSource>
    <table id="table1" runat="server">
        <tr>
            <td>
                <asp:DropDownList ID="acaDrop" runat="server" DataSourceID="SqlDataSource1" DataTextField="acName" DataValueField="acId"></asp:DropDownList>
            </td>
            <td>
               <%-- me
                <asp:DropDownList ID="teaDrop" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="userId"></asp:DropDownList>
            --%></td>
             </tr>
        <tr>
            <td>
               
            </td>
            <td>
<%--                me
                <asp:GridView ID="couGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="couId" DataSourceID="SqlDataSource3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="couName" HeaderText="课程名" SortExpression="couName" />
                        <asp:BoundField DataField="type" HeaderText="类别" SortExpression="type" />
                        <asp:BoundField DataField="stuNum" HeaderText="学生数" SortExpression="stuNum" />
                        <asp:BoundField DataField="term" HeaderText="学期" SortExpression="term" />
                        <asp:BoundField DataField="createUser" HeaderText="教师" SortExpression="createUser" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>--%>
            </td>
        </tr>
    </table>


</asp:Content>

