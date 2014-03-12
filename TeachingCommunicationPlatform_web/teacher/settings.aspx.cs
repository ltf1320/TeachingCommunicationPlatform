using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class teacher_myThings : System.Web.UI.Page
{

    SQLHelper sqlhp;
    string sid;
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlhp = new SQLHelper();
        sid = Session["ha_user"].ToString();
        if (!IsPostBack)
        {
            defaultPannel.Visible = true;
            pwdPannel.Visible = false;
            string selstr = "select * from users where userId=@ha_user";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@ha_user", sid);
            try
            {
                SqlDataReader rder = sqlhp.getReader(selstr, paras);
                rder.Read();
                IdContentLbl.Text = rder[0].ToString();
                nametxt.Text = rder[2].ToString();

                if (rder[1].ToString() == "2")
                    roleContentLbl.Text = "教师";
                else if (rder[1].ToString() == "3")
                    roleContentLbl.Text = "学生";
                else
                    roleContentLbl.Text = "管理员";
                postAdd.Text = rder[4].ToString();
                crtTimeContentLbl.Text = rder[5].ToString();

                string sql = "select acName from academy where acId=@acId";
                paras[0] = new SqlParameter("@acId", rder[6].ToString());
                rder.Close();
                acContentLbl.Text = sqlhp.getAValue(sql,paras);
            }
            catch (SqlException exception)
            {
                Response.Write("<Script>alert('数据库连接错误');</Script>");
            }
            sqlhp.close();
        }
    }
    protected void onChangePwd(object sender, EventArgs e)
    {
        defaultPannel.Visible = false;
        pwdPannel.Visible = true;
    }
    protected void updateData(object sender, EventArgs e)
    {
        string upstr = "update users set Name=@NName,email=@eemail where userId=@sid";
        SqlParameter[] paras = new SqlParameter[3];
        paras[0] = new SqlParameter("@sid", sid);
        paras[1] = new SqlParameter("@NName", nametxt.Text.Trim());
        paras[2] = new SqlParameter("@eemail", postAdd.Text.Trim());
        try
        {
            sqlhp.ExecuteSql(upstr, paras);
            Response.Write("<script>alert('修改成功');</script>");
        }
        catch (SqlException exception)
        {
            Response.Write("<Script>alert('数据库连接错误');</Script>");
        }
        finally
        {
        }
        sqlhp.close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string oripwd = oripwdtxt.Text.Trim();
        string selstr = "select pwd from users where userId=@sid";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@sid", sid);
        paras[1] = null;//me
        string nowpwd = sqlhp.getAValue(selstr, paras);
        if (oripwd != nowpwd)
            Response.Write("<script>alert('原始密码错误!');</script>");
        else
        {
            string upstr = "update users set pwd=@newpwd where userId=@sid";
            paras[0] = new SqlParameter("@sid", sid);
            paras[1] = new SqlParameter("@newpwd", newpwdtxt.Text.Trim());
            try
            {
                sqlhp.ExecuteSql(upstr, paras);
                Response.Write("<Script>alert('修改成功');</Script>");
                Response.Redirect(Request.Url.ToString());
            }
            catch (SqlException exception)
            {
                Response.Write("<Script>alert('数据库连接错误');</Script>");
            }
        }
        sqlhp.close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        defaultPannel.Visible = true;
        pwdPannel.Visible = false;
    }
}