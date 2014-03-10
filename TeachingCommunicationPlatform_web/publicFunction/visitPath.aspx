<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/basic.master" AutoEventWireup="true" CodeFile="visitPath.aspx.cs" Inherits="publicFunction_visitCourse" %>

<%--列出该课程所有文件--%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Images/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jqModal.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#checkedAll").click(function () {
                if ($(this).attr("checked") == true) { // 全选
                    $("input[@type='checkbox']").each(function () {
                        $(this).attr("checked", true);
                    });
                } else { // 取消全选
                    $("input[@type='checkbox']").each(function () {
                        $(this).attr("checked", false);
                    });
                }
            });
        });

        $().ready(function () {
            $('#divCreate').jqm({ trigger: '#create' });
            $('#divRename').jqm({ trigger: '#rename' });
            $('#divDelete').jqm({ trigger: '#delete' });
            $('#divUpload').jqm({ trigger: '#upload' });
            $('#divCopy').jqm({ trigger: '#copy' });
            $('#divPaste').jqm({ trigger: '#paste' });
            $('#divEdit').jqm({ trigger: '#edit' });
            $('#divCut').jqm({ trigger: '#cut' });
            $('#divCreateFile').jqm({ trigger: '#createfile' });
            $('#divErr').jqm();
            $('#divEditFile').jqm();
        });
    </script>
    <div style="padding: 5px;">
        <strong>路径: </strong>
        <asp:Label ID="lblCurrentPath" Font-Bold="true" runat="server" Font-Names="Verdana" Font-Size="15px"></asp:Label>
    </div>

    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView1_RowCommand" Font-Names="Verdana" Font-Size="12px" OnRowDataBound="GridView1_RowDataBound" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <input type="checkbox" name="checkedAll" id="checkedAll" />
                    </HeaderTemplate>
                    <ItemStyle Width="3%" Wrap="False" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("FullName") %>' Text='<%# Eval("Name") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreationDate" HeaderText="创建日期">
                    <ItemStyle Width="12%" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Size" HeaderText="大小" DataFormatString="{0} KB">
                    <ItemStyle HorizontalAlign="Right" Width="5%" Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="下载">
                    <ItemTemplate>
                        <asp:Label ID="NameText" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>

