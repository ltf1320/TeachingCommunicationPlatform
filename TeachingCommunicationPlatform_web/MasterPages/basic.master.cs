using System;
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
    userType nUserType;
    protected void Page_Init(object sender, EventArgs e)
    {
        sqlhp = new SQLHelper();
        //Session["ha_user"] = "00001";// testing
        if (Session["ha_user"] == null)
        {
            userNameBtn.Visible = false;
            loginBtn.Text = "登陆";
            nUserType = userType.visitor;
        }
        else
        {
            _userName = Session["ha_user"].ToString();
            userName.Visible = false;
            pwdtxt.Visible = false;
            string sql = "select userName from users where userId=@userId";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@userId", _userName);
            sql = "select roleId from users where userId=@userId";
            userNameBtn.Text = sqlhp.getAValue(sql, para);
            para[0] = new SqlParameter("@userId", _userName);
            string userTypeStr = sqlhp.getAValue(sql, para);
            if (userTypeStr == "1")
                nUserType = userType.admin;
            else if (userTypeStr == "2")
                nUserType = userType.teacher;
            else nUserType = userType.student;
            loginBtn.Text = "注销";
        }
        sqlhp.close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlhp = new SQLHelper();
    }
    protected void allCourseBnt_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\publicFunction/allCourse.aspx");
    }
    protected void searchBnt_Click(object sender, EventArgs e)
    {
        if (inputSearchTbx.Text != "输入查询的关键字")
        {
            if(DropDownList1.SelectedValue=="teacher")
                Response.Redirect("\\publicFunction/search.aspx?teacher=" + Server.UrlEncode(inputSearchTbx.Text));
            else
                Response.Redirect("\\publicFunction/search.aspx?searchCou="+Server.UrlEncode(inputSearchTbx.Text));
        }
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
                    Session["ha_pwd"] = pwd;
                    Response.Write("<Script>alert('登陆成功');</Script>");
                    //Response.Redirect(Request.Url.ToString());
                    string rroleId = null;
                    safeFileManager sfRole = new safeFileManager();
                    rroleId = sfRole.getUserRole(sid);
                    if(rroleId=="1")
                    {
                        Response.Redirect("\\admin/couseManage.aspx");
                    }
                    else if(rroleId=="2")
                    {
                        Response.Redirect("\\teacher/newThings.aspx");
                    }
                    else
                    {
                        Response.Redirect("\\student/newThings.aspx");
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
            Response.Redirect("\\publicFunction/allCourse.aspx");
        }

    }
    protected void userNameBtn_Click(object sender, EventArgs e)
    {
        switch(nUserType)
        {
            case userType.admin:
                Response.Redirect("\\admin/couseManage.aspx");
                break;
            case userType.teacher:
                Response.Redirect("\\teacher/newThings.aspx");
                break;
            case userType.student:
                Response.Redirect("\\student/newThings.aspx");
                break;
            default: break;
        }
    }
}
