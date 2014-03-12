using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;
using System.Timers;
public partial class teacher_myControl : System.Web.UI.Page
{
    SQLHelper sqlhp = new SQLHelper();
    safeFileManager sf = new safeFileManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox_deadline.Text = DateTime.Now.ToString();
        if (!IsPostBack)
        {
            GridViewBind();
            Panel_couNew.Visible = false;
            Panel_listCou.Visible = true;
            Panel_postMsg.Visible = false;
        }
        switch (DropDownList2.SelectedItem.Value)
        {
            case "0":
                TextBox_deadline.Enabled = false;
                break;
            case "1":
                TextBox_deadline.Enabled = true;
                break;
        }
    }
    private void GridViewBind()
    {
        sf.setUser("00000", "11111");
        sf.SetRootPath("users");
        sf.cd(Session["ha_user"].ToString());
        string[] lis = sf.readFile("listens");
        if (lis.Length == 0)
            return;
        string focus;
        SqlParameter[] paras = new SqlParameter[lis.Length];
        StringBuilder stb = new StringBuilder();
        stb.Append("select couId as 课程编号,couName as 课程名称 , type as 属性 , stuNum as 关注人数 ,term as 学期 , createUser as 教师 from Course where couId = @f0 ");
        paras[0] = new SqlParameter("@f0", lis[0]);
        for (int i = 1; i < lis.Length; i++)
        {
            paras[i] = new SqlParameter("@f" + i, lis[i]);
            stb.Append(" union select * from Course where couId = @f" + i);
        }
        DataSet ds = new DataSet();
        SqlDataAdapter da = sqlhp.getAdapter(stb.ToString(), paras);
        sqlhp.close();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0].DefaultView;
        // GridView1.DataKeyNames = new string[] { "couId" }; 
        GridView1.DataBind();


    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView2.DataKeys[e.RowIndex].Values[0].ToString(); //取出要删除记录的主键值
        string delestr1 = "delete from manageCou where couId = @id";
        string delestr2 = "delete from Course  where couId = @id";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("@id", id);
        sqlhp.ExecuteSql(delestr1, paras);
        paras[0] = new SqlParameter("@id", id);
        sqlhp.ExecuteSql(delestr2, paras);
        sqlhp.close();
        sf.SetRootPath("courses");
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        /////////////未dug
        sf.cd(id);
        string[] canFou = sf.readFile("listeners");
        if (canFou.Length != 0)
            for (int i = 0; i < canFou.Length; i++)
            {
                sf.SetRootPath("users");
                sf.cd(canFou[i]);
                sf.deleteStrFromFile("listens", id);

            }
        //////////////
        if (sf.deleteFolder(id))
        {
            GridView1.DataBind();
            Methods.showMessageBox(Response, "删除成功");
        }
        else
        {
            Methods.showMessageBox(Response, "删除失败");
        }
    }
    protected void newOneBtn_Click(object sender, EventArgs e)
    {
        //Panel3.Visible = true;
    }
    protected void subBtn_Click(object sender, EventArgs e)
    {
        if (!sf.setUser(Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        string cname = couNameTB.Text.Trim();
        string cterm = termTB.Text.Trim();
        string ctype = typeTB.Text.Trim();
        string cCreate = Session["ha_user"].ToString();
        string selstr = "select max(couId) from Course";
        string tid = Session["ha_user"].ToString();
        SqlParameter[] paras = new SqlParameter[1];
        string ccouId = sqlhp.getAValue(selstr, paras);
        if (ccouId == "")
        { ccouId = "-1"; }
        ccouId = (Convert.ToInt32(ccouId) + 1).ToString();
        sqlhp.close();
        //bug//////////////////////////////////
        if (Methods.mkCou(ccouId, cname, ctype, "1", cterm, cCreate, tid, "00000", "11111"))//Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {

            Methods.showMessageBox(Response, "添加成功");
            Response.Redirect("\\teacher/myControl.aspx");
        }
        else
        {
            Methods.showMessageBox(Response, "添加失败");
        }
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        //Panel3.Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!sf.setUser("00000", "11111"))//Session["ha_user"].ToString(), Session["ha_pwd"].ToString()))
        {
            Methods.showMessageBox(Response, "对不起您没有权限");
            return;
        }
        Label Label_cou = (Label)GridView1.Rows[e.RowIndex].FindControl("Label_cou");
        string id = Label_cou.Text;
        //string id = GridView1.DataKeys[e.RowIndex].Value.ToString(); //取出要删除记录的主键值
        //删除coures下对我的记录
        sf.SetRootPath("courses");
        sf.cd(id);
        sf.deleteStrFromFile("listeners", Session["ha_user"].ToString());
        //删除user下我的focus记录
        sf.SetRootPath("users");
        sf.cd(Session["ha_user"].ToString());
        sf.deleteStrFromFile("listens", id);
        //更新数据库关注人数
        string upstr = "update Course set stuNum =stuNum-1 where couId = @id";
        SqlParameter[] paras = new SqlParameter[1];
        paras[0] = new SqlParameter("id", id);
        sqlhp.ExecuteSql(upstr, paras);
        sqlhp.close();
        GridView2.DataBind();
        GridViewBind();
    }

    protected void Button_sub_Click(object sender, EventArgs e)
    {
        string cid = DropDownList1.SelectedValue.ToString();
        string ttype = DropDownList2.SelectedValue.ToString();
        bool type;
        if (ttype == "1")
            type = true;
        else
            type = false;
        DateTime ddate = new DateTime();
        DateTime date = ddate.Date;
        DateTime deadline = Convert.ToDateTime(TextBox_deadline.Text.Trim());
        string topic = TextBox_topic.Text.Trim();//排零
        string ttext = TextBox_content.Text.ToString();
        string atMe = TextBox_at.Text.ToString();
        string filename = FileUpload1.FileName.ToString();
        string[] at;
        List<string> at2 = new List<string>();
        List<string> flist = new List<string>();
        flist.Add(filename);
        string[] filelist = flist.ToArray();
        if (type)
        {
            sf.setUser("00000", "11111");
            sf.SetRootPath("course");
            sf.cd(cid);
            at = sf.readFile("listeners");
        }
        else
        {
            string tem = "";
            for (int i = 0; i < atMe.Length; i++)
            {
                if (!atMe[i].Equals(","))
                {
                    tem += atMe[i];
                }
                else
                {
                    at2.Add(tem);
                    tem = "";
                }
            }
            if (tem != "")
                at2.Add(tem);
            at = at2.ToArray();
        }
        CMessage cm = new CMessage();
        if (filename == "")
        {
            filelist = null;
        }
        string mid = CMessage.addNewThing(cid, type, topic, date, deadline, ttext, filelist, at).ToString();
        if (mid == "-1")
            Methods.showMessageBox(Response, "发送失败");
        else
        {
            sf.setUser("00000", "11111");
            sf.SetRootPath("courses");
            sf.cd(cid);
            string[] tm = sf.readFile("listeners");
            for (int i = 0; i < tm.Length; i++)
            {
                sf.SetRootPath("users");
                sf.cd(tm[i].ToString());
                sf.AppendLineToFile("newThings", mid);
                sf.AppendLineToFile("newThings", cid);
            }
            if (at != null)
                for (int i = 0; i < at.Length; i++)
                {
                    //sf.SetRootPath("users");
                    // sf.cd(at[i].ToString());
                    sf.AppendLineToFile("at", mid);
                    sf.AppendLineToFile("at", cid);
                }
        }
        if (FileUpload1.HasFile)
            FileUpload1.SaveAs(Server.MapPath("~/") + "severFiles/" + "courses/" + cid + "/data/" + FileUpload1.FileName);

        // "客户端路径：" + FileUpload1.PostedFile.FileName
        //"文件名：" + System.IO.Path.GetFileName(FileUpload1.FileName) 
        //"文件扩展名：" + System.IO.Path.GetExtension(FileUpload1.FileName) 
        //"文件大小：" + FileUpload1.PostedFile.ContentLength + " KB〈br>" +
        //"文件MIME类型：" + FileUpload1.PostedFile.ContentType + "〈br>" +
        //"保存路径：" + Server.MapPath("upload") + "\\" + FileUpload1.FileName;

    }
    protected void Button_add_Click(object sender, EventArgs e)
    {
        string addp = TextBox_addperson.Text.Trim();
        string addc = TextBox_addcou.Text.Trim();
        string str = "update users set roleId = @newrole where userId=@id";
        SqlParameter[] paras = new SqlParameter[2];
        paras[0] = new SqlParameter("@newrole", "2");
        paras[1] = new SqlParameter("@id", addp);
        sqlhp.ExecuteSql(str, paras);
        string str2 = "insert into manageCou (userId,couId) values (@id,@addc)";
        paras[0] = new SqlParameter("@id", addp);
        paras[1] = new SqlParameter("@addc", addc);
        sqlhp.ExecuteSql(str2, paras);
        sqlhp.close();

    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        Panel_couNew.Visible = false;
        Panel_listCou.Visible = false;
        Panel_postMsg.Visible = false;
        switch (e.Item.Value)
        {
            case "couList": Panel_listCou.Visible = true; break;
            case "postMsg": Panel_postMsg.Visible = true; break;
            case "couMana": Panel_couNew.Visible = true; break;
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList2.SelectedItem.Value)
        {
            case "0":
                TextBox_deadline.Enabled = false;
                break;
            case "1":
                TextBox_deadline.Enabled = true;
                break;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Label_term = (Label)e.Row.FindControl("Label_term");
            Label_term.Text = Methods.analyseTerm(Label_term.Text);
        }
    }
}