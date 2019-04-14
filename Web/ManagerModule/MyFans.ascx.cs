using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using SelfConnection;

public partial class Web_ManagerModule_MyFans : System.Web.UI.UserControl
{
    public string userName;
    MyConnection manager = new MyConnection();
    public string _userName
    {
        set
        {
            this.userName = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string getInfoStr = "select Head from Users where NickName=@nickName";
        SqlParameter para = new SqlParameter("@nickName", userName);
        manager.openConn();
        manager.myCmd.Parameters.Add(para);
        manager.setCmdStr(getInfoStr, manager.myConn);
        SqlDataReader reader = manager.exeRead();
        string headPath = "";
        if (reader.Read()) {
            headPath = reader[0].ToString();
        }
        reader.Close();
        manager.closeConn();
        fansHead.ImageUrl = "../" + headPath;
        fansName.Text = userName;
    }
}