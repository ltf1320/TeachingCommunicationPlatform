using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;

public partial class admin_userManage : System.Web.UI.Page
{
    safeFileManager sf = new safeFileManager();
    SQLHelper sqlhp = new SQLHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

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
        string uname = nameTb.Text.Trim();
        string urole = roleTb.Text.Trim();
        string upwd = pwdTb.Text.Trim();
        string uemail = emailTb.Text.Trim();
        string uaca = acaTb.Text.Trim();
        string selstr = "select max(userId) from users";
        SqlParameter[] paras = new SqlParameter[1];
        string uuserId = sqlhp.getAValue(selstr, paras);
        if (uuserId == "")
        { uuserId = "-1"; }
        uuserId = (Convert.ToInt32(uuserId) + 1).ToString();
        sqlhp.close();
        if (Methods.mkUser(uuserId, urole, uname, upwd, uemail, uaca, Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "添加成功");
            Response.Redirect("\\admin/userManage.aspx");
        }
        else
        {
            Methods.showMessageBox(Response, "添加失败");
        }
    }
    protected void newOneBtn_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        //newOneBtn.Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值
        sf.SetRootPath("users");
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }


        ////////////////////////未dug
        sf.cd(id);
        string[] canFou = sf.readFile("listens");
        for (int i = 0; i < canFou.Length; i++)
        {
            sf.SetRootPath("courses");
            sf.cd(canFou[i]);
            sf.deleteStrFromFile("listeners", id);

        }
        string upstr = "update Course set stuNum =stuNum -@stu where couId = @id";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@stu", canFou.Length);
        paras[1] = new SqlParameter("@id", id);
        sqlhp.ExecuteSql(upstr, paras);
        sqlhp.close();
        //////////////////////
        sf.returnBackSpaceFolderPath();
        if (sf.deleteFolder(id))
        {
            string constr = "select couid from Course where createUser=@id";
            sf.SetRootPath("courses");
            SqlParameter[] paras2 = new SqlParameter[1];
            paras2[0] = new SqlParameter("@id",id);
            SqlDataReader rder = sqlhp.getReader(constr, paras2);
            while(rder.Read())
                sf.deleteFolder(rder[0].ToString());
            //删除人的时候
            GridView1.DataBind();
            Methods.showMessageBox(Response, "删除成功");
            sqlhp.close();
        }
        else
        {
            Methods.showMessageBox(Response, "删除失败");
        }
    }
}