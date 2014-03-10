using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class teacher_newThings : System.Web.UI.Page
{
    safeFileManager sFM;
    string user;
    protected void Page_Load(object sender, EventArgs e)
    {
        sFM = new safeFileManager();
        if (Session["ha_user"] == null || Session["ha_pwd"] == null)
            Response.Redirect("publicFunction\allCourse.aspx");
        user=Session["ha_user"].ToString();
        sFM.setUser(user, Session["ha_pwd"].ToString());
        sFM.SetRootPath("users\\" + user);

    }
}