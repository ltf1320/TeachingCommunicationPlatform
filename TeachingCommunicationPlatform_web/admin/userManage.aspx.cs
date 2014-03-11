using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class admin_userManage : System.Web.UI.Page
{
    safeFileManager sf = new safeFileManager();
    SQLHelper sqlhp = new SQLHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void backBtn_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        Panel1.Visible = true;
        // newOneBtn.Visible = true ;
    }
    protected void subBtn_Click(object sender, EventArgs e)
    {
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        string uname = nameTb.Text.Trim();
        string urole = roleTb.Text.Trim();
        string upwd = pwdTb.Text.Trim();
        string uemail = emailTb.Text.Trim();
        string uaca = acaTb.Text.Trim();
        string selstr = "select max(userId) from users";
        SqlParameter[] paras = new SqlParameter[1];
        string uuserId = sqlhp.getAValue(selstr, paras);
        if (uuserId == "")
        { uuserId = "-1"; }
        uuserId = (Convert.ToInt32(uuserId) + 1).ToString();
        sqlhp.close();
        if (Methods.mkUser(uuserId, urole, uname, upwd, uemail, uaca))
        {
            Methods.showMessageBox(Response, "添加成功");
            Response.Redirect("\\admin/userManage.aspx");
        }
        else
        {
            Methods.showMessageBox(Response, "添加失败");
        }
    }
    protected void newOneBtn_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        //newOneBtn.Visible = false;
    }
}