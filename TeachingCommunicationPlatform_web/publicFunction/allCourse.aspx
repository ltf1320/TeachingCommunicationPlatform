﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/basic.master" AutoEventWireup="true" CodeFile="allCourse.aspx.cs" Inherits="publicFunction_allCourse" %>

<%-- 选择学院->选择教师->选择课程->访问文件 --%>
<%--功能：添加关注（非匿名者）--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [acName], [acId] FROM [academy]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [userId], [Name] FROM [users] WHERE (([academy] = @academy) AND ([roleId] = @roleId)) ">
        <SelectParameters>
            <asp:ControlParameter ControlID="acaDrop" Name="academy" PropertyName="SelectedValue" Type="String" />
            <asp:Parameter DefaultValue="2" Name="roleId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT * FROM [Course] WHERE ([createUser] = @createUser)">
        <SelectParameters>
            <asp:ControlParameter ControlID="teaDrop" Name="createUser" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT couId, Course.couName, Course.type, Course.stuNum, Course.term, Course.createUser FROM Course INNER JOIN users ON Course.createUser = users.userId WHERE (users.academy = @academy)">
        <SelectParameters>
            <asp:ControlParameter ControlID="acaDrop" Name="academy" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table id="table1" runat="server" style="width:1000px">
        <tr>
            <td>
                <asp:Label ID="label1" runat="server" Text="选择学院："></asp:Label>
                <asp:DropDownList ID="acaDrop" AutoPostBack="true" OnSelectedIndexChanged="acaDrop_SelectedIndexChanged" runat="server" DataSourceID="SqlDataSource1" DataTextField="acName" DataValueField="acId"></asp:DropDownList>
<%--            </td>
            <td>--%>
                <asp:Label ID="label2" runat="server" Text="选择教师："></asp:Label>
                <asp:DropDownList ID="teaDrop" AutoPostBack="True" OnSelectedIndexChanged="teaDrop_SelectedIndexChanged" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="couGridView" runat="server" EmptyDataText="暂无数据" Width="1000px" OnRowCommand="couGridView_RowCommand" OnRowDataBound="couGridView_RowDataBound" AutoGenerateColumns="False" DataKeyNames="couId" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="couName" HeaderText="课程名" SortExpression="couName" />
                        <asp:BoundField DataField="type" HeaderText="类别" SortExpression="type" />
                        <asp:BoundField DataField="stuNum" HeaderText="关注人数" SortExpression="stuNum" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label2" runat="server" Text="学期"></asp:Label>
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
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </td>
        </tr>
    </table>


</asp:Content>

