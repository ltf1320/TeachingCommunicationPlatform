using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_basic : System.Web.UI.MasterPage
{
    string _userName;
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
            pwd.Visible = false;
            loginDropList.Visible = false;
            userNameLabel.Text = _userName;
            loginBtn.Text = "注销";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

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

}
