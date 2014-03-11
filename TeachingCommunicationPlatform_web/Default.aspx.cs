using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
  //      List<CMessage> msgList = CMessage.getMsgs("0");
        safeFileManager sFM = new safeFileManager();
        SQLHelper sqlHelper=new SQLHelper();
        string sql = "insert into Course values(0,@couName,@couType,0,'20140','1')";
        SqlParameter[] para = new SqlParameter[2];
        para[0] = new SqlParameter("@couName", "操作系统");
        para[1] = new SqlParameter("@couType", "必修");
        Response.Write(sqlHelper.ExecuteSql(sql, para));
    }
}