using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_DeleteFriend : System.Web.UI.UserControl
{
    public string name;
    MyConnection manager = new MyConnection();

    public string _name
    {
        set {
            this.name = value.ToString();
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        friendName.Text = name;
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        string deleteStr = "delete from Friends where (FriendNickName=@name1 and FriendBackName = @name2) or (FriendNickName=@name2 and FriendBackName = @name1)";
        SqlParameter[] para = new SqlParameter[] { new SqlParameter("@name1",Session["userName"].ToString()),
                                                                        new SqlParameter("@name2",name)};
        manager.myCmd.Parameters.AddRange(para);
        manager.openConn();
        manager.setCmdStr(deleteStr, manager.myConn);
        manager.exeNoQuery();
        manager.closeConn();
        Response.Redirect("ManageRelation.aspx");
    }
}