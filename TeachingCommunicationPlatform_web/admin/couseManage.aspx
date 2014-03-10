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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <table style="height: 546px">
        <tr>
            <td style="width: 476px;">

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT   couId as 课程编号, couName as 课程名称, type as 属性, stuNum as 已选学生 , term as 学期, createUser as 创建者 from Course" DeleteCommand="DELETE FROM Course WHERE (couId = @cid)
DELETE FROM manageCou WHERE (couId = @cid and userId = @tid)"
                    InsertCommand="INSERT INTO Course(couId, couName, type,stuNum,term,createUser) VALUES (@cid, @cname, @ctype,  @cstunum, @ctime, @cre)
INSERT INTO manageCou (userId,couId) values(@tid,@couId)"
                    UpdateCommand="
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


            </td>
            <td class="style1"></td>
        </tr>
        <tr>
            <td style="height: 243px; width: 476px;">

                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="课程编号" DataSourceID="SqlDataSource1" Width="777px" OnRowCancelingEdit="GridView1_RowCancelingEdit"  OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="课程编号" HeaderText="课程编号" ReadOnly="True" SortExpression="课程编号" />
                            <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" ReadOnly="True" />
                            <asp:BoundField DataField="属性" HeaderText="属性" SortExpression="属性" />
                            <asp:BoundField DataField="已选学生" HeaderText="已选学生" SortExpression="已选学生" ReadOnly="True" />
                            <asp:BoundField DataField="学期" HeaderText="学期" SortExpression="学期" />
                            <asp:BoundField DataField="创建者" HeaderText="创建者" SortExpression="创建者" ReadOnly="True" />
                            <asp:CommandField ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                <asp:Button ID="newOneBtn" runat="server" Text="新建" OnClick="newOneBtn_Click" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 298px; width: 476px;">
                <asp:Panel ID="Panel2" runat="server" Height="280px" Style="margin-top: 0px" Visible="False">

                    <asp:Label ID="couNameLb" runat="server" Text="课程名字:"></asp:Label>
                    <asp:TextBox ID="couNameTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="couNameRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="couNameTB"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="typeLb" runat="server" Text="课程属性:"></asp:Label>
                    <asp:TextBox ID="typeTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="typeRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="typeTB"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="termLb" runat="server" Text="学期:"></asp:Label>

                    <asp:TextBox ID="termTB" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="termRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="termTB"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="tidLB" runat="server" Text="教师ID:"></asp:Label>

                    <asp:TextBox ID="tidTB" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="tidRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="tidTB"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Button ID="subBtn" runat="server" Text="提交" OnClick="subBtn_Click" />

                    <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" Text="返回" />

                </asp:Panel>

            </td>
        </tr>
    </table>
</asp:Content>

