using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// safeFileManager 的摘要说明
/// </summary>
public class safeFileManager : fileManager
{
    string userName;
    SQLHelper sqlHelper;
    string webRootFolder;
    enum opType
    {
        admin,
        user
    }
    enum folderType
    {
        userConfig,
        course,
        invalid
    }
    folderType nfolderType;
    private folderType getPathInfo(string path, out string name)
    {
        int index = path.IndexOf("severFiles\\");
        index += 11;
        string ffolder = path.Substring(index, 5);
        string substr = path.Substring(index);

        name = path.Substring(index, tt);
        int tt = substr.IndexOf('\\');
        if (tt == path.Length)
            return folderType.invalid;
        substr = substr.Substring(tt);
        tt = substr.IndexOf('\\');
        if (tt != path.Length)
            tt--;
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
        nfolderType = safeFileManager.folderType.invalid;
    }
    /// 写根目录
    new public bool SetRootPath(string path)
    {
        string name;
        folderType fdType;
        fdType = getPathInfo(path, out name);
        if (fdType == safeFileManager.folderType.invalid)
            return false;
        nfolderType = fdType;
        strRootFolder = path;
        return true;
    }
}
