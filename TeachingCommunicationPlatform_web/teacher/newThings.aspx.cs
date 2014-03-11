using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text;

public partial class teacher_newThings : System.Web.UI.Page
{
    safeFileManager sFM;
    string user;
    List<CMessage> msgList;
    SQLHelper sqlHelper;
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlHelper = new SQLHelper();
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
        //DataList1.DataSource = msgList;
        //DataList1.DataBind();
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            CMessage msg=(CMessage)e.Item.DataItem;
            //课程名称
            Label Label_cou = (Label)e.Item.FindControl("Label_cou");
            string sql = "select couName,term,createUser from Course where couId=@couId";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@couId", msg.couId);
            SqlDataReader rder = sqlHelper.getReader(sql, para);
            rder.Read();
            StringBuilder couName = new StringBuilder();
            couName.Append(rder[0].ToString());
            couName.Append("(" + rder[2].ToString() + ")");
            string term = rder[1].ToString();
            couName.Append("," + Methods.analyseTerm(term));
            Label_cou.Text = couName.ToString();

            //时间
            Label Label_date = (Label)e.Item.FindControl("Label_date");
            Label_date.Text = msg.date.ToString();


        }
        catch(Exception ex)
        {
            Methods.showMessageBox(Response, "数据库连接错误");
        }
        finally { sqlHelper.close(); }
    }
}