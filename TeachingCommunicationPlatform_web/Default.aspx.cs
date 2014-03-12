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
        /*
        SQLHelper sqlHelper = new SQLHelper();
        string sql = "insert into users values(@userId,1,@Name,@pwd,@email,NULL,NULL)";
        SqlParameter[] para = new SqlParameter[4];
        para[0] = new SqlParameter("@userId", "00000");
        para[1] = new SqlParameter("@Name", "bool");
        para[2] = new SqlParameter("@pwd", "11111");
        para[3] = new SqlParameter("@email", "bool@bool.com");
        Response.Write(sqlHelper.ExecuteSql(sql, para));
         * */
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