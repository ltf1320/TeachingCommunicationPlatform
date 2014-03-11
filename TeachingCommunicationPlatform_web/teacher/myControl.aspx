<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/userMasterPage.master" AutoEventWireup="true" CodeFile="myControl.aspx.cs" Inherits="teacher_myControl" %>

<%-- 列出已关注课程/关注课程（连接到publicFunction\allCourse.aspx)/取消关注--%>
<%--关注课程:
找到users下listens文件，添加课程(couId)
找到courses下listeners文件，添加userId--
数据库中课程人数+1    --%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">





    <asp:Button ID="focusMoreBtn" runat="server" Height="21px" OnClick="focusMoreBtn_Click" Text="关注更多" Width="77px" />





</asp:Content>











<%--功能：课程管理者
管理课程列表
选中->发布（删除)消息/发布(删除)作业/查看上交作业/添加管理者（!!teacher only)--%>

<%--发布消息：
输入消息topic
消息内容
上传文件
@user
找到couse文件夹的message，添加信息
生成新的消息id
在course文件夹下找到listeners，找到所有listeners的user文件夹下的newthings文件，将couId,id添加进去
格式见pd--%>


<%--功能：新建和删除课程(!!teacher only）
新建课程：
输入课程名等
生成新couId(max+1)
在courses文件夹下创建相关文件（夹）--%>
