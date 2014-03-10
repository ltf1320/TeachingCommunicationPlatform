using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_couseManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;  //GridView编辑项索引等于单击行的索引
        GridView1.DataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SQLHelper sqlhp = new SQLHelper();
        string id = GridView1.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值
        //string teaID = GridView1.DataKeys[e.RowIndex].Values[1].ToString();
        string deleStr1 = "DELETE FROM Course WHERE couId=@id";
        string deleStr2 = "DELETE FROM manageCou WHERE couId=@id";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@id", id);
        paras[1] = null;
        sqlhp.ExecuteSql(deleStr2, paras);
        paras[0] = new SqlParameter("@id", id);
        paras[1] = null;
        sqlhp.ExecuteSql(deleStr1, paras);
        sqlhp.close();
        GridView1.DataBind();

        safeFileManager sf = new safeFileManager();
        sf.SetRootPath("courses");
        sf.setUser(Session["ha_user"].ToString(),Session["ha_pwd"].ToString());
        if(sf.deleteFolder(id))
        {
            Methods.showMessageBox(Response, "删除成功");
        }
        else
        {
            Methods.showMessageBox(Response, "删除失败");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}