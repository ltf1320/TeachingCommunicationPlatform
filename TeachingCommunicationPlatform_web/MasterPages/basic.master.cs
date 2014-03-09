using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_basic : System.Web.UI.MasterPage
{
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
