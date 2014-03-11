using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
/// <summary>
/// methods 的摘要说明
/// </summary>
public class Methods
{
	private Methods()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string analyseTerm(string term)
    {
        int year = Convert.ToInt32(term.Substring(0, 4));
        StringBuilder res = new StringBuilder();
        switch (term[4])
        {
            case '0':
                res.Append(year - 1);
                res.Append('-');
                res.Append(year);
                res.Append("第二学期");
                break;
            case '1':
                res.Append(year);
                res.Append('-');
                res.Append(year + 1);
                res.Append("第一学期");
                break;
        }
        res.Append("\\");
        return res.ToString();
    }
    public static void showMessageBox(HttpResponse response,string message)
    {
        response.Write("<Script>alert('"+message+"');</Script>");
    }
 //   public static int jsp_return;
    /*
    public static void showComfirmBox(HttpResponse response,string valueName,string message)
    {
        response.Write("<Script language='javascript'>if(confirm('" + message + "'))<%=" + valueName + "%>=true;else <%=" + valueName + "%>=false;</script>");
    }
     * */
    public static void listen(string userId, string couId)
    {

    }
    public static void cancelListen(string userId, string couId)
    {

    }
    /// <summary>
    /// 添加新消息
    /// </summary>
    /// <param name="couId">课程号</param>
    /// <param name="type">0:message,1:task</param>
    /// <param name="topic">标题</param>
    /// <param name="date">时间</param>
    /// <param name="deadLine">截止时间(task only)</param>
    /// <param name="text">文本</param>
    /// <param name="fileList">文件列表</param>
    /// <param name="userList">@user列表（如果是task则自动@所有关注的人）</param>
    /// <returns>成功返回消息编号，否则返回-1</returns>
    private static object newThingLock;
    public static int addNewThing(string couId, bool type, string topic, DateTime date, DateTime deadLine, string text, string[] fileList, string[] @userList)
    {
        CMessage msg = new CMessage();
        lock (newThingLock)
        {
            if (msg.createMsg(couId, type, topic, date, deadLine, text, fileList, userList) == -1)
                return -1;
            if (!msg.writeThisMsg())
                return -1;
            return -1;
        }
    }
    public static bool delNewThing(string couId, int id)
    {
        lock (newThingLock)
        {
            return CMessage.deleteMsg(couId, id);
        }
    }
    public static bool isListened(string userId, string couId)
    {
        return false;
    }

    public static bool mkCou(string cid,string cname,string ctype ,string cstuNum,string cterm , string cCreate , string tid)
    {
        safeFileManager sf = new safeFileManager();
        SQLHelper sqlhp = new SQLHelper();
        string insertstr1 = "insert into Course (couId,couName,type,stuNum,term,createUser) values(@cid,@cname,@ctype,@cstuNum,@cterm,@tid )";
        string insertstr2 = "insert into manageCou(userId,couId) values (@tid,@cid)";
        SqlParameter[] paras = new SqlParameter[7];
        paras[0] = new SqlParameter("@cid", cid);
        paras[1] = new SqlParameter("@cname", cname);
        paras[2] = new SqlParameter("@ctype", ctype);
        paras[3] = new SqlParameter("@cstuNum", cstuNum);
        paras[4] = new SqlParameter("@cterm", cterm);
        paras[5] = new SqlParameter("@tid", tid);
        if(sqlhp.ExecuteSql(insertstr1, paras)==0)
        { 
            sqlhp.close();
            return false; 
        }
        SqlParameter[] paras2 = new SqlParameter[2];
        paras2[0] = new SqlParameter("@tid", tid);
        paras2[1] = new SqlParameter("@cid", cid);
        if (sqlhp.ExecuteSql(insertstr2, paras2) == 0)
        {
             sqlhp.close();
            return false ;
        }
        sqlhp.close();
        sf.SetRootPath("courses");
        sf.createFolder(cid);
        sf.cd(cid);
        sf.createFolder("tasks");
        sf.createFolder("data");
        sf.CreateFile("message");
        sf.CreateFile("listeners");
        return true;
    }

}