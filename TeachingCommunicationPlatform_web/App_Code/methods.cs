using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
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
    
    public static bool isListened(string userId, string couId)
    {
        return false;
    }

    public static bool mkCou(string cid,string cname,string ctype ,string cstuNum,string cterm , string cCreate , string tid,string userId,string userPwd)
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
        sf.setUser(userId,userPwd);
        sf.SetRootPath("courses");
        sf.createFolder(cid);
        sf.cd(cid);
        sf.createFolder("tasks");
        sf.createFolder("data");
        sf.CreateFile("message");
        sf.CreateFile("listeners");

        return true;
    }

    public static bool mkUser(string uid, string urole,string uname,string upwd,string uemail,string uaca,string userId,string userPwd)
    {
        safeFileManager sf = new safeFileManager();
        SQLHelper sqlhp = new SQLHelper();
        DateTime DT = System.DateTime.Now; 
        string utime = System.DateTime.Now.ToString();
        string insertstr1 = "insert into users (userId,roleId,Name,pwd,email,createDate,academy) values(@uid,@urole,@uname,@upwd,@uemail,@utime,@uaca )";
        SqlParameter[] paras = new SqlParameter[7];
        paras[0] = new SqlParameter("@uid",uid );
        paras[1] = new SqlParameter("@urole", urole);
        paras[2] = new SqlParameter("@uname", uname);
        paras[3] = new SqlParameter("@upwd", upwd);
        paras[4] = new SqlParameter("@uemail", uemail);
        paras[5] = new SqlParameter("@utime", utime);
        paras[6] = new SqlParameter("@uaca", uaca);
        if (sqlhp.ExecuteSql(insertstr1, paras) == 0)
        {
            sqlhp.close();
            return false;
        }
        sqlhp.close();

        sf.setUser(userId, userPwd);
        sf.SetRootPath("users");
        sf.createFolder(uid);
        sf.cd(uid);
        sf.CreateFile("newThings");
        sf.CreateFile("listens");
        sf.CreateFile("propeties");
        sf.CreateFile("at");
        return true;
    }
}