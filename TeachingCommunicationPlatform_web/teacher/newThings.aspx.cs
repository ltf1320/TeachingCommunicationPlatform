using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class teacher_newThings : System.Web.UI.Page
{
    safeFileManager sFM;
    string user;
    List<CMessage> msgList;
    protected void Page_Load(object sender, EventArgs e)
    {
        sFM = new safeFileManager();
        if (Session["ha_user"] == null || Session["ha_pwd"] == null)
            Response.Redirect("publicFunction\allCourse.aspx");
        user=Session["ha_user"].ToString();
        sFM.setUser(user, Session["ha_pwd"].ToString());
        sFM.SetRootPath("users\\" + user);
        StreamReader rder= sFM.getStreamReader("newThings");
        msgList=new List<CMessage>();
        CMessage msg;
        string couId; int msgId;
        while(!rder.EndOfStream)
        {
            couId = rder.ReadLine();
            msgId = Convert.ToInt32(rder.ReadLine());
            msg = new CMessage();
            msg.getMsgInfo(couId, msgId);
            if (msg.hasMsg)
                msgList.Add(msg);
        }
        dataBind();
    }
    protected void dataBind()
    {
        DataList1.DataSource = msgList;
        DataList1.DataBind();
    }
    protected void DataList1_DataBinding(object sender, EventArgs e)
    {

    }
}