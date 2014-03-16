<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="@Me.aspx.cs" Inherits="teacher_Me" %>

<%-- 在users文件夹下找到newThings文件，按couId和id查找消息/作业并显示（提供链接） --%>
<%--页面设计--%>

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
                        <asp:DataList ID="DataList_file" runat="server" Width="677px" OnItemDataBound="DataList1_ItemDataBound1" OnItemCommand="DataList_file_ItemCommand">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="text-align:left">
                                            <asp:LinkButton id="downBtn" CommandName="downLoad" runat="server"></asp:LinkButton>
                                            </td>
                                    </tr>
                                </table>
                                
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <asp:DataList ID="DataList_at" runat="server" Width="677px" OnItemDataBound="DataList_at_ItemDataBound" >
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="text-align:left">
                                            <asp:Label ID="Label_atName" Font-Italic="true" ForeColor="Red" runat="server" Text="@yiikou"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
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
