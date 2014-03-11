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
        stb.Append("select * from Course where couId = @f0");
        paras[0]=new SqlParameter("@f0",lis[0]);
        DataSet ds = new DataSet();
        for(int i = 1 ; i < lis.Length;i++)
        {
            paras[i] = new SqlParameter("@f" + i, lis[i]);
            string sqstr="union select * from Cours";
            stb.Append("")








            stb.Append("union");
            paras[0] = new SqlParameter("@f", focus);
             SqlDataAdapter da = sqlhp.getAdapter(sqlstr, paras);
             da.fill(ds);

             GridView1.DataSource = ds.Tables[0].DefaultView;
            sqlhp.close();
        }
        string SqlStr = "SELECT Cource.*,Teacher.* FROM Cource,Teacher where Cource.teaID=Teacher.teaID order by Teacher.teaID";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(connStr);
        try
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(SqlStr, conn);
            da.Fill(ds);

            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }
        finally
        {
            if (conn.State.ToString() == "Open")
                conn.Close();
        }
    }
}