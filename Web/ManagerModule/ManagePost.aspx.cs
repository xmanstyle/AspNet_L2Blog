using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_ManagePost : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {  /////////////根据博文标题和作者删除博文
        string temp = GridView1.Rows[e.RowIndex].Cells[0].Text;
        manager.openConn();
        string delStr = "DELETE FROM Posts where ([PostNickName] = '"+Session["userName"].ToString()+"') and ([PostTitle] = '"+temp+"')";
        manager.setCmdStr(delStr, manager.myConn);
        manager.exeNoQuery();
        manager.closeConn();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {   //////////////////////点了编辑以后跳转到写文章页面编辑
         //Response.Write(GridView1.Rows[e.NewEditIndex].Cells[0].Text);
        Session["editPostTitle"] = GridView1.Rows[e.NewEditIndex].Cells[0].Text;
        Response.Redirect("WritePost.aspx");
    }
}