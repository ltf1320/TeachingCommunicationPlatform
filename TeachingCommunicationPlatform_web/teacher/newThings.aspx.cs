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
        rder.Close();
        sqlHelper.close();
        dataBind();
    }
    protected void dataBind()
    {
        DataList1.DataSource = msgList;
        DataList1.DataBind();
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        SqlDataReader rder=null;
        try
        {
            CMessage msg=(CMessage)e.Item.DataItem;
            //课程名称
            Label Label_cou = (Label)e.Item.FindControl("Label_cou");
            string sql = "select couName,term,createUser from Course where couId=@couId";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@couId", msg.couId);
            rder = sqlHelper.getReader(sql, para);
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

            //作业
            Label Label_task = (Label)e.Item.FindControl("Label_task");
            if(msg.type)
            {
                Label_task.Text="（作业:完成时间:"+msg.deadLine.ToString()+"）";
            }
            else Label_task.Visible=false;

            //文件
            DataList dataList=(DataList)e.Item.FindControl("DataList_file");
            if(msg.fileList==null || msg.fileList.Length==0)
            {
                dataList.Visible = false;
            }
            else
            {
                dataList.DataSource = msg.fileList;
                dataList.DataBind();
            }

            rder.Close();
            //at
            DataList dataList_at = (DataList)e.Item.FindControl("DataList_at");
            if(msg.atList==null || msg.atList.Length==0)
            {
                dataList_at.Visible = false;
            }
            else
            {
                dataList_at.DataSource = msg.atList;
                dataList_at.DataBind();
            }

        }
        catch(Exception ex)
        {
            Methods.showMessageBox(Response, "数据库连接错误");
        }
        finally { if(rder!=null) rder.Close(); sqlHelper.close(); }
    }
    protected void DataList1_ItemDataBound1(object sender, DataListItemEventArgs e)
    {
        try
        {
            string filePath = (string)e.Item.DataItem;

            LinkButton fileLabel =(LinkButton) e.Item.FindControl("downBtn");
            fileLabel.CommandArgument = filePath;
        }
        catch(Exception ex)
        {
            Methods.showMessageBox(Response, "文件设置失败");
        }
    }
    protected void DataList_at_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label label = (Label)e.Item.FindControl("Label_atName");
            string userId = (string)e.Item.DataItem;

            string sql = "select Name from users where userId=@userId";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@userId", userId);
            label.Text = "@" + sqlHelper.getAValue(sql, para);
        }
        catch(Exception ex)
        {
            Methods.showMessageBox(Response, "数据库连接错误!");
        }
    }
    protected void DataList_file_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName=="downLoad")
        {
            Response.ContentType = "application/octet-stream";
            safeFileManager sFileMana = new safeFileManager();
            sFileMana.SetRootPath("");
            string fileName = e.CommandArgument.ToString();
            FileStream fs = sFileMana.getFileStream(fileName);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            //通知浏览器下载文件而不是打开 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}