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
 //       CMessage msg=new CMessage();
        int id=CMessage.addNewThing("0", false, "bool",new DateTime(2014, 3, 11, 21, 27, 10),new DateTime(2014,3,11) , "yiikou", null, null);
        Response.Write(id);
        List<CMessage> msglist = CMessage.getMsgs("0");
//                Response.Write(CMessage.isMsgExist("0",id));
//        Response.Write(CMessage.deleteMsg("0", 3));
//        msg.getMsgInfo("0", id);
//        Response.Write(msg.topic);
//        Response.Write(msg.text);
    }
}