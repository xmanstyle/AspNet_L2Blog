using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_ManagePostType : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        manageApply();/////////////页面加载时先看数据库中是否有好友申请
        manageDelete();////////////页面加载时显示已有的好友放在删除panel 中
    }


    protected void manageApply() 
    {////////////////// 管理好友申请///////////////////////
        string backName = Session["userName"].ToString();
        string getApplyStr = "select ApplyNickName from Apply where ApplyBackName=@applyBackName";
        SqlParameter para = new SqlParameter("@applyBackName", backName);
        manager.myCmd.Parameters.Add(para);
        manager.openConn();
        manager.setCmdStr(getApplyStr, manager.myConn);
        SqlDataReader reader = manager.exeRead();
        if (reader.FieldCount > 0)
        {
            while (reader.Read())
            {
                ///////////////获取用户控件的实例
                Web_ManagerModule_MyApply temp = (Web_ManagerModule_MyApply)Page.LoadControl("MyApply.ascx");

                temp.applyName = reader[0].ToString();
                temp.selfName = backName;
                applyShow.Controls.Add(temp);
                applyShow.Controls.Add(new LiteralControl("<br/>"));
            }
        }
        else if (reader.FieldCount == 0)
        {
            Label emptyLab = new Label();
            emptyLab.Text = "您最近没有新的好友申请!";
            applyShow.Controls.Add(emptyLab);
        }
        reader.Close();
        manager.closeConn();
    }
    protected void manageDelete() 
    {   //////////////////////删除好友panel
        string person = Session["userName"].ToString();
        Table friendsTable = new Table();
        string getFriendStr = "select FriendNickName,FriendBackName from Friends where FriendNickName=@personName or FriendBackName=@personName";
        SqlParameter para = new SqlParameter("@personName", person);
        manager.myCmd.Parameters.Add(para);
        manager.openConn();
        manager.setCmdStr(getFriendStr, manager.myConn);
        SqlDataReader reader = manager.exeRead();
        TableRow row = new TableRow();
        int curNum = 0;
        while (reader.Read())
        {
            TableCell cell = new TableCell();
            curNum++;
            Web_ManagerModule_DeleteFriend temp = (Web_ManagerModule_DeleteFriend)Page.LoadControl("DeleteFriend.ascx");
            temp.name = (reader[0].ToString().Equals(person) ? reader[1].ToString() : reader[0].ToString());
            cell.Controls.Add(temp);
            row.Controls.Add(cell);
            if (curNum % 9 == 0)
            {
                friendsTable.Controls.Add(row);
                row = new TableRow();
            }
            else
            {
                friendsTable.Controls.Add(row);
            }
        }
        deleteShow.Controls.Add(friendsTable);
        reader.Close();
        manager.closeConn();
    }
}