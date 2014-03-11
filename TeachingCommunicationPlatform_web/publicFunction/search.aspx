<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/basic.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="publicFunction_search" %>

<%--搜索课程名/教师名->列出相关课程/教师 ->访问文件/访问教师(allCourse.aspx)--%>
<%--功能：添加关注（非匿名者）--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="teacher">教师</asp:ListItem>
        <asp:ListItem Value="course">课程</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />--%>
    <asp:GridView ID="GridView_tea" EmptyDataText="找不到相关教师" runat="server" Width="1000px" AutoGenerateColumns="False" OnRowCommand="GridView_tea_RowCommand" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
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
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:GridView ID="GridView_cou" EmptyDataText="找不到相关课程" runat="server" Width="1000px" AutoGenerateColumns="False" OnRowCommand="couGridView_RowCommand" OnRowDataBound="couGridView_RowDataBound" DataSourceID="SqlDataSource2" style="margin-top: 2px" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="couName" HeaderText="课程名" SortExpression="couName" />
            <asp:BoundField DataField="type" HeaderText="类型" SortExpression="type" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="Label3" runat="server" Text="学期"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="termLabel" runat="server" Text='<%#Bind("term") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="createUser" HeaderText="教师" SortExpression="createUser" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="listenBtn" runat="server" CommandName="listen" CommandArgument='<%# Bind("couId") %>' Text="关注"></asp:LinkButton>
                    <asp:LinkButton ID="viewBtn" runat="server" CommandName="view" CommandArgument='<%# Bind("couId") %>' Text="查看文件"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2"  runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [couId],[couName], [type], [term], [createUser] FROM [Course] WHERE ([couName] LIKE '%' + @couName + '%')">
        <SelectParameters>
            <asp:QueryStringParameter Name="couName" QueryStringField="searchCou" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT users.userId, users.Name, users.email, academy.acName FROM users INNER JOIN academy ON users.academy = academy.acId WHERE (users.Name LIKE '%' + @Name + '%')">
        <SelectParameters>
            <asp:QueryStringParameter Name="Name" QueryStringField="searchTea" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


