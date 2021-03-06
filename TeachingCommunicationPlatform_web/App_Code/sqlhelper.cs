﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
///SQLHelper
/// </summary>
public class SQLHelper
{
    private SqlConnection con;
    public SQLHelper()
    {
        string constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TeachingCommunicationPlatform_DBConnectionString"].ConnectionString;
        con = new SqlConnection(constr);
    }
    private void open()
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
    }
    /// <summary>
    /// 关闭连接
    /// </summary>
    public void close()
    {
        if (con.State == ConnectionState.Open)
            con.Close();
    }
    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sqlstr">sql命令</param>
    /// <param name="para">参数</param>
    /// <returns>影响的行数</returns>
    public int ExecuteSql(string sqlstr, SqlParameter[] para)
    {
        open();
        SqlCommand cmd = createCommand(sqlstr, para);
        return cmd.ExecuteNonQuery();
    }
    /// <summary>
    /// 执行sql得到reader
    /// </summary>
    /// <param name="sqlstr"></param>
    /// <param name="para"></param>
    /// <returns></returns>
    public SqlDataReader getReader(string sqlstr, SqlParameter[] para)
    {
        open();
        SqlCommand cmd = createCommand(sqlstr, para);
        SqlDataReader rder = cmd.ExecuteReader();
        return rder;
    }
    /// <summary>
    /// 执行sql得到Adapter
    /// </summary>
    /// <param name="sqlstr"></param>
    /// <param name="para"></param>
    /// <returns></returns>
    public SqlDataAdapter getAdapter(string sqlstr, SqlParameter[] para)
    {
        open();
        SqlCommand cmd = createCommand(sqlstr, para);
        return new SqlDataAdapter(cmd);
    }
    public string getAValue(string sqlstr, SqlParameter[] para)
    {
        System.Console.Write("11");
        open();
        SqlCommand cmd = createCommand(sqlstr, para);
        return cmd.ExecuteScalar().ToString();
    }
    private SqlCommand createCommand(string sqlstr, SqlParameter[] para)
    {
        SqlCommand cmd = new SqlCommand(sqlstr, con);
        if (para != null)
            foreach (SqlParameter p in para)
                if (p != null)
                    cmd.Parameters.Add(p);
        return cmd;
    }
    public DataSet getDataSet(string sqlstr, SqlParameter[] para)
    {
        SqlDataAdapter ada = getAdapter(sqlstr, para);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        return ds;
    }
    public void bindGridView(GridView gv, string sqlstr, SqlParameter[] para)
    {
        DataSet ds = getDataSet(sqlstr, para);
        gv.DataSource = ds;
        gv.DataBind();
    }
}