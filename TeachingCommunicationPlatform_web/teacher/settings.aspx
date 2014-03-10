<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="teacher_myThings" %>

<%-- 查看和修改个人信息 --%>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>"
        SelectCommand="SELECT * FROM [user] WHERE ([sid] = @sid)">
        <SelectParameters>
            <asp:SessionParameter Name="sid" SessionField="sid" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Panel ID="defaultPannel" runat="server">
        <asp:Label ID="Label6" Width="80" Text="班级:" runat="server"></asp:Label><asp:Label ID="classLabel" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label10" Text="姓名:" Width="80" runat="server"></asp:Label>
        <asp:TextBox ID="nametxt" Width="150" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="sub" runat="server"
            ErrorMessage="请输入姓名" ControlToValidate="nametxt"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label4" runat="server" Width="80" Text="性别:"></asp:Label>
        <asp:DropDownList ID="sex" runat="server" AutoPostBack="true">
            <asp:ListItem>男</asp:ListItem>
            <asp:ListItem>女</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label5" Width="80" Text="邮箱:" runat="server"></asp:Label>
        <asp:TextBox ID="postAdd" Width="150" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="Validator_haspostadd" runat="server" ValidationGroup="sub"
            ErrorMessage="请输入邮箱" ControlToValidate="postAdd"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="Validator_postadd" runat="server" ValidationGroup="sub"
            ErrorMessage="电子邮件格式错误" ControlToValidate="postAdd" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="Label9" Width="80" Text="生源地:" runat="server"></asp:Label>
        <asp:TextBox ID="sfrom" Width="150" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="修改" OnClick="updateData" />
        <asp:Button ID="Button4" runat="server" Text="更改密码" OnClick="onChangePwd" />
    </asp:Panel>
    <asp:Panel ID="pwdPannel" runat="server">
        <asp:Label ID="Label1" runat="server" Text="原始密码"></asp:Label>
        <asp:TextBox ID="oripwdtxt" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="pwd" runat="server"
            ControlToValidate="oripwdtxt" ErrorMessage="请输入原始密码"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label2" runat="server" Text="新密码"></asp:Label>
        <asp:TextBox ID="newpwdtxt" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="pwd" runat="server"
            ControlToValidate="oripwdtxt" ErrorMessage="请输入新密码"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label3" runat="server" Text="确认新密码"></asp:Label>
        <asp:TextBox ID="cpwdtxt" TextMode="Password" runat="server"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="pwd"
            ControlToValidate="cpwdtxt" ControlToCompare="newpwdtxt" ErrorMessage="两次输入密码不同"></asp:CompareValidator>
        <br />
        <asp:Button ID="Button2" runat="server" Text="确认" ValidationGroup="pwd" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="返回" OnClick="Button3_Click" />
    </asp:Panel>
</asp:Content>--%>
