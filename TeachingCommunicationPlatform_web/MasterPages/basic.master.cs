using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_basic : System.Web.UI.MasterPage
{
    string _userName;
    SQLHelper sqlhp;
    protected void Page_Init(object sender, EventArgs e)
    {
        if(Session["userName"]==null)
        {
            userNameLabel.Visible = false;
            loginBtn.Text = "登陆";
        }
        else
        {
            _userName = Session["userName"].ToString();
            userName.Visible = false;
            pwdtxt.Visible = false;
            loginDropList.Visible = false;
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
        string sid = userName.Text.ToString();
        string pwd = pwdtxt.Text.ToString();
        string constr = "select count(*) from user where userId='" + sid + "'" + " and pwd='" + pwd + "'";
        //try
        //{
            if (Convert.ToInt32(sqlhp.getAValue(constr, null)) == 1)
            {
                Session["ha_user"] = sid;
                //       Response.Redirect("stuManager.aspx");
                Response.Write("<Script>alert('登陆成功');window.location=Request.Url.ToString( ) ;</Script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Login", "<script>alert('用户名或密码错误');</script>");
            sqlhp.close();
        //}
        //catch (SqlException exception)
        //{
        //    Response.Write("<Script>alert('数据库连接错误');</Script>");
        //}
    }
}
