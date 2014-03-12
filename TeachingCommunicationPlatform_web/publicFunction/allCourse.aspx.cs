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
//        Session["ha_user"] = "00000";
//        Session["ha_pwd"] = "11111";
        if (!IsPostBack)
        {
            if (Request.QueryString["teacher"] != null)
            {
                string teaId = Server.UrlDecode(Request.QueryString["teacher"].ToString());
                string sql = "select academy from users where userId=@teaId";
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter("@teaId", teaId);
                try
                {
                    string acId = sqlHelper.getAValue(sql, para);
                    acaDrop.DataBind();
                    acaDrop.SelectedValue = acId;
                    acaDrop_SelectedIndexChanged(null, null);
                    teaDrop.SelectedValue = teaId;
                    teaDrop_SelectedIndexChanged(null, null);
                    couGridView.Visible = true;
                }
                catch (Exception ex)
                {
                    Methods.showMessageBox(Response, "查询失败");
                }
            }
            else
            {
                acaDrop.DataBind();
                acaDrop_SelectedIndexChanged(null, null);
            }
        }
    }

    protected void couGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="listen")
        {
            Methods.listen(Session["ha_user"].ToString(), e.CommandArgument.ToString());
            Methods.showMessageBox(Response, "关注成功！");
        }
        if (e.CommandName == "cancelListen")
        {
            Methods.cancelListen(Session["ha_user"].ToString(), e.CommandArgument.ToString());
            Methods.showMessageBox(Response, "已取消关注！");
        }
        if(e.CommandName=="view")
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
            string sql = "select count(*) from manageCou where userId=@userId and couId=@couId";
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@userId", user);
            para[1] = new SqlParameter("@couId", couId);
            if(Convert.ToInt32(sqlHelper.getAValue(sql, para))==1)
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
    protected void acaDrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        couGridView.DataSource = SqlDataSource4;
        couGridView.DataBind();
        ListItem item = new ListItem("未选择", "");
        teaDrop.Items.Clear();
        teaDrop.Items.Add(item);
        string sql = "select userId,Name from users where academy=@aca";
        SqlParameter[] para = new SqlParameter[1];
        para[0] = new SqlParameter("@aca", acaDrop.SelectedValue);
        SqlDataReader rder = sqlHelper.getReader(sql, para);
        try
        {
            while (rder.Read())
            {
                teaDrop.Items.Add(new ListItem(rder[1].ToString(), rder[0].ToString()));
            }
            
        }
        catch(Exception ex)
        {
            Methods.showMessageBox(Response, "数据库连接错误");
        }
        rder.Close();
    }
    protected void teaDrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (teaDrop.SelectedValue == "null")
            return;
        couGridView.DataSource = SqlDataSource3;
        couGridView.DataBind();
    }
}