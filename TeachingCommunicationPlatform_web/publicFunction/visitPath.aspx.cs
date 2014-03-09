using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class publicFunction_visitCourse : System.Web.UI.Page
{
    safeFileManager sFileMana;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ha_path"] == null)
        {
            Response.Redirect("allCourse.aspx");
            return;
        }
        sFileMana = new safeFileManager();
        sFileMana.SetRootPath(Session["ha_path"].ToString());
        if(Session["ha_user"]!=null)
        {
            sFileMana.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString());
        }
    }
}