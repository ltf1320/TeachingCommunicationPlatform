<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMasterPage.master" AutoEventWireup="true" CodeFile="couseManage.aspx.cs" Inherits="admin_couseManage" %>

<%-- 添加/删除课程 --%>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 221px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<table>
    <tr>
        <td style="width: 395px">
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT   couId as 课程编号, couName as 课程名称, type as 属性, stuNum as 已选学生 , term as 学期, createUser as 创建者 from Course" DeleteCommand="DELETE FROM Course WHERE (couId = @cid)
DELETE FROM manageCou WHERE (couId = @cid and userId = @tid)" InsertCommand="INSERT INTO Course(couId, couName, type,stuNum,term,createUser) VALUES (@cid, @cname, @ctype,  @cstunum, @ctime, @cre)
INSERT INTO manageCou (userId,couId) values(@tid,@couId)" UpdateCommand="
UPDATE Course SET   term = @ctime, type = @ctype, stuNum = @cstunum">
                <DeleteParameters>
                    <asp:Parameter Name="cid" />
                    <asp:Parameter Name="tid" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="cid" />
                    <asp:Parameter Name="cname" />
                    <asp:Parameter Name="ctype" />
                    <asp:Parameter Name="cstunum" />
                    <asp:Parameter Name="ctime" />
                    <asp:Parameter Name="cre" />
                    <asp:Parameter Name="tid" />
                    <asp:Parameter Name="couId" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ctime" />
                    <asp:Parameter Name="ctype" />
                    <asp:Parameter Name="cstunum" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="课程编号" DataSourceID="SqlDataSource1" Width="484px" >
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="课程编号" HeaderText="课程编号" ReadOnly="True" SortExpression="课程编号" />
                    <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" />
                    <asp:BoundField DataField="属性" HeaderText="属性" SortExpression="属性" />
                    <asp:BoundField DataField="已选学生" HeaderText="已选学生" SortExpression="已选学生" />
                    <asp:BoundField DataField="学期" HeaderText="学期" SortExpression="学期" />
                    <asp:BoundField DataField="创建者" HeaderText="创建者" SortExpression="创建者" />
                </Columns>
            </asp:GridView>
            
        </td>
        <td class="style1">
           
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 395px">
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

