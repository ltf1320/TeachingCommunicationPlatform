using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// CMessage 的摘要说明
/// </summary>
public class CMessage
{
    string _couId;
    int _msgId;
    public string couId
    {
        get { return _couId; }
    }
    public int msgId
    {
        get { return _msgId; }
    }
    /// <summary>
    /// 0:message,1:task
    /// </summary>
    bool type;
    /// <summary>
    /// 标题
    /// </summary>
    string topic;
    /// <summary>
    /// 时间
    /// </summary>
    DateTime date;
    /// <summary>
    /// 截止日期(task only)
    /// </summary>
    DateTime deadLine;
    /// <summary>
    /// 文本
    /// </summary>
    string text;
    /// <summary>
    /// 文件列表
    /// </summary>
    string[] fileList;
    /// <summary>
    /// @人列表
    /// </summary>
    string[] atList;

    public bool getMsgInfo(string couId, int msgId)
    {
        try
        {
            safeFileManager sFM = new safeFileManager();
            sFM.setUser("00000", "11111");
            sFM.SetRootPath("courses\\" + couId);
            FileStream rder = File.OpenRead(sFM.getNPath() + "message");
            StreamReader srder = new StreamReader(rder);
            string id;
            while (true)
            {
                while (true)
                {
                    id = srder.ReadLine();
                    if (id != "")
                        break;
                }
                if (Convert.ToInt32(id) == msgId)
                {
                    readMsg(srder);
                    break;
                }
                else
                    skipMsg(srder);
            }
            return true;
        }
        catch
        { return false; }
    }
    /// <summary>
    /// 读取除id以外的全部信息存入类
    /// </summary>
    /// <param name="rder"></param>
    private bool readMsg(StreamReader rder)
    {
        try
        {
            string buf = rder.ReadLine();
            type = Convert.ToBoolean(buf);
            topic = rder.ReadLine();
            date = Convert.ToDateTime(rder.ReadLine());
            if (type)
                deadLine = Convert.ToDateTime(rder.ReadLine());
            text = rder.ReadLine();
            int fileNum = Convert.ToInt32(rder.ReadLine());
            fileList = new string[fileNum];
            for (int i = 0; i < fileNum; i++)
                fileList[i] = rder.ReadLine();
            int atNum = Convert.ToInt32(rder.ReadLine());
            atList = new string[atNum];
            for (int i = 0; i < atNum; i++)
                atList[i] = rder.ReadLine();
            return true;
        }
        catch
        { return false; }
    }

    private bool skipMsg(StreamReader rder)
    {
        try
        {
            string buf = rder.ReadLine();
            bool type = Convert.ToBoolean(buf);
            buf = rder.ReadLine(); //topic = rder.ReadLine();
            buf = rder.ReadLine(); //date = Convert.ToDateTime(rder.ReadLine());
            if (type)
                buf = rder.ReadLine(); //deadLine = Convert.ToDateTime(rder.ReadLine());
            buf = rder.ReadLine(); //text = rder.ReadLine();
            int fileNum = Convert.ToInt32(rder.ReadLine());
            //fileList = new string[fileNum];
            for (int i = 0; i < fileNum; i++)
                buf = rder.ReadLine(); //fileList[i] = rder.ReadLine();
            int atNum = Convert.ToInt32(rder.ReadLine());
            //atList = new string[atNum];
            for (int i = 0; i < atNum; i++)
                buf = rder.ReadLine(); //atList[i] = rder.ReadLine();
            return true;
        }
        catch
        { return false; }
    }

    public CMessage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
}