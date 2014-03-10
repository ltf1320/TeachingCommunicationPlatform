using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
/// <summary>
/// methods 的摘要说明
/// </summary>
public class Methods
{
	private Methods()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string analyseTerm(string term)
    {
        int year = Convert.ToInt32(term.Substring(0, 4));
        StringBuilder res = new StringBuilder();
        switch (term[4])
        {
            case '0':
                res.Append(year - 1);
                res.Append('-');
                res.Append(year);
                res.Append("第二学期");
                break;
            case '1':
                res.Append(year);
                res.Append('-');
                res.Append(year + 1);
                res.Append("第一学期");
                break;
        }
        res.Append("\\");
        return res.ToString();
    }
    public static void showMessageBox(HttpResponse response,string message)
    {
        response.Write("<Script>alert('"+message+"');</Script>");
    }
}