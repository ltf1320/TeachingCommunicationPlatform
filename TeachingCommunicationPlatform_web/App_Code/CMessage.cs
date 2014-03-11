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
    bool _hasMsg;
    /// <summary>
    /// 是否有合理的数据
    /// </summary>
    public bool hasMsg
    {
        get { return _hasMsg; }
    }
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
    public bool type;
    /// <summary>
    /// 标题
    /// </summary>
    public string topic
    { get; set; }
    /// <summary>
    /// 时间
    /// </summary>
    public DateTime date
    {
        get;
        set;
    }
    /// <summary>
    /// 截止日期(task only)
    /// </summary>
    public DateTime deadLine
    { get; set; }
    /// <summary>
    /// 文本
    /// </summary>
    public string text
    { get; set; }
    /// <summary>
    /// 文件列表
    /// </summary>
    public string[] fileList
    { get; set; }
    /// <summary>
    /// @人列表
    /// </summary>
    public string[] atList
    {
        get;
        set;
    }

    public CMessage()
    {
        _hasMsg = false;
    }

    public static bool isMsgExist(string couId, int msgId)
    {
        safeFileManager sFM = new safeFileManager();
        sFM.setUser("00000", "11111");
        sFM.SetRootPath("courses\\" + couId);
        FileStream rder = File.OpenRead(sFM.getNPath() + "message");
        StreamReader srder = new StreamReader(rder);
        try
        {
            string id = srder.ReadLine();//跳过开头的记录值
            while (!srder.EndOfStream)
            {
                while (true)
                {
                    id = srder.ReadLine();
                    if (id != "")
                        break;
                }
                if (Convert.ToInt32(id) == msgId)
                {
                    srder.Close();
                    return true;
                }
                else
                    skipMsg(srder);
            }
            srder.Close();
            return false;
        }
        catch (Exception e)
        {
            srder.Close();
            return false;
        }
    }
    /// <summary>
    /// 访问文件读取信息内容
    /// </summary>
    /// <param name="couId"></param>
    /// <param name="msgId"></param>
    /// <returns>成功返回true</returns>
    public bool getMsgInfo(string couId, int msgId)
    {

        safeFileManager sFM = new safeFileManager();
        sFM.setUser("00000", "11111");
        sFM.SetRootPath("courses\\" + couId);
        FileStream rder = File.OpenRead(sFM.getNPath() + "message");
        StreamReader srder = new StreamReader(rder);
        string id;
        id = srder.ReadLine();
        try
        {
            while (!srder.EndOfStream)
            {
                while (true)
                {
                    id = srder.ReadLine();
                    if (id != "")
                        break;
                }
                if (Convert.ToInt32(id) == msgId)
                {
                    _couId = couId;
                    _msgId = msgId;
                    readMsg(srder);
                    break;
                }
                else
                    skipMsg(srder);
            }
            rder.Close();
            srder.Close();
            return true;
        }
        catch
        {
            rder.Close();
            srder.Close();
            return false;
        }
    }
    /// <summary>
    /// 读取除id以外的全部信息存入类
    /// </summary>
    /// <param name="rder"></param>
    public bool readMsg(StreamReader rder)
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
            _hasMsg = true;
            return true;
        }
        catch
        { return false; }
    }

    private static bool skipMsg(StreamReader rder)
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
    /// <summary>
    /// 创建新消息
    /// </summary>
    /// <param name="couId">课程号</param>
    /// <param name="type">0:message,1:task</param>
    /// <param name="topic">标题</param>
    /// <param name="date">时间</param>
    /// <param name="deadLine">截止时间(task only)</param>
    /// <param name="text">文本</param>
    /// <param name="fileList">文件列表</param>
    /// <param name="userList">@user列表（如果是task则自动@所有关注的人）</param>
    /// <returns>成功返回消息编号，否则返回-1</returns>
    public int createMsg(string couId, bool type, string topic, DateTime date, DateTime deadLine, string text, string[] fileList, string[] @userList)
    {
        safeFileManager sFM = new safeFileManager();
        sFM.setUser("00000", "11111");
        sFM.SetRootPath("courses\\" + couId);

        FileStream rder = File.OpenRead(sFM.getNPath() + "message");
        StreamReader srder = new StreamReader(rder);
        try
        {
            int num;
            string tmp = srder.ReadLine();
            if (tmp == null)
                num = 1;
            else num = Convert.ToInt32(tmp);
            _msgId = num;
            _couId = couId;
            this.type = type;
            if (topic == null) return -1;
            this.topic = topic;
            if (date == null) return -1;
            this.date = date;
            if (type)
            {
                if (deadLine == null) return -1;
                this.deadLine = deadLine;
            }
            if (text == null)
                return -1;
            this.text = text;
            this.fileList = fileList;
            if (type)
            {
                atList = sFM.readFile("listeners");
            }
            else this.atList = @userList;
            _hasMsg = true;
            rder.Close();
            srder.Close();
            return num;
        }
        catch (Exception e)
        {
            rder.Close();
            srder.Close();
            return -1;
        }
    }
    /// <summary>
    /// 将此信息写入文件
    /// </summary>
    /// <returns></returns>
    public bool writeThisMsg()
    {
        StreamWriter swter=null;
        try
        {
            safeFileManager sFM = new safeFileManager();
            sFM.setUser("00000", "11111");
            sFM.SetRootPath("courses\\" + couId);
            string[] ttext = sFM.readFile("message");
            if (ttext.Length == 0)
            {
                ttext = new string[1];
                ttext[0] = (_msgId + 1).ToString();
            }
            else ttext[0] = (_msgId + 1).ToString();
            sFM.DeleteFile("message");
            sFM.CreateFile("message");
            bool hr;
            for (int i = 0; i < ttext.Length; i++)
                hr = sFM.AppendLineToFile("message", ttext[i]);

            swter = sFM.getAppendSteam("message");
            writeThisMsg(swter);
            swter.Close();
            return true;
        }
        catch (Exception e)
        {
            if(swter!=null)
                swter.Close();
            return false;
        }
    }

    private bool writeThisMsg(StreamWriter wter)
    {
        if (!hasMsg) return false;
        try
        {
            wter.WriteLine(_msgId.ToString());
            wter.WriteLine(type.ToString()); //type = Convert.ToBoolean(buf);
            wter.WriteLine(topic);//topic = rder.ReadLine();
            wter.WriteLine(date.ToString());//date = Convert.ToDateTime(rder.ReadLine());
            if (type)
                wter.WriteLine(deadLine.ToString());//deadLine = Convert.ToDateTime(rder.ReadLine());
            wter.WriteLine(text);//text = rder.ReadLine();
            if (fileList == null)
                wter.WriteLine("0");
            else
            {
                wter.WriteLine(fileList.Length.ToString());
                for (int i = 0; i < fileList.Length; i++)
                    wter.WriteLine(fileList[i]);
            }
            if (atList == null)
                wter.WriteLine("0");
            else
            {
                wter.WriteLine(atList.Length.ToString());
                for (int i = 0; i < atList.Length; i++)
                    wter.WriteLine(atList[i]);
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="couId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool deleteMsg(string couId, int msgId)
    {
        bool deled = false;
        CMessage tmsg = new CMessage();
        safeFileManager sFM = new safeFileManager();
        sFM.setUser("00000", "11111");
        sFM.SetRootPath("courses\\" + couId);
        StreamReader srder = sFM.getStreamReader("message");
        sFM.CreateFile("message_tmp");
        StreamWriter wter = sFM.getAppendSteam("message_tmp");
        try
        {
            string buf = srder.ReadLine();
            wter.WriteLine(buf);
            while (!srder.EndOfStream)
            {
                string id = srder.ReadLine();
                while (id == "")
                    id = srder.ReadLine();
                if (Convert.ToInt32(id) == msgId)
                {
                    deled = true;
                    CMessage.skipMsg(srder);
                }
                else
                {
                    tmsg.readMsg(srder);
                    tmsg._msgId = Convert.ToInt32(id);
                    tmsg.writeThisMsg(wter);
                }
            }
            srder.Close();
            wter.Close();
            sFM.DeleteFile("message");
            sFM.copyFile("message", "message_tmp");
            sFM.DeleteFile("message_tmp");
            return deled;
        }
        catch (Exception e)
        {
            wter.Close();
            srder.Close();
            return false;
        }
    }
    /// <summary>
    /// 得到这个
    /// </summary>
    /// <param name="couId"></param>
    /// <returns></returns>
    public static List<CMessage> getMsgs(string couId)
    {
        CMessage tmsg;
        List<CMessage> msglst = new List<CMessage>();
        safeFileManager sFM = new safeFileManager();
        sFM.setUser("00000", "11111");
        sFM.SetRootPath("courses\\" + couId);
        FileStream rder = File.OpenRead(sFM.getNPath() + "message");
        StreamReader srder = new StreamReader(rder);
        try
        {
            string buf = srder.ReadLine();
            while (!srder.EndOfStream)
            {
                int id = Convert.ToInt32(srder.ReadLine());
                tmsg = new CMessage();
                tmsg._msgId = id;
                tmsg.readMsg(srder);
                tmsg._couId = couId;
                msglst.Add(tmsg);
            }
            //倒置
            for (int i = 0; i < msglst.Count/2;i++)
            {
                tmsg = msglst[i];
                msglst[i] = msglst[msglst.Count - i - 1];
                msglst[msglst.Count - i - 1] = tmsg;
            }
            rder.Close();
            srder.Close();
        }
        catch (Exception e)
        {
            rder.Close();
            srder.Close();
            return null;
        }
        return msglst;
    }

    /// <summary>
    /// 添加新消息
    /// </summary>
    /// <param name="couId">课程号</param>
    /// <param name="type">0:message,1:task</param>
    /// <param name="topic">标题</param>
    /// <param name="date">时间</param>
    /// <param name="deadLine">截止时间(task only)</param>
    /// <param name="text">文本</param>
    /// <param name="fileList">文件列表</param>
    /// <param name="userList">@user列表（如果是task则自动@所有关注的人）</param>
    /// <returns>成功返回消息编号，否则返回-1</returns>
    private static object newThingLock = new object();
    public static int addNewThing(string couId, bool type, string topic, DateTime date, DateTime deadLine, string text, string[] fileList, string[] @userList)
    {
        CMessage msg = new CMessage();
        int id;
        lock (newThingLock)
        {
            if ((id = msg.createMsg(couId, type, topic, date, deadLine, text, fileList, userList)) == -1)
                return -1;
            if (!msg.writeThisMsg())
                return -1;
            return id;
        }
    }
    public static bool delNewThing(string couId, int id)
    {
        lock (newThingLock)
        {
            return CMessage.deleteMsg(couId, id);
        }
    }
}