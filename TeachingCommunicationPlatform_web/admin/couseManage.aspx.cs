using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class admin_couseManage : System.Web.UI.Page
{
    safeFileManager sf = new safeFileManager();
    SQLHelper sqlhp = new SQLHelper();
    Methods met;
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
        string id = GridView1.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值
        sf.SetRootPath("courses");
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        if (sf.deleteFolder(id))
        {
            Methods.showMessageBox(Response, "删除成功");
        }
        else
        {
            Methods.showMessageBox(Response, "删除失败");
        }
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

    }
    protected void newOneBtn_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        //newOneBtn.Visible = false;
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
        string cname = couNameTB.Text.Trim();
        string cterm = termTB.Text.Trim();
        string ctype = typeTB.Text.Trim();
        string cCreate = Session["ha_user"].ToString();
        string selstr = "select max(couId) from Course";
        string tid = tidTB.Text.Trim();
        SqlParameter[] paras = new SqlParameter[1];
        string ccouId=sqlhp.getAValue(selstr, paras);
        if(ccouId=="")
        { ccouId = "-1"; }
        ccouId = (Convert.ToInt32(ccouId) + 1).ToString();
        sqlhp.close();
        if (Methods.mkCou(ccouId, cname, ctype, "0", cterm, cCreate, tid))
        {
            Methods.showMessageBox(Response,"添加成功");
            Response.Redirect("\\admin/couseManage.aspx");
        }
        else
        {
            Methods.showMessageBox(Response, "添加失败");
        }

    }
    //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string courceID = GridView1.DataKeys[e.RowIndex].Values[0].ToString();   //取出编辑行的主键值 
    //    string courceName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtName")).Text;//取出修改后的值
    //    string courceTime = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTime")).Text;
    //    string courceAddress = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAddress")).Text;

    //    string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //    string SqlStr = "update Cource set CourceName='" + courceName + "',teaID='" + teaID + "',courceTime='" + courceTime + "',courceAddress='" + courceAddress + "' where courceID='" + courceID + "' and teaID='" + oldTeaID + "'";

    //    try
    //    {
    //        SqlConnection conn = new SqlConnection(connStr);  //创建连接对象
    //        if (conn.State.ToString() == "Closed")   //如果连接关闭,打开连接
    //            conn.Open();
    //        SqlCommand comm = new SqlCommand(SqlStr, conn);
    //        comm.ExecuteNonQuery();     //执行修改
    //        comm.Dispose();
    //        if (conn.State.ToString() == "Open")  //如果连接打开,关闭连接
    //            conn.Close();

    //        GridView1.EditIndex = -1;
    //        GridViewBind();
    //    }
    //}
}