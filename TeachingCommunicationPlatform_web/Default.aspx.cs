using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 //       safeFileManager sFM = new safeFileManager();
        //if(sFM.SetRootPath("D:\\programdesign\\git\\TeachingCommunicationPlatform\\TeachingCommunicationPlatform_web\\severFiles\\users\\ltf\\aa"))
        //    Response.Write("OK");
        //else Response.Write("WTF");
        //string haha="123456789aaa123456";
        //Response.Write(haha.IndexOf("aaa"));

        //sFM.setUser("00000", "11111");
        //Response.Write(sFM.nUserType.ToString());

        //SQLHelper sqlHelper=new SQLHelper();
        //string sql = "update Course set term=20131 where couId=1";
        //sqlHelper.ExecuteSql(sql, null);
        /*
        Session["ha_user"] = "00000";
        Session["ha_pwd"] = "11111";
        Session["ha_path"] = safeFileManager.getPath(safeFileManager.folderType.course, "1");
        Response.Redirect("publicFunction\\visitPath.aspx");
         * */
        safeFileManager sFM = new safeFileManager();
        sFM.setUser("00000", "11111");
        sFM.SetRootPath("courses");
        string[] txt=sFM.readFile("a.txt");
        for (int i = 0; i < txt.Length; i++)
            Response.Write(txt[i]);
  //      Response.Write(sFM.DeleteFile("a"));
    }
}