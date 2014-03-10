<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/basic.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="publicFunction_search" %>

<%--搜索课程名/教师名->列出相关课程/教师 ->访问文件/访问教师(allCourse.aspx)--%>
<%--功能：添加关注（非匿名者）--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="teacher">教师</asp:ListItem>
        <asp:ListItem Value="course">课程</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
    <asp:GridView ID="GridView_tea" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView_tea_RowCommand" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
            <asp:BoundField DataField="email" HeaderText="电子邮件" SortExpression="email" />
            <asp:BoundField DataField="acName" HeaderText="学院" SortExpression="acName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="choose" runat="server" CommandArgument='<%# Bind("userId") %>' Text="查看"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GridView_cou" runat="server" AutoGenerateColumns="False" OnRowCommand="couGridView_RowCommand" OnRowDataBound="couGridView_RowDataBound" DataSourceID="SqlDataSource2">
        <Columns>
            <asp:BoundField DataField="couName" HeaderText="课程名" SortExpression="couName" />
            <asp:BoundField DataField="type" HeaderText="类型" SortExpression="type" />
            <asp:BoundField DataField="term" HeaderText="学期" SortExpression="term" />
            <asp:BoundField DataField="createUser" HeaderText="教师" SortExpression="createUser" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="listenBtn" runat="server" CommandName="listen" CommandArgument='<%# Bind("couId") %>' Text="关注"></asp:LinkButton>
                    <asp:LinkButton ID="viewBtn" runat="server" CommandName="view" CommandArgument='<%# Bind("couId") %>' Text="查看文件"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [couName], [type], [term], [createUser] FROM [Course] WHERE ([couName] LIKE '%' + @couName + '%')">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" Name="couName" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT users.userId, users.Name, users.email, academy.acName FROM users INNER JOIN academy ON users.academy = academy.acId WHERE (users.Name LIKE '%' + @Name + '%')">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" Name="Name" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


