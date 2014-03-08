using System;
using System.Collections.Generic;
using System.Text;


/// FileSystemItem

public class FileSystemItem
{
    private string _Name;
    private string _FullName;

    private DateTime _CreationDate;
    private DateTime _LastAccessDate;
    private DateTime _LastWriteDate;

    private bool _IsFolder;

    private long _Size;
    private long _FileCount;
    private long _SubFolderCount;

    private string _Version;
    /// 名称
    public string Name
    {
        get
        {
            return _Name;
        }
        set
        {
            _Name = value;
        }
    }
    /// 完整目录
    public string FullName
    {
        get
        {
            return _FullName;
        }
        set
        {
            _FullName = value;
        }
    }

    /// 创建时间

    public DateTime CreationDate
    {
        get
        {
            return _CreationDate;
        }
        set
        {
            _CreationDate = value;
        }
    }


    /// 是否是文件夹

    public bool IsFolder
    {
        get
        {
            return _IsFolder;
        }
        set
        {
            _IsFolder = value;
        }
    }


    /// 大小

    public long Size
    {
        get
        {
            return _Size;
        }
        set
        {
            _Size = value;
        }
    }


    /// 访问时间

    public DateTime LastAccessDate
    {
        get
        {
            return _LastAccessDate;
        }
        set
        {
            _LastAccessDate = value;
        }
    }


    /// 修改时间

    public DateTime LastWriteDate
    {
        get
        {
            return _LastWriteDate;
        }
        set
        {
            _LastWriteDate = value;
        }
    }


    /// 文件数

    public long FileCount
    {
        get
        {
            return _FileCount;
        }
        set
        {
            _FileCount = value;
        }
    }


    /// 文件夹数

    public long SubFolderCount
    {
        get
        {
            return _SubFolderCount;
        }
        set
        {
            _SubFolderCount = value;
        }
    }


    /// 版本
    public string Version()
    {
        if (_Version == null)
            _Version = GetType().Assembly.GetName().Version.ToString();
        return _Version;
    }

}