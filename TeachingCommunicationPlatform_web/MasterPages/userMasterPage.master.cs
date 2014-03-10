using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class MasterPages_userMasterPage : System.Web.UI.MasterPage
{
    SQLHelper sqlhp;
    string sid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //sqlhp = new SQLHelper();
        sid = Session["ha_user"].ToString();
        if (!IsPostBack)
        {
            //string selstr = "select * from users where userId=@ha_user";
            //SqlParameter[] paras = new SqlParameter[2];
            //paras[0] = new SqlParameter("@ha_user", sid);
            string rroleId = null;
            safeFileManager sfRole = new safeFileManager();
            rroleId = sfRole.getUserRole(sid);
            //Response.Write("111111111////");
            try
            {
                //SqlDataReader rder = sqlhp.getReader(selstr, paras);
                //rder.Read();
                //rroleId = rder[1].ToString();
                //sqlhp.close();
                //Response.Write(rroleId);
                if(rroleId=="2")
                {
                    //Response.Write("2222");
                    Menu1.Items[0].NavigateUrl = "../teacher/newThings.aspx";
                    Menu1.Items[1].NavigateUrl = "../teacher/@Me.aspx";
                    Menu1.Items[2].NavigateUrl = "../teacher/settings.aspx";
                    Menu1.Items[3].NavigateUrl = "../teacher/myControl.aspx";
                }
                if (rroleId == "3")
                {
                    Response.Write("2222");
                    Menu1.Items[0].NavigateUrl = "../student/newThings.aspx";
                    Menu1.Items[1].NavigateUrl = "../student/@Me.aspx";
                    Menu1.Items[2].NavigateUrl = "../student/settings.aspx";
                    Menu1.Items[3].NavigateUrl = "../student/myControl.aspx";
                }
            }
            catch (SqlException exception)
            {
                Response.Write("<Script>alert('数据库连接错误');</Script>");
            }
        }
    }
}
