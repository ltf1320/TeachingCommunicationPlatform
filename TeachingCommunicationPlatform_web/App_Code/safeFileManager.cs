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

//具有用户权力检测功能的文件管理
public class safeFileManager : fileManager
{
    string userName,pathName; //现在目录的名字
    SQLHelper sqlHelper;   
    public string webRootFolder;  //web的根目录
    public userType nUserType;    //现在用户类别
    public string npath;
    public string getPathName()
    {
        return pathName;
    }
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
    public folderType nFolderType;/// 现在目录类别

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
        name = substr.Substring(tt+1);
        tt = name.IndexOf('\\');
        if (tt != -1)
        {
            name = name.Substring(0, tt);
        }
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
        npath = "";
    }

    public new bool isExistDirectory(string folder)
    {
        StringBuilder path = new StringBuilder();
        path.Append(getNPath());
        path.Append(folder);
        return base.isExistDirectory(path.ToString());
    }

    /// <summary>
    /// 写操作相对根目录
    /// 会把目录的类别和名字记录下来
    /// </summary>
    /// <param name="path">目录</param>
    /// <returns>成功返回true</returns>
    new public bool SetRootPath(string path)
    {
        StringBuilder pp = new StringBuilder();
        pp.Append(webRootFolder);
        pp.Append(path);
        string name;
        folderType fdType;
        fdType = getPathInfo(pp.ToString(), out name);
        if (fdType == safeFileManager.folderType.invalid)
            return false;
        nFolderType = fdType;
        strRootFolder = path;
        if (strRootFolder[strRootFolder.Length - 1] != '\\')
            strRootFolder = strRootFolder + '\\';
        pathName = name;
        npath = "";
        return true;
    }
    /// <summary>
    /// 得到操作目录
    /// </summary>
    /// <param name="type">目录类别</param>
    /// <param name="name">目录名</param>
    /// <returns>目录</returns>
    public static string getPath(folderType type,string name)
    {
        StringBuilder str=new StringBuilder();
        switch(type)
        {
            case folderType.course:
                str.Append("courses\\");
                str.Append(name);
                str.Append("\\data\\");
                break;
            case folderType.userConfig:
                str.Append("users\\");
                str.Append(name);
                str.Append("\\");
                break;
            default:
                break;
        }
        return str.ToString();
    }
    /// <summary>
    /// 得到当前绝对操作目录
    /// </summary>
    /// <returns>目录</returns>
    public string getNPath()
    {
        StringBuilder path = new StringBuilder();
        path.Append(webRootFolder);
        path.Append(strRootFolder);
        path.Append(npath);
        return path.ToString();
    }

    /// <summary>
    /// 得到当前相对操作目录
    /// </summary>
    /// <returns>目录</returns>
    public string getRelativeNPath()
    {
        StringBuilder path = new StringBuilder();
        path.Append(strRootFolder);
        path.Append(npath);
        return path.ToString();
    }


    /// <summary>
    /// 得到当前绝对根目录
    /// </summary>
    /// <returns></returns>
    public string getRootPath()
    {
        return webRootFolder+strRootFolder;
    }
    /// <summary>
    /// 得到当前相对根目录
    /// </summary>
    /// <returns></returns>
    public string getRelativeRootPath()
    {
        return strRootFolder;
    }
    /// <summary>
    /// 返回上一级目录
    /// </summary>
    /// <returns>如果已经是根目录了，则返回根目录</returns>
    public void returnBackSpaceFolderPath()
    {
        if (npath == "")
            return ;
        int index=npath.LastIndexOf('\\', 0, npath.Length - 1);
        npath=npath.Substring(0, index);
        return;
    }

    /// <summary>
    /// 访问目录
    /// </summary>
    /// <param name="folder"></param>
    public void cd(string folder)
    {
        StringBuilder path = new StringBuilder();
        path.Append(npath);
        path.Append(folder);
        if (path[path.Length - 1] != '\\')
            path.Append('\\');
        npath = path.ToString();
    }

    /// <summary>
    /// 设置用户
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="pwd">密码</param>
    /// <returns>成功返回true</returns>
    public bool setUser(string userId,string pwd)
    {
        string sql = "select count(*) from users where userId=@userId and pwd=@pwd";
        SqlParameter[] para = new SqlParameter[2];
        para[0] = new SqlParameter("@userId", userId);
        para[1] = new SqlParameter("@pwd", pwd);
        string res = sqlHelper.getAValue(sql, para);
        if (res == "0")
            return false;
        sql = "select roleId from users where userId=@userId";
        para[0] = new SqlParameter("@userId", userId);
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
        sqlHelper.close();
        return true;
    }
    new public List<FileSystemItem> GetItems()
    {
        if (nFolderType != folderType.invalid)
            return base.GetItems(getNPath());
        return null;
    }

    public bool isUserCanEditFile()
    {
        if (nUserType == userType.admin)
        {
            return true;
        }
        if (nUserType == userType.visitor)
            return false;
        switch (nFolderType)
        {
            case folderType.course:
                if (isUserManageCourse(userName, pathName))
                    return true;
                break;
            case folderType.courses:
                if (nUserType == userType.teacher)
                    return true;
                break;
            default: break;
        }
        return false;
    }

    public bool createFolder(string name)
    {
        if (isUserCanEditFolder(name))
        {
            base.CreateFolder(name, webRootFolder+strRootFolder+npath);
            return true;
        }
        return false;
    }
    private bool isUserManageCourse(string userName,string couId)
    {
        string sql = "select count(*) from manageCou where userName=@userName and couId=@couId";
        SqlParameter[] para = new SqlParameter[2];
        para[0] = new SqlParameter("@userName", userName);
        para[1] = new SqlParameter("@couId", couId);
        string res = sqlHelper.getAValue(sql, para);
        int value = Convert.ToInt32(res);
        if (value > 0)
        {
            return true;
        }
        sqlHelper.close();
        return false;
    }
    public bool isUserCanEditFolder(string folderName)
    {
        if (nUserType == userType.admin)
        {
            return true;
        }
        if (nUserType == userType.teacher)
        {
            if (nFolderType != folderType.users && nFolderType != folderType.invalid)
            {
                return true;
            }
            if(nFolderType==folderType.courses&& isUserManageCourse(userName,folderName))
            {
                return true;
            }
            return false;
        }
        if (nUserType == userType.student)
        {
            if (nFolderType == folderType.course)
            {
                if (isUserManageCourse(userName, pathName))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    public bool CreateFile(string filename)
    {
        if(isUserCanEditFile())
        {
            return base.CreateFile(filename, webRootFolder + strRootFolder + npath);
        }
        return false;
    }

    new public bool DeleteFile(string fileName)
    {
        StringBuilder path = new StringBuilder();
        path.Append(webRootFolder);
        path.Append(strRootFolder);
        path.Append(fileName);
        if (isUserCanEditFile())
        {
            return base.DeleteFile(path.ToString());
        }
        return false;
    }
    public bool deleteFolder(string folderName)
    {
        StringBuilder fd = new StringBuilder();
        fd.Append(webRootFolder);
        fd.Append(strRootFolder);
        fd.Append(folderName);
        if (isUserCanEditFolder(folderName))
        {
            return base.DeleteFolder(fd.ToString());
        }
        return false;
    }
    /*
    /// <summary>
    /// 写入一个新文件，在文件中写入内容，然后关闭文件。如果目标文件已存在，则改写该文件。
    /// </summary>
    /// <param name="parentName"></param>
    /// <param name="contents"></param>
    /// <returns></returns>
    new public bool WriteAllText(string fileName, string contents)
    {
        StringBuilder path=new StringBuilder();
        path.Append(webRootFolder);
        path.Append(strRootFolder);
        path.Append(fileName);
        if(isUserCanEditFile())
        {
            return base.WriteAllText(path.ToString(), contents);
        }
        return false;
    }
    */
    public new bool AppendLineToFile(string fileName, string text)
    {
        StringBuilder path = new StringBuilder();
        path.Append(webRootFolder);
        path.Append(strRootFolder);
        path.Append(fileName);
        if (isUserCanEditFile())
        {
            return base.AppendLineToFile(path.ToString(), text);
        }
        return false;
        
    }
    /// <summary>
    /// 按行读文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>错误返回null</returns>
    public new string[] readFile(string fileName)
    {
        StringBuilder path = new StringBuilder();
        path.Append(webRootFolder);
        path.Append(strRootFolder);
        path.Append(fileName);
        return base.readFile(path.ToString());
    }
    public string getUserRole(string sid)
    {
        SQLHelper sqlhp;
        sqlhp = new SQLHelper();
        string selstr = "select roleId from users where userId=@ha_user";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@ha_user", sid);
        paras[1] = null;
        string rroleId = null;
        SqlDataReader rder = sqlhp.getReader(selstr, paras);
        rder.Read();
        rroleId = rder[0].ToString();
        sqlhp.close();
        return rroleId;
    }
}
