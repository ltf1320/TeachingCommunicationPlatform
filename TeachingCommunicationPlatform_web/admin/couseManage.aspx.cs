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
        if (Methods.mkCou(ccouId, cname, ctype, "1", cterm, cCreate, tid,Session["ha_user"].ToString(),Session["ha_pwd"].ToString()))
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
    //    string cid = GridView1.DataKeys[e.RowIndex].Values[0].ToString();   //取出编辑行的主键值 
    //    string ctype = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("typeTB")).Text;//取出修改后的值
    //    string cterm = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("termTB")).Text;

    //    string upStr = "update Course set type=@ctype , term=@cterm where couId=@cid";
    //    SqlParameter[] paras = new SqlParameter[3];
    //    paras[2] = new SqlParameter("@cid", cid);
    //    paras[0] = new SqlParameter("@ctype", ctype);
    //    paras[1] = new SqlParameter("@cterm", cterm);
    //    sqlhp.ExecuteSql(upStr, paras);
    //    sqlhp.close();
    //    GridView1.DataBind();

    //}

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值
        sf.SetRootPath("courses");
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        //未dug
        sf.cd(id);
        string[] canFou = sf.readFile("listeners");
        for (int i = 0; i < canFou.Length;i++ )
        {
            sf.SetRootPath("users");
            sf.cd(canFou[i]);
            sf.deleteStrFromFile("listens", id);

        }
        sf.SetRootPath("courses");
            if (sf.deleteFolder(id))
            {
                GridView1.DataBind();
                Methods.showMessageBox(Response, "删除成功");
            }
            else
            {
                Methods.showMessageBox(Response, "删除失败");
            }
    }
}