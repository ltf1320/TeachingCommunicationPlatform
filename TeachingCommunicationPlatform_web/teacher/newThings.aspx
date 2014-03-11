<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="newThings.aspx.cs" Inherits="teacher_newThings" %>

<%-- 在users文件夹下找到newThings文件，按couId和id查找消息/作业并显示（提供链接） --%>
<%--页面设计--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:DataList ID="DataList1" runat="server" Width="877px"  OnItemDataBound="DataList1_ItemDataBound">
        <ItemTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label_cou" runat="server" Text='<%#Bind("couId") %>'></asp:Label>
                        <asp:Label ID="Label_topic" runat="server" Text='<%#Bind("topic") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label_txt" runat="server" Text='<%#Bind("text") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="文件列表："></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="DataList1" runat="server" Width="677px">
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="ListView1" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="Label_atName" runat="server" Text="@yiikou"></asp:Label>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label_date" runat="server" Text="时间"></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle />
    </asp:DataList>
</asp:Content>
