using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_WritePost : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { ////////////// this is first request page /////////////////
            if (Session["editPostTitle"] != null)
            { //////////// 如果 Session["editPostTitle"] != null 那就是编辑博文 ///////////
                //Response.Write("edit post");
                string strTitle = Session["editPostTitle"].ToString();
                int flId = 0;
                string getPostStr = "select  PostZy ,PostNr ,PostFlId from Posts where PostNickName=  '" + Session["userName"].ToString() + "' and PostTitle='" + strTitle + "' ";
                manager.openConn();
                manager.setCmdStr(getPostStr, manager.myConn);
                SqlDataReader reader = manager.exeRead();
                txtTitle.Text = strTitle;
                if (reader.Read())
                {
                    txtZy.Text = reader[0].ToString();
                    txtContent.Text = reader[1].ToString();
                    flId = Int32.Parse(reader[2].ToString());
                }
                DropDownList1.SelectedIndex = (flId - 1);
                reader.Close();
                manager.closeConn();
            }
            else if (Session["editPostTitle"] == null)
            {  ////////// 写新的博文 ///////////////
                //Response.Write("new_post");
                txtTitle.Text = "";
                txtZy.Text = "";
                txtContent.Text = "";
            }
        }
    }

    protected void btSubmi_Click(object sender, EventArgs e)
    {
        manager.openConn();
        if (txtTitle.Text != "")
        {  //////////博文标题不能为空
            DateTime timeNow = DateTime.Now;
            string time = timeNow.Year.ToString();
            time = time + "/" + timeNow.Month.ToString();
            time = time + "/" + timeNow.Day.ToString();
            string second = " " + timeNow.Hour.ToString();
            second = second + ":" + timeNow.Minute.ToString();
            second = second + ":" + timeNow.Second.ToString();
            time += second;
            string userStr = Session["userName"].ToString();
            if (Session["editPostTitle"] != null)
            {   ///////////编辑文章,使用更新语句
                //Response.Write("click edit");
                string titleStr = Session["editPostTitle"].ToString();
                int typeId = (DropDownList1.SelectedIndex + 1);
                //titleStr = replace(titleStr);
                string update = "update Posts SET PostTitle= '" + txtTitle.Text + "',PostZy='" + txtZy.Text + "',PostNr='" + txtContent.Text + "',PostLastTime= '" + time + "',PostFlId= '" + typeId + "' where PostNickName = '" + userStr + "' and PostTitle= '" + titleStr + "'";
                manager.setCmdStr(update, manager.myConn);
            }
            else
            {  ///////////写新文章,使用插入语句
                //Response.Write("click new");
                int typeId = (DropDownList1.SelectedIndex + 1);
                int visited = 0;
                int pl = 0;
                string insertStr = "insert into Posts(PostFlId,PostTitle,PostZy,PostNr,PostVisited,PostPl,PostLastTime,PostNickName) values('" + typeId + "','" + txtTitle.Text + "','" + txtZy.Text + "','" + txtContent.Text + "','" + visited + "','" + pl + "','" + time + "','" + userStr + "' ) ";
                manager.setCmdStr(insertStr, manager.myConn);
            }
            Session["editPostTitle"] = null;
            manager.exeNoQuery();
            manager.closeConn();
            Response.Redirect("WritePost.aspx");
            //Response.Write("<script>alert('提交成功')</script>");
        }
        else {
            Response.Write("<script>alert('标题不能为空')</script>");
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        //////////////其实该button的功能就是用于刷新页面，因为提交需要点两次按钮
        if (txtTitle.Text != "")
        {
            Response.Write("<script>alert('保存成功')</script>");
        }
        else
        {
            Response.Write("<script>alert('标题不能为空')</script>");
        }
    }
}