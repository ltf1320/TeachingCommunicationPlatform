using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class publicFunction_allCourse : System.Web.UI.Page
{
    SQLHelper sqlHelper;
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlHelper = new SQLHelper();
    }

    protected void couGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="listen")
        {
            Methods.listen(Session["ha_user"].ToString(), e.CommandArgument.ToString());
            Methods.showMessageBox(Response, "关注成功！");
        }
        if(e.CommandName=="view")
        {
            Session["ha_path"] = safeFileManager.getPath(safeFileManager.folderType.course, e.CommandArgument.ToString());
            Response.Redirect("visitPath.aspx");
        }
    }
    protected void couGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(Session["ha_user"]==null)
        {
            LinkButton listenBtn=(LinkButton)e.Row.FindControl("listenBtn");
            listenBtn.Visible = false;
        }
        string user = Session["ha_user"].ToString();
        DataRowView dataRow = (DataRowView)e.Row.DataItem;
        string couId = dataRow[0].ToString();
        string sql = "select count(*) from manageCou where userId=@userId and couId=@couId";
        SqlParameter[] para = new SqlParameter[2];
        para[0] = new SqlParameter("@userId", user);
        para[1] = new SqlParameter("@couId", couId);

    }
}