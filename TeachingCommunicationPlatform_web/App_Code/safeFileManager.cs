using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// 用户类别
/// </summary>
public enum userType
{
    admin,
    student,
    teacher,
    visitor
}

//具有用户权利检测功能的文件管理
public class safeFileManager : fileManager
{
    string userName,pathName; //现在目录的名字
    SQLHelper sqlHelper;   
    string webRootFolder;  //web的根目录
    userType nUserType;    //现在用户类别
    /// <summary>
    /// 目录类别
    /// </summary>
    public enum folderType
    {
        /// <summary>
        /// 用户配置文件夹
        /// </summary>
        userConfig, 
        /// <summary>
        /// 课程资源文件夹
        /// </summary>
        course,
        /// <summary>
        /// 用户文件夹
        /// </summary>
        users,
        /// <summary>
        /// 课程文件夹
        /// </summary>
        courses,
        /// <summary>
        /// 非法访问
        /// </summary>
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
        
        if (ffolder == "users")
        {
            if(name.Length==0)
                return folderType.users;
            else 
                return safeFileManager.folderType.userConfig;
        }
        if (ffolder == "cours")
        {
            if(name.Length==0)
                return folderType.courses;
            else 
                return safeFileManager.folderType.course;
        }
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

    /// <summary>
    /// 设置用户
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="pwd">密码</param>
    /// <returns>成功返回true</returns>
    public bool setUser(string userName,string pwd)
    {
        string sql = "select count(*) from user where userName=@userName and pwd=@pwd";
        SqlParameter[] para = new SqlParameter[2];
        para[0] = new SqlParameter("@userName", userName);
        para[1] = new SqlParameter("@pwd", pwd);
        string res = sqlHelper.getAValue(sql, para);
        if (res == "0")
            return false;
        sql = "select rouId from user where userName=@userName";
        para[0] = new SqlParameter("@userName", userName);
        para[1] = null;
        res = sqlHelper.getAValue(sql, para);
        int type=Convert.ToInt32(res);
        switch(type)
        {
            case 1:
            nUserType = userType.admin;
                break;
            case 2:
            nUserType = userType.teacher;
                break;
            case 3:
            nUserType = userType.student;
                break;
            default:
                nUserType = userType.visitor;
                break;
        }
        return true;
    }
    new public List<FileSystemItem> GetItems()
    {
        if (nFolderType != folderType.invalid)
            return base.GetItems();
        return null;
    }

    public bool createFolder(string name)
    {
        if (nUserType == userType.admin)
        {
            base.CreateFolder(name, strRootFolder);
            return true;
        }
        return false;
    }
}
