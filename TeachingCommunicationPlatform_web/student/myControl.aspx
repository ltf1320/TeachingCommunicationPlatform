<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="myControl.aspx.cs" Inherits="student_myControl" %>

<%-- 列出已关注课程/关注课程（连接到publicFunction\allCourse.aspx)/取消关注--%>
<%--关注课程:
找到users下listens文件，添加课程(couId)
找到courses下listeners文件，添加userId--%>

<%--功能：课程管理者
管理课程列表
选中->发布（删除)消息/发布(删除)作业/查看上交作业--%>

<%--发布消息：
输入消息topic
消息内容
上传文件
@user
找到couse文件夹的message，添加信息
生成新的消息id
在course文件夹下找到listeners，找到所有listeners的user文件夹下的newthings文件，将couId,id添加进去
格式见pd--%>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="SELECT [couId] FROM [manageCou] WHERE ([userId] = @userId)">
        <SelectParameters>
            <asp:SessionParameter Name="userId" SessionField="ha_user" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="select * from [Course] where ([couId] in (SELECT couId FROM [manageCou] WHERE ([userId] = @userId)))
"
        DeleteCommand="delete from [manageCou ] where [userId] = @userId">
        <DeleteParameters>
            <asp:Parameter Name="userId" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="userId" SessionField="ha_user" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Panel ID="Panel1" runat="server">
        <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="1.2em" ForeColor="#666666" StaticSubMenuIndent="10px" OnMenuItemClick="Menu1_MenuItemClick">
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle BackColor="#E3EAEB" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <Items>
                <asp:MenuItem Text="关注的课程" Value="couList"></asp:MenuItem>
                <asp:MenuItem Text="发布消息" Value="postMsg"></asp:MenuItem>
                <asp:MenuItem Text="管理我的课程" Value="couMana"></asp:MenuItem>
            </Items>
            <StaticHoverStyle BackColor="#666666" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#1C5E55" />
        </asp:Menu>
    </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
        <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="1.2em" ForeColor="#666666" StaticSubMenuIndent="10px" OnMenuItemClick="Menu1_MenuItemClick">
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle BackColor="#E3EAEB" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <Items>
                <asp:MenuItem Text="关注的课程" Value="couList"></asp:MenuItem>
            </Items>
            <StaticHoverStyle BackColor="#666666" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#1C5E55" />
        </asp:Menu>
    </asp:Panel>
    <%--发布消息：
     输入消息topic
     消息内容
     上传文件
     @user
     找到couse文件夹的message，添加信息
     生成新的消息id
     在course文件夹下找到listeners，找到所有listeners的user文件夹下的newthings文件，将couId,id添加进去
     格式见pd--%>

    <asp:Panel ID="Panel_postMsg" runat="server">

        <table style="height: 222px; width: 350px; text-align: left">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="选择课程:"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="couId" DataValueField="couId">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label_topic" runat="server" Text="话题:"></asp:Label>
                    <asp:TextBox ID="TextBox_topic" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        <asp:ListItem Value="1">作业</asp:ListItem>
                        <asp:ListItem Value="0">消息</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label_deadline" runat="server" Text="截止时间:"></asp:Label>
                    <asp:TextBox ID="TextBox_deadline" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label_content" runat="server" Text="内容:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox_content" runat="server" Height="64px" TextMode="MultiLine" Width="295px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="Label_deadline0" runat="server" Text="@:"></asp:Label>
                    <asp:TextBox ID="TextBox_at" runat="server"></asp:TextBox>
                    <asp:Label ID="Label_hint" runat="server" Text="用&quot;,&quot;间隔"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="上传文件"></asp:Label>
                    <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" />
                </td>
                </tr>
            <tr>
                <td style="text-align:center">
                    <asp:Button ID="Button_sub0" Width="100px" runat="server" OnClick="Button_sub_Click" Text="提交" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel_listCou" runat="server">
        <table>
            <tr>
                <td>
                    <%-- 列出已关注课程/关注课程（连接到publicFunction\allCourse.aspx)/取消关注--%>
                    <%--关注课程:
                    找到users下listens文件，添加课程(couId)
                    找到courses下listeners文件，添加userId--
                    数据库中课程人数+1    --%>
                    <asp:GridView ID="GridView1" runat="server" EmptyDataText="你没有关注课程" Height="116px" Width="291px" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label_cou" runat="server" Visible="false" Text='<%#Bind("课程编号")  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="取消关注" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

        </table>

    </asp:Panel>

    <asp:Panel ID="Panel_couMana" Visible="false" runat="server">
        <table>
            <tr>
                <td>
                    <%--功能：课程管理者
                    管理课程列表
                    选中->发布（删除)消息/发布(删除)作业/查看上交作业/添加管理者（!!teacher only)--%>
                    <asp:Button ID="Button_add" runat="server" OnClick="Button_add_Click" Text="添加管理者" />
                    <asp:TextBox ID="TextBox_addperson" runat="server">管理者ID</asp:TextBox>
                    <asp:TextBox ID="TextBox_addcou" runat="server">管理的课程编号</asp:TextBox>
                    

                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel_couNew" runat="server">
        <table>
            <tr>
                <td>
                    <%--功能：新建和删除课程(!!teacher only）
                    新建课程：
                    输入课程名等
                    生成新couId(max+1)
                    在courses文件夹下创建相关文件（夹）--%>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="couId" DataSourceID="SqlDataSource1" OnRowDeleting="GridView2_RowDeleting" OnRowDataBound="GridView2_RowDataBound">
                        <Columns>
                            <%--<asp:BoundField DataField="couId" HeaderText="课程号" ReadOnly="True" SortExpression="couId" />--%>
                            <asp:BoundField DataField="couName" HeaderText="课程名" SortExpression="couName" />
                            <asp:BoundField DataField="type" HeaderText="类型" SortExpression="type" />
                            <asp:BoundField DataField="stuNum" HeaderText="学生数" SortExpression="stuNum" />
                            <asp:TemplateField>
                                <HeaderTemplate>DataControlRowType.DataRow
                                    <asp:Label ID="Label_term" runat="server" Text="学期"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label_term" runat="server" Text='<%#Bind("term") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="createUser" HeaderText="教师" SortExpression="createUser" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:Button runat="server" Text="新建" ID="newOneBtn" OnClick="newOneBtn_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel_couNew_new" runat="server" Visible="False">
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
                                    <asp:Button ID="subBtn" runat="server" Text="提交" OnClick="subBtn_Click" ValidationGroup="confirm" Style="height: 21px" />

                                    <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" Text="返回" />
                                </td>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>







