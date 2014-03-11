<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="myControl.aspx.cs" Inherits="teacher_myControl" %>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Panel ID="Panel1" runat="server">
        <asp:Button ID="focusMoreBtn" runat="server" Height="21px" OnClick="focusMoreBtn_Click" Text="关注更多" Width="77px" />
        <table style="height: 222px; width: 348px">
            <tr>
                <td>
                    <%--发布消息：
                    输入消息topic
                    消息内容
                    上传文件
                    @user
                    找到couse文件夹的message，添加信息
                    生成新的消息id
                    在course文件夹下找到listeners，找到所有listeners的user文件夹下的newthings文件，将couId,id添加进去
                    格式见pd--%>

                </td>
            </tr>
            <tr>
                <td>
                    <%-- 列出已关注课程/关注课程（连接到publicFunction\allCourse.aspx)/取消关注--%>
                    <%--关注课程:
                    找到users下listens文件，添加课程(couId)
                    找到courses下listeners文件，添加userId--
                    数据库中课程人数+1    --%>
                    <asp:GridView ID="GridView1" runat="server" Height="116px" Width="291px">
                    </asp:GridView>




                </td>

            </tr>

        </table>

    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server">
        <table>
            <tr>
                <td>
                    <%--功能：课程管理者
                    管理课程列表
                    选中->发布（删除)消息/发布(删除)作业/查看上交作业/添加管理者（!!teacher only)--%>


                </td>
            </tr>
            <tr>
                <td>
                    <%--功能：新建和删除课程(!!teacher only）
                    新建课程：
                    输入课程名等
                    生成新couId(max+1)
                    在courses文件夹下创建相关文件（夹）--%>

                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="couId" DataSourceID="SqlDataSource1" OnRowDeleting="GridView2_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="couId" HeaderText="couId" ReadOnly="True" SortExpression="couId" />
                            <asp:BoundField DataField="couName" HeaderText="couName" SortExpression="couName" />
                            <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
                            <asp:BoundField DataField="stuNum" HeaderText="stuNum" SortExpression="stuNum" />
                            <asp:BoundField DataField="term" HeaderText="term" SortExpression="term" />
                            <asp:BoundField DataField="createUser" HeaderText="createUser" SortExpression="createUser" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:Button runat="server" Text="新建" ID="newOneBtn" OnClick="newOneBtn_Click" />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TeachingCommunicationPlatform_DBConnectionString %>" SelectCommand="select * from [Course] where ([couId] in (SELECT couId FROM [manageCou] WHERE ([userId] = @userId)))
">
                        <SelectParameters>
                            <asp:SessionParameter Name="userId" SessionField="ha_user" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel3" runat="server" Visible="False">
                                         <asp:Label ID="couNameLb" runat="server" Text="课程名字:"></asp:Label>
                    <asp:TextBox ID="couNameTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="couNameRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="couNameTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="typeLb" runat="server" Text="课程属性:"></asp:Label>
                    <asp:TextBox ID="typeTB" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="typeRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="typeTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="termLb" runat="server" Text="学期:"></asp:Label>

                    <asp:TextBox ID="termTB" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="termRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="termTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="tidLB" runat="server" Text="教师ID:"></asp:Label>

                    <asp:TextBox ID="tidTB" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="tidRFV" runat="server" ErrorMessage="必须填写" ControlToValidate="tidTB" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Button ID="subBtn" runat="server" Text="提交" OnClick="subBtn_Click" ValidationGroup="confirm" style="height: 21px" />

                    <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" Text="返回" />
                    </asp:Panel>
                </td>
            </tr>
        </table>

    </asp:Panel>


</asp:Content>







