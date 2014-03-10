﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class MasterPages_basic : System.Web.UI.MasterPage
{
    string _userName;
    SQLHelper sqlhp;
    protected void Page_Init(object sender, EventArgs e)
    {
        //Session["ha_user"] = "00001";// testing
        if (Session["ha_user"] == null)
        {
            userNameLabel.Visible = false;
            loginBtn.Text = "登陆";
        }
        else
        {
            _userName = Session["ha_user"].ToString();
            userName.Visible = false;
            pwdtxt.Visible = false;
            userNameLabel.Text = _userName;
            loginBtn.Text = "注销";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlhp = new SQLHelper();
    }
    protected void allCourseBnt_Click(object sender, EventArgs e)
    {
        Response.Redirect("publicFunction/allCourse.aspx");
    }
    protected void searchBnt_Click(object sender, EventArgs e)
    {
        if (inputSearchTbx.Text != "输入查询的课程")
            Response.Redirect("publicFunction/search.aspx");
        else
            return;
    }
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        if (loginBtn.Text == "登陆")
        {
            safeFileManager sfLoging = new safeFileManager();
            string sid = userName.Text.ToString();
            string pwd = pwdtxt.Text.ToString();
            try
            {
                if (sfLoging.setUser(sid, pwd))
                {
                    Session["ha_user"] = sid;
                    Response.Write("<Script>alert('登陆成功');</Script>");
                    //Response.Redirect(Request.Url.ToString());
                    string rroleId = null;
                    safeFileManager sfRole = new safeFileManager();
                    rroleId = sfRole.getUserRole(sid);
                    if(rroleId=="1")
                    {
                        Response.Redirect("../admin/couseManage.aspx");
                    }
                    else if(rroleId=="2")
                    {
                        Response.Redirect("../teacher/newThings.aspx");
                    }
                    else
                    {
                        Response.Redirect("../student/newThings.aspx");
                    }

                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Login", "<script>alert('用户名或密码错误');</script>");
            }
            catch (SqlException exception)
            {
                Response.Write("<Script>alert('数据库连接错误');</Script>");
            }
        }
        else
        {
            Session["ha_user"] = null;
            Response.Redirect("../publicFunction/allCourse.aspx");
        }

    }
}
