using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;

public partial class Login : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text == "") {
            Response.Write(@"<script>alert('用户名不能为空')</script>");
            //return;
        }
        else if (txtPassword.Text == "") {
            Response.Write(@"<script>alert('密码不能为空')</script>");
            //return;
        }
        else
        {
            string username1 = txtUserName.Text.Trim();
            string userpassword = txtPassword.Text.Trim();
            manager.openConn();

            //给标量变量@username1赋值的一种方式
            //SqlParameter[] parameters1 = { new SqlParameter("@username1", username1),
            //                               new SqlParameter("@userpassword",userpassword)};
            //cmd1.Parameters.AddRange(parameters1);

            //给标量变量@username1赋值的另一种方式
            //cmd1.Parameters.AddWithValue("@username1", username1);
            //cmd1.Parameters.AddWithValue("@userpassword", userpassword);

            //string strCheck = "select UserName from Users where UserName=  '"+@username1+"'";
            //在字符串中拼接变量时，单引号('  ')之间不能有空格，否则在最后连接好的字符串中有多余空格的，导致数据库语法错误，不能正常读取想要的值
           
            string strCheck = "select UserName from Users where UserName=  '" + username1 + "'";
            manager.setCmdStr(strCheck, manager.myConn);
            SqlDataReader sdr = manager.exeRead();
            if (sdr.Read()) {//存在用户
                //比较密码
                sdr.Close();
                strCheck = "select UserName,PassWord from Users where UserName= '"+username1+"' and PassWord= '"+userpassword+"'";
                manager.setCmdStr(strCheck, manager.myConn);
                SqlDataReader sdr1 = manager.exeRead();
                if (!sdr1.Read())
                {
                    sdr1.Close();
                    Response.Write("<script>alert('密码错误!')</script>");
                    manager.closeConn();
                }
                else {
                    sdr1.Close();
                    Session["userName"] = txtUserName.Text;
                    Response.Write("<script>alert('登陆成功!')</script>");
                    Response.Redirect("../index.aspx");
                    manager.closeConn();
                }
            }
            else if(!sdr.Read())
            {//不存在该用户
                sdr.Close();
                Response.Write("<script>alert('用户不存在 ! ')</script>");
                manager.closeConn();
            }
        }
    }
}