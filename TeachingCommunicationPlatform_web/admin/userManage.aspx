<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMasterPage.master" AutoEventWireup="true" CodeFile="userManage.aspx.cs" Inherits="admin_userManage" %>

<%-- 添加/删除用户 --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" DeleteCommand="delete from [manageCou] where [userId] = @original_userId 
delete from [Course] where [createUser]=@original_userId 
DELETE FROM [users] WHERE [userId] = @original_userId "
        InsertCommand="INSERT INTO [users] ([userId], [roleId], [Name], [email], [pwd], [createDate], [academy]) VALUES (@userId, @roleId, @Name, @email, @pwd, @createDate, @academy)" OldValuesParameterFormatString="original_{0}"
        SelectCommand="SELECT [userId], [roleId], [Name], [email], [pwd], [createDate], [academy] FROM [users]" UpdateCommand="UPDATE [users] SET [roleId] = @roleId, [Name] = @Name, [email] = @email, [pwd] = @pwd, [createDate] = @createDate, [academy] = @academy WHERE [userId] = @original_userId ">
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
    <div>
    <table style="width: 1000px">
        <tr>
            <td style="width: 870px;">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="userId" DataSourceID="SqlDataSource1" Width="840px" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="userId" HeaderText="用户名" ReadOnly="True" SortExpression="userId" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="角色"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label_role" runat="server" Text='<%#Bind("roleId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                            <asp:BoundField DataField="email" HeaderText="电子邮件" SortExpression="email" />
                            <asp:BoundField DataField="pwd" HeaderText="密码" SortExpression="pwd" />
                            <asp:BoundField DataField="createDate" HeaderText="创建时间" SortExpression="createDate" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="学院"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label_aca" runat="server" Text='<%#Bind("academy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="操作" ShowDeleteButton="True" ShowEditButton="True" />
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
                </asp:Panel>
                <asp:Button ID="newOneBtn" runat="server" Text="新建" OnClick="newOneBtn_Click" />
                <asp:Panel ID="Panel2" runat="server" Height="280px" Style="margin-top: 0px" Visible="False">
                    <table style="text-align: left">
                        <tr>
                            <td>
                                <asp:Label ID="roleLb" Text="角色" Width="80" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="roleTb" Width="150" runat="server"></asp:TextBox>
                                <asp:RangeValidator ID="roleRange" runat="server" ControlToValidate="roleTb" ErrorMessage="角色代号不正确" MaximumValue="3" MinimumValue="1" ValidationGroup="sub"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="nameLb" runat="server" Text="姓名" Width="80"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="nameTb" runat="server" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="nameTb" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="pwdLb" Text="密码" Width="80" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="pwdTb" Width="150" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="validator_pwd" ValidationGroup="sub" runat="server"
                                    ErrorMessage="此处不能为空" ControlToValidate="pwdTb"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="agapwdLb" Text="确认密码" Width="99px" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="agapwdTb" Width="150" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ID="Compare_pwd" runat="server" ControlToCompare="pwdTb" ControlToValidate="agapwdTb" ErrorMessage="两次密码不同" ValidationGroup="sub"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="emailLb" Text="邮箱" Width="80" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="emailTb" Width="150" runat="server" TextMode="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="validator_email" ValidationGroup="sub" runat="server"
                                    ErrorMessage="此处不能为空" ControlToValidate="emailTb"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="acaLb" Text="学院" Width="80" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="acDp" runat="server" DataSourceID="SqlDataSource2" DataTextField="acName" DataValueField="acId">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [acId], [acName] FROM [academy]"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="subBtn" runat="server" Text="提交" OnClick="subBtn_Click" ValidationGroup="sub" />
                                <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" Text="返回" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

