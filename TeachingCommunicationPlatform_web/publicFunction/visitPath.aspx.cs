using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;


public partial class publicFunction_visitCourse : System.Web.UI.Page
{
    safeFileManager sFileMana;
    SQLHelper sqlHelper;
    string rootName;
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlHelper = new SQLHelper();
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
        dataBind();
    }
    protected void dataBind()
    {
        GridView1.DataSource = sFileMana.GetItems();
        GridView1.DataBind();
        StringBuilder path=new StringBuilder();
        if (sFileMana.nFolderType == safeFileManager.folderType.course)
        {
            string sql = "select couName,term,Name from Course,users where createUser=userId and couId=@couId";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@couId", sFileMana.getPathName());
            SqlDataReader rder = sqlHelper.getReader(sql, para);
            if (rder.Read())
            {
                path.Append(rder[0].ToString());
                path.Append("(" + rder[2].ToString() + ")");
                string term = rder[1].ToString();
                path.Append("," + Methods.analyseTerm(term));
                rootName = path.ToString();
                lblCurrentPath.Text = rootName;
            }
            else
                Methods.showMessageBox(Response, "数据库连接错误");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (sFileMana.isExistDirectory(e.CommandArgument.ToString()))
        {
            sFileMana.cd(e.CommandArgument.ToString());
            dataBind();
        }
        else
        {
            StringBuilder downPath=new StringBuilder();
  //          downPath.Append(sFileMana.getNPath());
            downPath.Append(e.CommandArgument.ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb = (LinkButton)e.Row.Cells[1].FindControl("LinkButton1");
            Label nametxt = (Label)e.Row.Cells[4].FindControl("NameText");
            string dstr = "..\\severFiles\\"+nametxt.Text.Substring(sFileMana.webRootFolder.Length, nametxt.Text.Length - sFileMana.webRootFolder.Length);
            nametxt.Text = string.Format("<a href=\"{0}\">下载</a>", dstr);
        }
    }
}