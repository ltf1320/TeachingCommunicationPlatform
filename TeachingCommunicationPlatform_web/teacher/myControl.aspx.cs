using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
public partial class teacher_myControl : System.Web.UI.Page
{
    SQLHelper sqlhp = new SQLHelper();
    safeFileManager sf = new safeFileManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void focusMoreBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("\\publicFunction/allCourse.aspx");
    }
    private void GridViewBind()
    {
        sf.setUser("00000","11111");
        sf.SetRootPath("users");
        sf.cd(Session["ha_user"].ToString());
        string [] lis = sf.readFile("listens");
        string focus;
        SqlParameter[] paras = new SqlParameter[lis.Length];
        StringBuilder stb =new StringBuilder();
        stb.Append("select couId as 课程编号,couName as 课程名称 , type as 属性 , stuNum as 关注人数 ,term as 学期 , createUser as 教师 from Course where couId = @f0 ");
        paras[0]=new SqlParameter("@f0",lis[0]);
        for(int i = 1 ; i < lis.Length;i++)
        {
            paras[i] = new SqlParameter("@f" + i, lis[i]);
            stb.Append(" union select * from Course where couId = @f"+i);
        }
            DataSet ds = new DataSet();
            SqlDataAdapter da = sqlhp.getAdapter(stb.ToString(), paras);
            sqlhp.close();
             da.Fill(ds);
             GridView1.DataSource = ds.Tables[0].DefaultView;
             GridView1.DataBind();

     
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView2.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值
        string delestr1 = "delete from manageCou where couId = @id";
        string delestr2 = "delete from Course  where couId = @id";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@id", id);
        sqlhp.ExecuteSql(delestr1, paras);
        paras[0] = new SqlParameter("@id", id);
        sqlhp.ExecuteSql(delestr2, paras);
        sqlhp.close();
        sf.SetRootPath("courses");
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        /////////////未dug
        sf.cd(id);
        string[] canFou = sf.readFile("listeners");
        for (int i = 0; i < canFou.Length; i++)
        {
            sf.SetRootPath("users");
            sf.cd(canFou[i]);
            sf.deleteStrFromFile("listens", id);

        }
        //////////////
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
    protected void newOneBtn_Click(object sender, EventArgs e)
    {
        Panel3.Visible = true;
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
        string ccouId = sqlhp.getAValue(selstr, paras);
        if (ccouId == "")
        { ccouId = "-1"; }
        ccouId = (Convert.ToInt32(ccouId) + 1).ToString();
        sqlhp.close();
        if (Methods.mkCou(ccouId, cname, ctype, "1", cterm, cCreate, tid, Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {

            Methods.showMessageBox(Response, "添加成功");
            Response.Redirect("\\admin/couseManage.aspx");
        }
        else
        {
            Methods.showMessageBox(Response, "添加失败");
        }
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        Panel3.Visible = false;
    }
}