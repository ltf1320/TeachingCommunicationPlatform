using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
  //      List<CMessage> msgList = CMessage.getMsgs("0");
        safeFileManager sFM = new safeFileManager();
        sFM.SetRootPath("courses");
        sFM.setUser("00000", "11111");
        Response.Write(sFM.deleteStrFromFile("a.txt", "ltf"));
    }
}