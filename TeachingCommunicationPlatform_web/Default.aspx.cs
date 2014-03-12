using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        safeFileManager sFM = new safeFileManager();
        sFM.SetRootPath("courses");
        sFM.setUser("00000", "11111");
        sFM.deleteFolder("10");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                FileUpload1.SaveAs(Server.MapPath("upload") + "\\" + FileUpload1.FileName);
                Label1.Text = "客户端路径：" + FileUpload1.PostedFile.FileName + "〈br>" +
                              "文件名：" + System.IO.Path.GetFileName(FileUpload1.FileName) + "〈br>" +
                              "文件扩展名：" + System.IO.Path.GetExtension(FileUpload1.FileName) + "〈br>" +
                              "文件大小：" + FileUpload1.PostedFile.ContentLength + " KB〈br>" +
                              "文件MIME类型：" + FileUpload1.PostedFile.ContentType + "〈br>" +
                              "保存路径：" + Server.MapPath("upload") + "\\" + FileUpload1.FileName;
            }
            catch (Exception ex)
            {
                Label1.Text = "发生错误：" + ex.Message.ToString();
            }
        }
        else
        {
            Label1.Text = "没有选择要上传的文件！";
        }

    }
}