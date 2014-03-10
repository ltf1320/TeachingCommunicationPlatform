<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="student_myThings" %>

<%-- 查看和修改个人信息 --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>"
        SelectCommand="SELECT * FROM [users] WHERE ([userId] = @ha_user)">
        <SelectParameters>
            <asp:SessionParameter Name="ha_user" SessionField="ha_user" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Panel ID="defaultPannel" runat="server" Height="281px" style="margin-top: 0px">
        <br />
        <asp:Label ID="Label10" Text="昵称：" Width="80px" runat="server"></asp:Label>
        <asp:TextBox ID="nametxt" Width="150" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="sub" runat="server"
            ErrorMessage="请输入昵称" ControlToValidate="nametxt"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="Label5" Width="80" Text="邮箱:" runat="server"></asp:Label>
        <asp:TextBox ID="postAdd" Width="150" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="Validator_haspostadd" runat="server" ValidationGroup="sub"
            ErrorMessage="请输入邮箱" ControlToValidate="postAdd"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="Validator_postadd" runat="server" ValidationGroup="sub"
            ErrorMessage="电子邮件格式错误" ControlToValidate="postAdd" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Label ID="IdLbl" runat="server" Text="ID:"></asp:Label>
        <asp:Label ID="IdContentLbl" runat="server"></asp:Label>
         <br />
         <br />
        <asp:Label ID="roleLbl" runat="server" Text="账号类别"></asp:Label>
        <asp:Label ID="roleContentLbl" runat="server"></asp:Label>
         <br />
         <br />
        <asp:Label ID="crtTimeLbl" runat="server" Text="注册时间"></asp:Label>
        <asp:Label ID="crtTimeContentLbl" runat="server"></asp:Label>
         <br />
         <br />
        <asp:Label ID="acLbl" runat="server" Text="学院"></asp:Label>
        <asp:Label ID="acContentLbl" runat="server"></asp:Label>
         <br />
         <br />
        <asp:Button ID="Button1" runat="server" OnClick="updateData" Text="修改" />
        <asp:Button ID="Button4" runat="server" OnClick="onChangePwd" Text="更改密码" />
    </asp:Panel>
    <asp:Panel ID="pwdPannel" runat="server" Height="151px">
        <asp:Label ID="Label1" runat="server" Text="原始密码"></asp:Label>
        <asp:TextBox ID="oripwdtxt" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="pwd" runat="server"
            ControlToValidate="oripwdtxt" ErrorMessage="请输入原始密码"></asp:RequiredFieldValidator>
        <br />
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
</asp:Content>
