﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

public class fileManager
{
    protected string strRootFolder;
    public fileManager()
    {
        //根目录
        strRootFolder = HttpContext.Current.Request.PhysicalApplicationPath + "severFiles\\";
        strRootFolder = strRootFolder.Substring(0, strRootFolder.LastIndexOf(@"\"));
    }
    /// 读根目录
    public string GetRootPath()
    {
        return strRootFolder;
    }

    /// 写根目录
    public void SetRootPath(string path)
    {
        strRootFolder = path;
    }
    /// 读取列表
    public List<FileSystemItem> GetItems()
    {
        return GetItems(strRootFolder);
    }

    /// 读取列表
    public List<FileSystemItem> GetItems(string path)
    {
        string[] folders = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);
        List<FileSystemItem> list = new List<FileSystemItem>();
        foreach (string s in folders)
        {
            FileSystemItem item = new FileSystemItem();
            DirectoryInfo di = new DirectoryInfo(s);
            item.Name = di.Name;
            item.FullName = di.FullName;
            item.CreationDate = di.CreationTime;
            item.IsFolder = true;
            list.Add(item);
        }
        foreach (string s in files)
        {
            FileSystemItem item = new FileSystemItem();
            FileInfo fi = new FileInfo(s);
            item.Name = fi.Name;
            item.FullName = fi.FullName;
            item.CreationDate = fi.CreationTime;
            item.IsFolder = false;
            item.Size = fi.Length;
            list.Add(item);
        }
        /*
        if (path.ToLower() != strRootFolder.ToLower())
        {
            FileSystemItem topitem = new FileSystemItem();
            DirectoryInfo topdi = new DirectoryInfo(path).Parent;
            topitem.Name = "[上一级]";
            topitem.FullName = topdi.FullName;
            list.Insert(0, topitem);

            FileSystemItem rootitem = new FileSystemItem();
            DirectoryInfo rootdi = new DirectoryInfo(strRootFolder);
            rootitem.Name = "[根目录]";
            rootitem.FullName = rootdi.FullName;
            list.Insert(0, rootitem);

        }
         * */
        return list;
    }
    /// 创建文件夹
    protected void CreateFolder(string name, string parentName)
    {
        DirectoryInfo di = new DirectoryInfo(parentName);
        di.CreateSubdirectory(name);
    }
    /// 删除文件夹
    protected bool DeleteFolder(string path)
    {
        try
        {
            Directory.Delete(path,true);
            return true;
        }
        catch(Exception exception)
        {
            return false;
        }
    }
    /// 移动文件夹
    protected bool MoveFolder(string oldPath, string newPath)
    {
        try
        {
            Directory.Move(oldPath, newPath);
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// 创建文件
    protected bool CreateFile(string filename, string path)
    {
        try
        {
            FileStream fs = File.Create(path + filename);
            fs.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// 创建文件
    protected bool CreateFile(string filename, string path, byte[] contents)
    {
        try
        {
            FileStream fs = File.Create(path + "\\" + filename);
            fs.Write(contents, 0, contents.Length);
            fs.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// 读取文本文件
    public string OpenText(string parentName)
    {
        StreamReader sr = File.OpenText(parentName);
        StringBuilder output = new StringBuilder();
        string rl;
        while ((rl = sr.ReadLine()) != null)
        {
            output.Append(rl);
        }
        sr.Close();
        return output.ToString();
    }
    /// 写入一个新文件，在文件中写入内容，然后关闭文件。如果目标文件已存在，则改写该文件。 
    protected bool WriteAllText(string parentName, string contents)
    {
        try
        {
            File.WriteAllText(parentName, contents, Encoding.UTF8);
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// 删除文件
    protected bool DeleteFile(string path)
    {
        try
        {
            File.Delete(path);
            return true;
        }
        catch(Exception E)
        {
            return false;
        }
    }

    /// 移动文件
    protected bool MoveFile(string oldPath, string newPath)
    {
        try
        {
            File.Move(oldPath, newPath);
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// 读取文件信息
    protected FileSystemItem GetItemInfo(string path)
    {
        FileSystemItem item = new FileSystemItem();
        if (Directory.Exists(path))
        {
            DirectoryInfo di = new DirectoryInfo(path);
            item.Name = di.Name;
            item.FullName = di.FullName;
            item.CreationDate = di.CreationTime;
            item.IsFolder = true;
            item.LastAccessDate = di.LastAccessTime;
            item.LastWriteDate = di.LastWriteTime;
            item.FileCount = di.GetFiles().Length;
            item.SubFolderCount = di.GetDirectories().Length;
        }
        else
        {
            FileInfo fi = new FileInfo(path);
            item.Name = fi.Name;
            item.FullName = fi.FullName;
            item.CreationDate = fi.CreationTime;
            item.LastAccessDate = fi.LastAccessTime;
            item.LastWriteDate = fi.LastWriteTime;
            item.IsFolder = false;
            item.Size = fi.Length;
        }
        return item;
    }

    /// 复制文件夹
    protected bool CopyFolder(string source, string destination)
    {
        try
        {
            String[] files;
            if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
                destination += Path.DirectorySeparatorChar;
            if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
            files = Directory.GetFileSystemEntries(source);
            foreach (string element in files)
            {
                if (Directory.Exists(element))
                    CopyFolder(element, destination + Path.GetFileName(element));
                else
                    File.Copy(element, destination + Path.GetFileName(element), true);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    /// 判断是否为安全文件名
    public bool IsSafeName(string strExtension)
    {
        strExtension = strExtension.ToLower();//变为小写
        //得到string的.XXX的文件名后缀 LastIndexOf（得到点的位置） Substring（剪切从X的位置）

        if (strExtension.LastIndexOf(".") >= 0)
        { strExtension = strExtension.Substring(strExtension.LastIndexOf(".")); }
        else
        { strExtension = ".txt"; }//如果没有点 就当成txt文件

        //允许上传的扩展名，可以改成从配置文件中读出 
        string[] arrExtension = { ".htm", ".html", ".txt", ".js", ".css", ".xml", ".sitemap", ".jpg", ".gif", ".png", ".rar", ".zip" };

        for (int i = 0; i < arrExtension.Length; i++)
        {
            if (strExtension.Equals(arrExtension[i]))
            {
                return true;
            }
        }
        return false;
    }
    ///  判断是否为可编辑文件
    public bool IsCanEdit(string strExtension)
    {
        strExtension = strExtension.ToLower();//变为小写
        //得到string的.XXX的文件名后缀 LastIndexOf（得到点的位置） Substring（剪切从X的位置）

        if (strExtension.LastIndexOf(".") >= 0)
        { strExtension = strExtension.Substring(strExtension.LastIndexOf(".")); }
        else
        { strExtension = ".txt"; }//如果没有点 就当成txt文件

        //允许上传的扩展名，可以改成从配置文件中读出 
        string[] arrExtension = { ".htm", ".html", ".txt", ".js", ".css", ".xml", ".sitemap" };

        for (int i = 0; i < arrExtension.Length; i++)
        {
            if (strExtension.Equals(arrExtension[i]))
            {
                return true;
            }
        }
        return false;
    }
    protected StreamWriter getAppendSteam(string path)
    {
        try
        {
            return File.AppendText(path);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    protected bool AppendLineToFile(string path, string text)
    {
        StreamWriter wter = null;
        try
        {
            wter = File.AppendText(path);
            wter.WriteLine(text);
            wter.Flush();
            wter.Close();
        }
        catch(Exception e)
        {
            if(wter!=null)
                wter.Close();
            return false;
        }
        return true;
    }
    protected string[] readFile(string path)
    {
        string[] res;
        try
        {
            res = File.ReadAllLines(path);
        }
        catch
        {
            return null;
        }
        return res;
    }
    protected bool isExistDirectory(string path)
    {
        return Directory.Exists(path);
    }
    protected bool copyFile(string destin, string origin)
    {
        StreamReader rder = new StreamReader(File.OpenRead(origin));
        StreamWriter wter = new StreamWriter(File.OpenWrite(destin));
        try
        {
            wter.Write(rder.ReadToEnd());
            rder.Close();
            wter.Close();
            return true;
        }
        catch (Exception e)
        {
            rder.Close();
            wter.Close();
            return false;
        }
    }
    protected StreamReader getStreamReader(string path)
    {
        try
        {
            return new StreamReader(File.OpenRead(path));
        }
        catch(Exception e)
        {
            return null;
        }
    }
    protected StreamWriter getStreamWriter(string path)
    {
        try
        {
            return new StreamWriter(File.OpenWrite(path));
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public FileStream getFileStream(string fileName)
    {
        try
        {
            return new FileStream(fileName, FileMode.Open);
        }
        catch(Exception e)
        {
            return null;
        }
    }
    
}
