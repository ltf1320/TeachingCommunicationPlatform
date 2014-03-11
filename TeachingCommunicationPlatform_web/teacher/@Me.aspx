<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="@Me.aspx.cs" Inherits="teacher_Me" %>

<%-- 访问users文件夹\at -> 读取at内容 -> 找到相应消息内容 -> 显示 --%>
<%--功能:所有@,所有作业--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:DataList ID="DataList1" runat="server" Width="900px"  OnItemDataBound="DataList1_ItemDataBound">
        <ItemStyle BorderColor="Blue" BorderWidth="1" />
        <ItemTemplate>
            <table style="width:900px">
                <tr>
                    <td style="text-align:left">
                        <asp:Label ID="Label_cou" runat="server" Text='<%#Bind("couId") %>'></asp:Label>
                        <asp:Label ID="Label_topic" runat="server" Text='<%#Bind("topic") %>'></asp:Label>
                        <asp:Label ID="Label_task" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <asp:Label ID="Label_txt" runat="server" Text='<%#Bind("text") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <asp:Label ID="Label1" runat="server" Text="文件列表："></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="DataList_file" runat="server" Width="677px" OnItemDataBound="DataList1_ItemDataBound1">
                            <ItemTemplate>
                                <asp:Label ID="fileLabel" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="DataList_at" runat="server" Width="677px" OnItemDataBound="DataList_at_ItemDataBound" >
                            <ItemTemplate>
                                <asp:Label ID="Label_atName" runat="server" Text="@yiikou"></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>

                <tr>
                    <td style="text-align:right">
                        <asp:Label ID="Label_date" runat="server" Text="时间"></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle />
    </asp:DataList>
</asp:Content>