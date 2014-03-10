using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
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
    /// <param name="deadLine">截止时间</param>
    /// <param name="text">文本</param>
    /// <param name="fileList">文件列表</param>
    /// <param name="userList">@user列表（如果是task则自动@所有关注的人）</param>
    /// <returns>成功返回消息编号，否则返回-1</returns>
    public static int addNewThing(string couId, bool type, string topic, DateTime date, DateTime deadLine, string text, string[] fileList, string[] @userList)
    {
        return -1;
    }
    public static void delNewThing(string couId, int id)
    {
        
    }
    public static bool isListened(string userId, string couId);
}