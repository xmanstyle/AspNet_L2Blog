using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SelfConnection
{
    public class MyConnection
    {
        public SqlConnection myConn;
        public SqlCommand myCmd;
        public SqlDataReader myReader;


        public MyConnection()
        { // 初始化数据库连接
            myConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["L2BlogConnectionString"].ConnectionString);
            myCmd = new SqlCommand();
        }
        public void openConn()
        { // 打开数据库连接
            if (myConn.State != ConnectionState.Open)
            {
                myConn.Open();
            }
        }
        public void closeConn()
        { // 关闭数据库连接
            if (myConn.State != ConnectionState.Closed)
            {
                myConn.Close();
            }
        }
        public SqlDataAdapter getAdapter(string adapterStr, SqlConnection myConn)
        {
            SqlDataAdapter myAdapter = new SqlDataAdapter(adapterStr, myConn);
            return myAdapter;
        }
        public void setCmdStr(string cmdString, SqlConnection myConn)
        { // 设置SqlCommand 连接语句
            myCmd.CommandText = cmdString;
            myCmd.Connection = myConn;
        }
        public object exeScalar()
        {  // 返回查询的记录个数
            return myCmd.ExecuteScalar();
        }
        public void exeNoQuery()
        { // 对数据库进行增删改
            myCmd.ExecuteNonQuery();
        }
        public SqlDataReader exeRead()
        { // 返回所有查询的记录
            myReader = myCmd.ExecuteReader();
            return myReader;
        }
    }
}


/// <summary>
/// MyConnection 的摘要说明
/// </summary>
//public class MyConnection
//{
//    private SqlConnection myConn;
//    private SqlCommand myCmd;
//    private SqlDataAdapter myDataAdapter;
//    private DataSet myDataSet;
//    private SqlDataReader myDataReader;
//    public MyConnection()
//    {
//        //
//        // TODO: 在此处添加构造函数逻辑
//        //
//        myConn = new SqlConnection();
//        myCmd = new SqlCommand();
//        myDataAdapter = new SqlDataAdapter();
//        myDataSet = new DataSet();
//    }
//    //返回SqlConnection对象
//    public SqlConnection GetConnection()
//    {
//        myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
//        return myConn;
//    }

//    //关闭SqlConnection实例连接
//    public void CloseConn()
//    {
//        myConn = GetConnection();
//        myConn.Close();
//        myConn.Dispose();
//    }
//    //读取记录
//    public SqlDataReader Logins(string sql)
//    {
//        myConn = GetConnection();//与数据库连接
//        myCmd.CommandText = sql;
//        myCmd.Connection = myConn;
//        if (myConn.State == ConnectionState.Closed) { myConn.Open(); }
//        myDataReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
//        return myDataReader;
//    }
//    // 更新数据库
//    // <param name="strSql">sqlStr执行的SQL语句</param>
//    public void ExecNonQuery(string strSql)
//    {

//        try
//        {
//            myConn = GetConnection();//与数据库连接
//            myCmd = new SqlCommand();//初始化SqlCommand类对象
//            myCmd.Connection = myConn;
//            myCmd.CommandText = strSql;
//            if (myCmd.Connection.State != ConnectionState.Open)
//            {
//                myCmd.Connection.Open();//打开与数据库的连接
//            }
//            myCmd.ExecuteNonQuery();//执行Sql操作，并返回受影响的行数

//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message, ex);

//        }
//        finally
//        {
//            if (myCmd.Connection.State == ConnectionState.Open)
//            {//断开连接，释放资源
//                myCmd.Connection.Close();
//                myConn.Dispose();
//                myCmd.Dispose();
//            }
//        }
//    }

//    /// <summary>
//    /// 返回一个值
//    /// </summary>
//    /// <param name="strSql">sqlStr执行的SQL语句</param>
//    /// <returns>返回获取的值</returns>
//    public string ExecScalar(string strSql)
//    {

//        try
//        {
//            myConn = GetConnection();//与数据库连接
//            myCmd = new SqlCommand();//初始化SqlCommand类对象
//            myCmd.Connection = myConn;
//            myCmd.CommandText = strSql;
//            if (myCmd.Connection.State != ConnectionState.Open)
//            {
//                myCmd.Connection.Open();//打开与数据库的连接
//            }
//            //使用SqlCommand对象的ExecuteScalar方法查询并返回第一行第一列的值
//            strSql = Convert.ToString(myCmd.ExecuteScalar());
//            return strSql;

//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message, ex);

//        }
//        finally
//        {
//            if (myCmd.Connection.State == ConnectionState.Open)
//            {//断开连接，释放资源
//                myConn.Dispose();
//                myCmd.Connection.Close();
//                myCmd.Dispose();

//            }
//        }
//    }
//    /// <summary>
//    /// 说  明：  GetDataSet数据集，返回数据源的数据表
//    ///	返回值：  数据源的数据表
//    ///	参  数：  sqlStr执行的SQL语句，TableName 数据表名称
//    /// </summary>
//    public DataTable GetDataSet(string strSql, string TableName)
//    {
//        myDataSet = new DataSet();
//        try
//        {
//            myConn = GetConnection();//与数据库连接
//            myDataAdapter = new SqlDataAdapter(strSql, myConn); //实例化SqlDataAdapter类对象
//            myDataAdapter.Fill(myDataSet, TableName);//填充数据集
//            return myDataSet.Tables[TableName];//返回数据集DataSet的表的集合

//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message, ex);
//        }
//        finally
//        {//断开连接，释放资源
//            myConn.Close();
//            myDataAdapter.Dispose();
//            myDataSet.Dispose();
//            myConn.Dispose();
//        }
//    }

//    public static void DoSql(string sql)
//    {
//        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
//        try
//        {
//            myConnection.Open();//打开数据库
//            SqlCommand cmd = new SqlCommand(sql, myConnection);
//            cmd.ExecuteNonQuery();//
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.Message, ex);

//        }
//        finally
//        {
//            if (myConnection.State == ConnectionState.Open)
//            {//断开连接，释放资源
//                myConnection.Close();
//                myConnection.Dispose();
//                myConnection.Dispose();
//            }
//        }
//    }


//    /// <summary>
//    /// 执行查询SQL语句
//    /// </summary>
//    /// <param name="strSQL">待执行SQL语句</param>
//    /// <returns>执行结果的第1行第1列的值</returns>
//    public static object ExecSqlScalar(string strSQL)
//    {
//        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
//        myConnection.Open();
//        SqlCommand cmd = new SqlCommand(strSQL, myConnection);
//        return cmd.ExecuteScalar();
//    }
//    /// <summary>
//    /// 执行非查询SQL语句
//    /// </summary>
//    /// <param name="strSQL">待执行SQL语句</param>
//    /// <returns>受影响的行数</returns>
//    public static int ExecSqlNonQuerry(string strSQL)
//    {
//        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
//        //try {
//            myConnection.Open();
//        //} catch (Exception ex)
//        //{
//        //    throw new Exception(ex.Message, ex);
//        //}
//        SqlCommand cmd = new SqlCommand(strSQL, myConnection);
//        return cmd.ExecuteNonQuery();
//    }
//}
