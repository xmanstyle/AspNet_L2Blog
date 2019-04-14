using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;

public partial class Sign : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void userSubmit_Click(object sender, EventArgs e)
    {
        if (userName.Text == "" || userPassword.Text == "" || (userConfirmPass.Text == "" || userPassword.Text != userConfirmPass.Text) || userEmail.Text == "")
        {  ///////////不满足注册条件提示相应信息
            labError.Visible = true;
            if (userName.Text == "")
            {
                labError.Text = "请输入用户名!";
            }
            else if (userPassword.Text == "")
            {
                labError.Text = "请输入密码!";
            }
            else if (userConfirmPass.Text == "" || userPassword.Text != userConfirmPass.Text)
            {
                labError.Text = "两次输入密码不一致!";
            }
            else
            {
                labError.Text = "请输入邮箱!";
            }
            return;
        }
        else
        {  //////////////可以注册//////////////
            string username1 = userName.Text;
            string strCheck = "select count(*) from Users where UserName = @username1";
            manager.openConn();
            SqlParameter[] parameters1 = { new SqlParameter("@username1", username1) };
            manager.myCmd.Parameters.AddRange(parameters1);
            manager.setCmdStr(strCheck, manager.myConn);
            if ((int)manager.exeScalar() > 0)
            {  
                labError.Visible = true;
                labError.Text = "该用户已存在!";
                manager.closeConn();
                //return;
            }
            else
            {  ///////////要注册的用户名不存在 可以注册
                string username = userName.Text;
                string nickname = username;
                string userpassword = userPassword.Text;
                string useremail = userEmail.Text;
                string usersex = "";
                string photourl = "";
                string sqlstr = "";
                manager.openConn();
                if (userSexFemale.Checked)
                {
                    usersex = "女";
                    photourl = "Images/userSexFemale.jpg";
                    sqlstr = "insert into Users(UserName,PassWord,NickName,Sex,Email,Head,UserVisited,Power) values(@username,@userpassword,@nickname,@usersex,@useremail,@photourl,0,1)";
                    SqlParameter[] parameters = {new SqlParameter("@username",username),
                                             new SqlParameter("@userpassword",userpassword),
                                             new SqlParameter("@nickname",nickname),
                                             new SqlParameter("@usersex",usersex),
                                             new SqlParameter("@useremail",useremail),
                                             new SqlParameter("@photourl",photourl)};
                    manager.setCmdStr(sqlstr, manager.myConn);
                    manager.myCmd.Parameters.AddRange(parameters);
                    manager.exeNoQuery();
                    manager.closeConn() ;
                }
                else if (userSexMale.Checked)
                {
                    usersex = "男";
                    photourl = "Images/userSexMale.jpg";
                    sqlstr = "insert into Users(UserName,PassWord,NickName,Sex,Email,Head,UserVisited,Power) values(@username,@userpassword,@nickname,@usersex,@useremail,@photourl,0,1)";
                    SqlParameter[] parameters = {new SqlParameter("@username",username),
                                             new SqlParameter("@userpassword",userpassword),
                                             new SqlParameter("@nickname",nickname),
                                             new SqlParameter("@usersex",usersex),
                                             new SqlParameter("@useremail",useremail),
                                             new SqlParameter("@photourl",photourl)};
                    manager.setCmdStr(sqlstr, manager.myConn);
                    manager.myCmd.Parameters.AddRange(parameters);
                    manager.exeNoQuery();
                    manager.closeConn();
                }
                else if (userSexX.Checked)
                {
                    usersex = "保密";
                    photourl = "Images/userSexX.jpg";
                    sqlstr = "insert into Users(UserName,PassWord,NickName,Sex,Email,Head,UserVisited,Power) values(@username,@userpassword,@nickname,@usersex,@useremail,@photourl,0,1)";
                    SqlParameter[] parameters = {new SqlParameter("@username",username),
                                             new SqlParameter("@userpassword",userpassword),
                                             new SqlParameter("@nickname",nickname),
                                             new SqlParameter("@usersex",usersex),
                                             new SqlParameter("@useremail",useremail),
                                             new SqlParameter("@photourl",photourl)};
                    manager.setCmdStr(sqlstr, manager.myConn);
                    manager.myCmd.Parameters.AddRange(parameters);
                    manager.exeNoQuery();
                    manager.closeConn();
                }
                Response.Redirect("Login.aspx");
            }
        }
    }
}