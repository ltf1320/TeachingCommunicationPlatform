using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class publicFunction_visitCourse : System.Web.UI.Page
{
    static safeFileManager sFileMana=null;
    SQLHelper sqlHelper;
    string rootName;
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlHelper = new SQLHelper();
        if(sFileMana==null)
            sFileMana = new safeFileManager();
        if (!IsPostBack)
        {
            if (Session["ha_path"] == null)
            {
                Response.Redirect("allCourse.aspx");
                return;
            }
            sFileMana.SetRootPath(Session["ha_path"].ToString());
            try
            {
                if (Session["ha_user"] != null)
                {
                    sFileMana.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString());
                }
                dataBind();
            }
            catch (SqlException ex)
            {
                Methods.showMessageBox(Response, "数据库连接错误!" + ex.Message);
            }
            sqlHelper.close();
        }
    }
    protected void dataBind()
    {
        GridView1.DataSource = sFileMana.GetItems();
        GridView1.DataBind();
        StringBuilder path = new StringBuilder();
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
                path.Append("," + Methods.analyseTerm(term)+'\\');
                rootName = path.ToString();
                lblCurrentPath.Text = rootName+sFileMana.npath;
                if (sFileMana.npath != "")
                    returnBtn.Visible = true;
                else returnBtn.Visible = false;
            }
            else
                Methods.showMessageBox(Response, "数据库连接错误");
        }
        sqlHelper.close();
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
            StringBuilder downPath = new StringBuilder();
            string fileName = e.CommandArgument.ToString();
            FileStream fs = sFileMana.getFileStream(fileName);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileSystemItem item=(FileSystemItem)e.Row.DataItem;
            if (item.IsFolder)
            {
                LinkButton lk = (LinkButton)e.Row.FindControl("LinkButton1");
                lk.Text = "[" + lk.Text + "]";
            }
        }
    }
     protected void BackSpaceFolder(object sender, EventArgs e)
    {
        sFileMana.returnBackSpaceFolderPath();
        dataBind();
    }
}