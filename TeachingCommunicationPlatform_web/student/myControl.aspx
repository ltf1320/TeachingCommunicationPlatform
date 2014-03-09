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
