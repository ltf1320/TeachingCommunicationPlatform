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
    /// ����
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
    /// ����Ŀ¼
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

    /// ����ʱ��

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


    /// �Ƿ����ļ���

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


    /// ��С

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


    /// ����ʱ��

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


    /// �޸�ʱ��

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


    /// �ļ���

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


    /// �ļ�����

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


    /// �汾
    public string Version()
    {
        if (_Version == null)
            _Version = GetType().Assembly.GetName().Version.ToString();
        return _Version;
    }

}