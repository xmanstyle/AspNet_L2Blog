using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using SelfConnection;

public partial class Web_index : System.Web.UI.Page
{
    string pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] ORDER BY [PostLastTime] DESC";
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
            DataListBind();////////绑定数据表Posts,获得每条博文记录
            if (Session["userName"] == null)
            {  ///////用户没登录////////////
                userPanel.Enabled = false;
                userPanel.Visible = false;
            }
            else
            {  /////////用户登录/////////////
                string username1 = Session["userName"].ToString();
                manager.openConn();
                string strGetUser = "select NickName,Head from Users where UserName=  '" + username1 + "'";
                manager.setCmdStr(strGetUser, manager.myConn);
                SqlDataReader reader = manager.exeRead();
                string nickName = null;
                string head = null;
                if (reader.Read())
                {  //////读取reader中数据的两种方法
                    nickName = reader[0].ToString();
                    head = reader[1].ToString();
                    //nickName= reader.GetString(reader.GetOrdinal("NickName"));
                    //head = reader.GetString(reader.GetOrdinal("Head"));
                }
                reader.Close();
                manager.closeConn();

                ////////////////相应控件的显示与隐藏
                loginPanel.Enabled = false;
                loginPanel.Visible = false;
                userPanel.Enabled = true;
                userPanel.Visible = true;
                userHead.ImageUrl = head;
                userNickName.Text = nickName;
            }
    }
    protected void userExit_Click(object sender, EventArgs e)
    {  /////////////用户退出///////////
        loginPanel.Enabled = true;
        loginPanel.Visible = true;
        userPanel.Enabled = false;
        userPanel.Visible = false;
        Session["userName"] = null;
    }
    protected void toMyBlog_Click(object sender, EventArgs e)
    {   ///////////////去我的博客///////////////////////////////
        if (Session["userName"] == null)
        {  /////////用户没登录//////////////
            //Response.Write(@"<script>alert('请您先登录！')</script>");
            Response.Redirect("ClientModule/Login.aspx");
        }
        else {
            //////////用户已登录//////////////
            Session["author"] = Session["userName"];
            Session["postTitle"] = null;
            Response.Redirect("ManagerModule/PersonalPage.aspx");
        }
    }
    private void DataListBind()
    {   ///////////绑定相应的数据表//////////
        int current_page = Convert.ToInt32(lblCurrent.Text);
        manager.openConn();
        SqlDataAdapter oda = manager.getAdapter(pageStr, manager.myConn);
        DataSet ds = new DataSet();
        oda.Fill(ds);

        PagedDataSource ps = new PagedDataSource();
        ps.DataSource = ds.Tables[0].DefaultView;
        ps.AllowPaging = true;
        ps.PageSize = 5;
        lblTotal.Text = ps.PageCount.ToString();
        ps.CurrentPageIndex = current_page - 1;
        lbtnFirst.Enabled = true;
        lbntUp.Enabled = true;
        lbtnDown.Enabled = true;
        lbtnLast.Enabled = true;
        if (current_page == 1)
        {
            lbtnFirst.Enabled = false;
            lbntUp.Enabled = false;
        }
        if (current_page == Convert.ToInt32(lblTotal.Text))
        {
            lbtnLast.Enabled = false;
            lbtnDown.Enabled = false;
        }
        DataList1.DataSource = ps;
        DataList1.DataBind();
        manager.closeConn();
    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {  ///////////第一页//////////////
        if (txtType.Text != "")
        {
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostFlId='" + Int32.Parse(txtType.Text) + "'ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = "1";
        DataListBind();
    }
    protected void lbtnDown_Click(object sender, EventArgs e)
    {  ///////////下一页//////////////
        if (txtType.Text != "")
        {
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostFlId='" + Int32.Parse(txtType.Text) + "'ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) + 1).ToString();
        DataListBind();
    }
    protected void lbntUp_Click(object sender, EventArgs e)
    {  ///////////上一页//////////////
        if (txtType.Text != "")
        {
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostFlId='" + Int32.Parse(txtType.Text) + "'ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) - 1).ToString();
        DataListBind();
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {  ///////////最后一页//////////////
        if (txtType.Text != "")
        {
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostFlId='" + Int32.Parse(txtType.Text) + "'ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = lblTotal.Text;
        DataListBind();
    }

    protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        /////////获取博文分类的item,并绑定相应的博文显示
        int item= e.Item.ItemIndex+1;
        txtType.Text = item.ToString();
        lblCurrent.Text = "1";
        pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostFlId='"+item+"'ORDER BY [PostLastTime] DESC";
        DataListBind();
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {   //主要博文
        string temp = e.CommandName;///////////获取前台控件的"PostTitle$author"
        if(temp.Contains("$")){
        //--------------看博文 
            /////////////////由于点击DataList 的每个项，要传给跳转页相应的Session
            string[] temp2 = temp.Split('$');
            Session["postTitle"] = temp2[0];
            Session["author"] = temp2[1];
            string username1 = Session["author"].ToString();
            string postTitleStr = Session["postTitle"].ToString();

            //////////////更新作者访客量 和 博文的浏览量
            string strGetUser = "update Users set UserVisited=UserVisited+1 where NickName='" + username1 + "'";////////通过拼接形成SQL语句(不安全)
            string strPostVisited = "update Posts set PostVisited=PostVisited+1 where PostNickName='" +Session["author"].ToString()+ "' and PostTitle = '" + postTitleStr + "'";
            manager.openConn();
            manager.setCmdStr(strPostVisited, manager.myConn);
            manager.exeNoQuery();
            manager.setCmdStr(strGetUser, manager.myConn);
            manager.exeNoQuery();

            ////////////使用博文 id 来在个人页面更新评论量
            string getPostIdStr = "select PostId from Posts where PostTitle=@postTitle and PostNickName=@postNickName";/////使用SqlParameter给引用变量赋值形成SQL语句(较安全)
            SqlParameter[] para1 = new SqlParameter[]{new SqlParameter("@postTitle",Session["postTitle"].ToString()),
                                                                          new SqlParameter("@postNickName",Session["author"].ToString())};
            manager.myCmd.Parameters.AddRange(para1);
            manager.setCmdStr(getPostIdStr, manager.myConn);
            SqlDataReader reader1 = manager.exeRead();
            if (reader1.Read())
            {
                Session["commentPostId"] = Int32.Parse(reader1[0].ToString());
            }
            reader1.Close();
            manager.exeNoQuery();
            manager.closeConn();
        }else{
        //--------------看作者
            Session["postTitle"] = null;
            Session["author"] = e.CommandName;
            string username1 = Session["author"].ToString();
            string strGetUser = "update Users set UserVisited=UserVisited+1 where NickName='" + username1 + "'";
            manager.openConn();
            manager.setCmdStr(strGetUser, manager.myConn);
            manager.exeNoQuery();
            manager.closeConn();
        }
        Response.Redirect("ManagerModule/PersonalPage.aspx");
    }
    protected void DataList3_ItemCommand(object source, DataListCommandEventArgs e) 
    {   //推荐博文
        string temp = e.CommandName;
        if (temp.Contains("$"))
        {
           //--------------看推荐博文
            string[] temp2 = temp.Split('$');
            Session["postTitle"] = temp2[0];
            Session["author"] = temp2[1];
        }
        string username1 = Session["author"].ToString();
        string postTitleStr = Session["postTitle"].ToString();

        
        string strGetUser = "update Users set UserVisited=UserVisited+1 where NickName='" + username1 + "'";
        string strPostVisited = "update Posts set PostVisited=PostVisited+1 where PostNickName='" + username1 + "' and PostTitle = '" + postTitleStr + "'";
        manager.openConn();
        manager.setCmdStr(strGetUser, manager.myConn);
        manager.exeNoQuery();
        manager.setCmdStr(strPostVisited, manager.myConn);
        manager.exeNoQuery();

        ////////////使用博文 id 来在个人页面更新评论量
        string getPostIdStr = "select PostId from Posts where PostTitle=@postTitle and PostNickName=@postNickName";
        SqlParameter[] para1 = new SqlParameter[]{new SqlParameter("@postTitle",Session["postTitle"].ToString()),
                                                                          new SqlParameter("@postNickName",Session["author"].ToString())};
        manager.myCmd.Parameters.AddRange(para1);
        manager.setCmdStr(getPostIdStr, manager.myConn);
        SqlDataReader reader1 = manager.exeRead();
        if (reader1.Read())
        {
            Session["commentPostId"] = Int32.Parse(reader1[0].ToString());
        }
        reader1.Close();
        manager.closeConn();

        Response.Redirect("ManagerModule/PersonalPage.aspx");
    }
}