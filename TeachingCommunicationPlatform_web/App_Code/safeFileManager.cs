using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;

//用户类别
public enum userType
{
    admin,
    user,
    visitor
}

//具有用户权利检测功能的文件管理
public class safeFileManager : fileManager
{
    string userName,pathName; //现在目录的名字
    SQLHelper sqlHelper;   
    string webRootFolder;  //web的根目录
    userType nUserType;    //现在用户类别
    public enum folderType
    {
        userConfig,
        course,
        invalid
    }
    folderType nFolderType;/// 现在目录类别

    private folderType getPathInfo(string path, out string name)
    {
        int index = path.IndexOf("severFiles\\");
        index += 11;
        string ffolder = path.Substring(index, 5);
        string substr = path.Substring(index);
        name = "";
        int tt = substr.IndexOf('\\');
        if (tt == path.Length)
            return folderType.invalid;
        substr = substr.Substring(tt);
        tt = substr.IndexOf('\\');
        if (tt != path.Length)
            tt--;
        name = path.Substring(index, tt);
        string root = path.Substring(0, index);
        if (root != webRootFolder)
            return folderType.invalid;
        if (name.Length == 0)
            return safeFileManager.folderType.invalid;
        if (ffolder == "users")
            return safeFileManager.folderType.userConfig;
        if (ffolder == "cours")
            return safeFileManager.folderType.userConfig;
        return safeFileManager.folderType.invalid;
    }
    public safeFileManager()
    {
        sqlHelper = new SQLHelper();
        webRootFolder = HttpContext.Current.Request.PhysicalApplicationPath + "severFiles\\";
        nFolderType = safeFileManager.folderType.invalid;
        nUserType = userType.visitor; 
    }
    /// <summary>
    /// 写操作根目录
    /// 会把目录的类别和名字记录下来
    /// </summary>
    /// <param name="path">目录</param>
    /// <returns>成功返回true</returns>
    new public bool SetRootPath(string path)
    {
        string name;
        folderType fdType;
        fdType = getPathInfo(path, out name);
        if (fdType == safeFileManager.folderType.invalid)
            return false;
        nFolderType = fdType;
        strRootFolder = path;
        pathName = name;
        return true;
    }
    /// <summary>
    /// 得到操作目录
    /// </summary>
    /// <param name="type">目录类别</param>
    /// <param name="name">目录名</param>
    /// <returns>目录</returns>
    public string getPath(folderType type,string name)
    {
        StringBuilder str=new StringBuilder();
        str.Append(webRootFolder);
        switch(type)
        {
            case folderType.course:
                str.Append("course\\");
                str.Append(name);
                break;
            case folderType.userConfig:
                str.Append("users\\");
                str.Append(name);
                break;
            default:
                break;
        }
        return str.ToString();
    }

    public bool setUser(string userName,string pwd)
    {
        string sql = "select count(*) from user where userName=@userName and pwd=@pwd";
        SqlParameter[] para = new SqlParameter[2];
        para[0] = new SqlParameter("@userName", userName);
        para[1] = new SqlParameter("@pwd", pwd);
        string res = sqlHelper.getAValue(sql, para);
        if (res == "0")
            return false;
        sql = "select count(*) from userInRole where userName=@userName and roleId=1";
        para[0] = new SqlParameter("@userName", userName);
        para[1] = null;
        res = sqlHelper.getAValue(sql, para);
        if (res == "1")
            nUserType = userType.admin;
    }
}
