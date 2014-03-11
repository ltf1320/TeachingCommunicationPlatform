using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class publicFunction_search : System.Web.UI.Page
{
    SQLHelper sqlHelper;
    public bool jsp_return;
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlHelper = new SQLHelper();
        GridView_cou.Visible =false;
        GridView_tea.Visible = false;
        if (Request.QueryString["searchCou"] != null)
        {
            string cou =Server.UrlDecode(Request.QueryString["searchCou"].ToString());
            GridView_cou.Visible = true;
            GridView_cou.DataBind();
        }
        if (Request.QueryString["searchTea"] != null)
        {
            string tea = Server.UrlDecode(Request.QueryString["searchTea"].ToString());
            GridView_tea.Visible = true;
            GridView_tea.DataBind();
        }

    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if(DropDownList1.SelectedValue=="teacher")
    //    {
    //        GridView_tea.Visible = true;
    //        GridView_cou.Visible = false;
    //        GridView_tea.DataBind();
    //    }
    //    if(DropDownList1.SelectedValue=="course")
    //    {
    //        GridView_cou.Visible = true;
    //        GridView_tea.Visible = false;
    //        GridView_cou.DataBind();
    //    }
    //}
    protected void couGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "listen")
        {
            Methods.listen(Session["ha_user"].ToString(), e.CommandArgument.ToString());
            Methods.showMessageBox(Response, "关注成功！");
        }
        if (e.CommandName == "cancelListen")
        {
            Methods.cancelListen(Session["ha_user"].ToString(), e.CommandArgument.ToString());
            Methods.showMessageBox(Response, "已取消关注！");
        }
        if (e.CommandName == "view")
        {
            Session["ha_path"] = safeFileManager.getPath(safeFileManager.folderType.course, e.CommandArgument.ToString());
            Response.Redirect("visitPath.aspx");
        }
    }
    protected void couGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = (Label)e.Row.FindControl("termLabel");
            lb.Text = Methods.analyseTerm(lb.Text);
            LinkButton listenBtn = (LinkButton)e.Row.FindControl("listenBtn");
            if (Session["ha_user"] == null)
            {
                listenBtn.Visible = false;
                return;
            }
            string user = Session["ha_user"].ToString();
            DataRowView dataRow = (DataRowView)e.Row.DataItem;
            string couId = dataRow[0].ToString();
            if (Methods.isListened(user,couId))
            {
                listenBtn.Text = "取消关注";
                listenBtn.CommandArgument = "cancelListen";
            }
            else
            {
                listenBtn.Text = "关注";
                listenBtn.CommandArgument = "listen";
            }
            sqlHelper.close();
        }
    }
    protected void GridView_tea_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string value = "teacher=" + Server.UrlEncode(e.CommandArgument.ToString());
        Response.Redirect("allCourse.aspx?" + value);
    }
}