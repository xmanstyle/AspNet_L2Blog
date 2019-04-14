using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_MyApply : System.Web.UI.UserControl
{
    MyConnection manager = new MyConnection();
    public string applyName;//申请人
    public string selfName;//本人(被申请人)
    public string _applyName {
        set {
            this.applyName = value.ToString();
        }
    }
    public string _selfName
    {
        set
        {
            this.selfName = value.ToString();
        }
    }
    protected string getNowTime()
    {
        DateTime timeNow = DateTime.Now;
        string time = timeNow.Year.ToString();
        time = time + "/" + timeNow.Month.ToString();
        time = time + "/" + timeNow.Day.ToString();
        string second = " " + timeNow.Hour.ToString();
        second = second + ":" + timeNow.Minute.ToString();
        second = second + ":" + timeNow.Second.ToString();
        time += second;
        return time;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        applyLab.Text = applyName + " 想添加您为好友 ";
    }
    protected void addSamePara(){
        string time = getNowTime();
        SqlParameter[] para1 = new SqlParameter[]{new SqlParameter("@NickName",selfName),
                                                                          new SqlParameter("@BackName",applyName),
                                                                           new SqlParameter("@applyTime",time)};
        manager.myCmd.Parameters.AddRange(para1);
    }
    protected void delete() {
        string deleteApply = "delete from Apply where ApplyNickName=@BackName and ApplyBackName=@NickName";
        manager.openConn();
        manager.setCmdStr(deleteApply, manager.myConn);
        manager.exeNoQuery();
        manager.closeConn();
    }
    protected void btAgree_Click(object sender, EventArgs e)
    { /////////////////////// 同意添加好友////////////////////////////
        string insertFriend = "insert into Friends values(@NickName,@BackName,@applyTime)";
        addSamePara();

        //////////////////插入朋友表/////////////////
        manager.openConn();
        manager.setCmdStr(insertFriend, manager.myConn);
        manager.exeNoQuery();
        manager.closeConn();

        delete();

        string resStr = "您和"+applyName+"成为好友了 ！";
        Response.Write("<script>alert('"+resStr+"')</script>");
        Response.Redirect("ManageRelation.aspx");
    }

    protected void btRefuse_Click(object sender, EventArgs e)
    { ////////////////////不同意好友申请//////////////////////////////////
        addSamePara();
        delete();
        string resStr = "您拒绝了" + applyName + "的好友申请 ！";
        Response.Write("<script>alert('" + resStr + "')</script>");
        Response.Redirect("ManageRelation.aspx");
    }
}