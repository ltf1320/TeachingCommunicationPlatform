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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [couId], [couName], [type], [stuNum], [term], [createUser] FROM [Course]"
        DeleteCommand="delete from manageCou where couId=@original_couId DELETE FROM [Course] WHERE [couId] = @original_couId"
        InsertCommand="INSERT INTO [Course] ([couId], [couName], [type], [stuNum], [term], [createUser]) VALUES (@couId, @couName, @type, @stuNum, @term, @createUser)"
        UpdateCommand="UPDATE [Course] SET  [type] = @type, [term] = @term  WHERE [couId] = @original_couId" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="original_couId" Type="String" />
            <asp:Parameter Name="original_couName" Type="String" />
            <asp:Parameter Name="original_type" Type="String" />
            <asp:Parameter Name="original_stuNum" Type="Int32" />
            <asp:Parameter Name="original_term" Type="String" />
            <asp:Parameter Name="original_createUser" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="couId" Type="String" />
            <asp:Parameter Name="couName" Type="String" />
            <asp:Parameter Name="type" Type="String" />
            <asp:Parameter Name="stuNum" Type="Int32" />
            <asp:Parameter Name="term" Type="String" />
            <asp:Parameter Name="createUser" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="couName" Type="String" />
            <asp:Parameter Name="type" Type="String" />
            <asp:Parameter Name="stuNum" Type="Int32" />
            <asp:Parameter Name="term" Type="String" />
            <asp:Parameter Name="createUser" Type="String" />
            <asp:Parameter Name="original_couId" Type="String" />
            <asp:Parameter Name="original_couName" Type="String" />
            <asp:Parameter Name="original_type" Type="String" />
            <asp:Parameter Name="original_stuNum" Type="Int32" />
            <asp:Parameter Name="original_term" Type="String" />
            <asp:Parameter Name="original_createUser" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <table style="height: 546px">
        <tr>
            <td style="height: 243px; width: 476px;">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="couId" DataSourceID="SqlDataSource1" Width="777px" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="couId" HeaderText="课程编号" ReadOnly="True" SortExpression="couId" />
                            <asp:BoundField DataField="couName" HeaderText="课程名称" SortExpression="couName" ReadOnly="True" />
                            <asp:BoundField DataField="type" HeaderText="属性" SortExpression="type" />
                            <asp:BoundField DataField="stuNum" HeaderText="已选学生数" SortExpression="stuNum" ReadOnly="True" />
                            <asp:BoundField DataField="term" HeaderText="学期" SortExpression="term" />
                            <asp:BoundField DataField="createUser" HeaderText="教师" SortExpression="createUser" ReadOnly="True" />
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" HeaderText="操作" />
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="newOneBtn" runat="server" Text="新建" OnClick="newOneBtn_Click" />
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" Height="280px" Style="margin-top: 0px" Visible="False">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="couNameLb" runat="server" Text="课程名字:"></asp:Label>
                                <asp:TextBox ID="couNameTB" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="couNameRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="couNameTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="typeLb" runat="server" Text="课程属性:"></asp:Label>
                                <asp:TextBox ID="typeTB" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="typeRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="typeTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="termLb" runat="server" Text="学期:"></asp:Label>

                                <asp:TextBox ID="termTB" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="termRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="termTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="tidLB" runat="server" Text="教师ID:"></asp:Label>

                                <asp:TextBox ID="tidTB" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="tidRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="tidTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="subBtn" runat="server" Text="提交" OnClick="subBtn_Click" ValidationGroup="confirm" Style="height: 21px" />
                                <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" Text="返回" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </td>
        </tr>
    </table>
</asp:Content>

