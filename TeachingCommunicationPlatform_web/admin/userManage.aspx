<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMasterPage.master" AutoEventWireup="true" CodeFile="userManage.aspx.cs" Inherits="admin_userManage" %>

<%-- 添加/删除用户 --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <table style="height: 546px">
        <tr>
            <td style="width: 870px;">

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" DeleteCommand="delete from [manageCou] where [userId] = @original_userId 
delete from [Course] where [createUser]=@original_userId 
DELETE FROM [users] WHERE [userId] = @original_userId " InsertCommand="INSERT INTO [users] ([userId], [roleId], [Name], [email], [pwd], [createDate], [academy]) VALUES (@userId, @roleId, @Name, @email, @pwd, @createDate, @academy)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [userId], [roleId], [Name], [email], [pwd], [createDate], [academy] FROM [users]" UpdateCommand="UPDATE [users] SET [roleId] = @roleId, [Name] = @Name, [email] = @email, [pwd] = @pwd, [createDate] = @createDate, [academy] = @academy WHERE [userId] = @original_userId ">
                    <DeleteParameters>
                        <asp:Parameter Name="original_userId" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="userId" Type="String" />
                        <asp:Parameter Name="roleId" Type="Int32" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="email" Type="String" />
                        <asp:Parameter Name="pwd" Type="String" />
                        <asp:Parameter Name="createDate" Type="DateTime" />
                        <asp:Parameter Name="academy" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="roleId" Type="Int32" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="email" Type="String" />
                        <asp:Parameter Name="pwd" Type="String" />
                        <asp:Parameter Name="createDate" Type="DateTime" />
                        <asp:Parameter Name="academy" Type="String" />
                        <asp:Parameter Name="original_userId" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>

            </td>
            <td class="style1"></td>
        </tr>
        <tr>
            <td style="height: 243px; width: 870px;">

                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="userId" DataSourceID="SqlDataSource1" Width="840px" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="userId" HeaderText="userId" ReadOnly="True" SortExpression="userId" />
                            <asp:BoundField DataField="roleId" HeaderText="roleId" SortExpression="roleId" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                            <asp:BoundField DataField="pwd" HeaderText="pwd" SortExpression="pwd" />
                            <asp:BoundField DataField="createDate" HeaderText="createDate" SortExpression="createDate" />
                            <asp:BoundField DataField="academy" HeaderText="academy" SortExpression="academy" />
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="newOneBtn" runat="server" Text="新建" OnClick="newOneBtn_Click" />

                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 298px; width: 870px;">
                <asp:Panel ID="Panel2" runat="server" Height="280px" Style="margin-top: 0px" Visible="False">

                    <asp:Label ID="roleLb" Text="角色" Width="80" runat="server"></asp:Label>
                    <asp:TextBox ID="roleTb" Width="150" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="roleRange" runat="server" ControlToValidate="roleTb" ErrorMessage="角色代号不正确" MaximumValue="3" MinimumValue="1" ValidationGroup="sub"></asp:RangeValidator>
                    <br />
                    <asp:Label ID="nameLb" runat="server" Text="姓名" Width="80"></asp:Label>
                    <asp:TextBox ID="nameTb" runat="server" Width="150"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_name" ValidationGroup="sub" runat="server"
                        ErrorMessage="此处不能为空" ControlToValidate="nameTb"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="pwdLb" Text="密码" Width="80" runat="server"></asp:Label>
                    <asp:TextBox ID="pwdTb" Width="150" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_pwd" ValidationGroup="sub" runat="server"
                        ErrorMessage="此处不能为空" ControlToValidate="pwdTb"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="agapwdLb" Text="再次输入密码" Width="99px" runat="server"></asp:Label>
                    <asp:TextBox ID="agapwdTb" Width="150" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="Compare_pwd" runat="server" ControlToCompare="pwdTb" ControlToValidate="agapwdTb" ErrorMessage="两次密码不同" ValidationGroup="sub"></asp:CompareValidator>
                    <br />
                    <asp:Label ID="emailLb" Text="邮箱" Width="80" runat="server"></asp:Label>
                    <asp:TextBox ID="emailTb" Width="150" runat="server" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_email" ValidationGroup="sub" runat="server"
                        ErrorMessage="此处不能为空" ControlToValidate="emailTb"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="acaLb" Text="学院" Width="80" runat="server"></asp:Label>
                    <asp:TextBox ID="acaTb" Width="150" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_aca" ValidationGroup="sub" runat="server"
                        ErrorMessage="此处不能为空" ControlToValidate="acaTb"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Button ID="subBtn" runat="server" Text="提交" OnClick="subBtn_Click" ValidationGroup="sub" />

                    <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" Text="返回" />

                </asp:Panel>

            </td>
        </tr>
    </table>
</asp:Content>

