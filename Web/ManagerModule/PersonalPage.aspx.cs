using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using SelfConnection;

public partial class Web_ManagerModule_PersonalPage : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    Table fansTable = new Table();
    string postAuthor;
    string pageStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        postAuthor = Session["author"].ToString();
        //if (!IsPostBack)
        //{
            initPersonInfo();
            if (Session["userName"] == null)
            {
                ///////////没登录的情况/////////////////////
                exit.Visible = false;
                operate.Visible = false;
            }
            else
            {
                /////////////// 已经登录//////////////////
                exit.Visible = true;
                if (Session["userName"].ToString().Equals(Session["author"].ToString()))
                {
                    //////////////////// 进入自己的个人博客//////////////
                    operate.Visible = true;
                }
                else
                {
                    //////////////////// 进入别人的个人博客//////////////
                    operate.Visible = false;
                }
            }
            if ((Session["postTitle"] == null) && (Session["author"] != null))
            {
                ////////// 看作者信息//////////
                pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostNickName='" + postAuthor + "' ORDER BY [PostLastTime] DESC";
                this.DataListBind();
                pnlFooter.Visible = true;
                showPlId.Visible = false;
                plZoonId.Visible = false;
                lookPostId.Visible = false;
            }
            else if ((Session["postTitle"] != null) && (Session["author"] != null))
            {
                ///////// 看博客内容///////////
                //Response.Write(" look post " + "Session['postTitle']=" + Session["postTitle"].ToString() + "\nSession['author']=" + Session["author"].ToString());
                string username1 = Session["author"].ToString();
                string postTitleStr = Session["postTitle"].ToString();
                string strGetUser = "select  PostTitle ,PostNr from Posts where PostNickName=  '" + username1 + "' and PostTitle='" + postTitleStr + "' ";
                manager.openConn();
                manager.setCmdStr(strGetUser, manager.myConn);
                SqlDataReader reader = manager.exeRead();
                string postTitle = null;
                string postNr = null;
                if (reader.Read())
                {
                    postTitle = reader[0].ToString();
                    postNr = reader[1].ToString();
                }
                reader.Close();
                manager.closeConn();

                //承载博文标题
                Label labPostTitle = new Label();
                labPostTitle.Style["display"] = "block";
                labPostTitle.Style["font-size"] = "30px";
                labPostTitle.Style["color"] = "#648BCE";

                //承载博文内容
                Label labPostNr = new Label();
                labPostNr.Style["font-size"] = "25px";

                labPostTitle.Text = postTitle;
                labPostNr.Text = "&nbsp;&nbsp;" + postNr;
                pnlpostTitle.Controls.Add(labPostTitle);

                pnlContain.Controls.Add(labPostNr);//////////////博文内容 容器//////
                pnlFooter.Visible = false;
            }
            //显示个人博文分类
            setPersonBlogClass(Session["author"].ToString());
            setPersonFans(Session["author"].ToString());
            setPersonFriend(Session["author"].ToString());
        //}
    }

    /// <summary>
    /// 获得便于插入数据库的当前时间
    /// </summary>
    /// <returns></returns>
    protected string getNowTime() {
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

    //////////////// 个人博客分类/////////////////////////////////////////////
    protected void setPersonBlogClass(string person)
    {

        //////////// 通过作者发表过的博文来显示他的博文分类
        string selectClassStr = "select distinct PostClassName from Posts,PostClass where PostNickName='" + person + "' and Posts.PostFlId=PostClass.PostClassId ";
        manager.openConn();
        manager.setCmdStr(selectClassStr, manager.myConn);
        SqlDataReader readClass = manager.exeRead();
        int i = 0;
        while (readClass.Read())
        {
            LinkButton temp = new LinkButton();
            temp.Text = readClass[0].ToString();
            temp.ID = "itemClass" + (++i);
            temp.Attributes.Add("style", "font-size:40px");
            temp.Click += new System.EventHandler(itemClick);
            blogClassContain.Controls.Add(temp);
            blogClassContain.Controls.Add(new LiteralControl("<br/>"));
        }
        readClass.Close();
        manager.closeConn();
    }

    protected void itemClick(object sender, EventArgs e)
    {  /////////////////个人博文分类的点击事件//////////////////////
        //////////////////////由看文章跳转到看博文记录////////////////////////////
        pnlFooter.Visible = true;
        showPlId.Visible = false;
        plZoonId.Visible = false;
        lookPostId.Visible = false;

        LinkButton item = (LinkButton)sender;
        txtItemName.Text = item.Text;
        lblCurrent.Text = "1";

        Session["postTitle"] = null;

        String postAuthor = Session["author"].ToString();
        string getTypeIdStr = "select PostClassId from PostClass where PostClassName='" + txtItemName.Text + "'";
        string type = "";
        manager.openConn();
        manager.setCmdStr(getTypeIdStr, manager.myConn);
        SqlDataReader read = manager.exeRead();
        if (read.Read())
        {
            type = read[0].ToString();
        }
        read.Close();
        manager.closeConn();
        pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostNickName='" + postAuthor + "' and PostFlId='" + type + "' ORDER BY [PostLastTime] DESC";
        DataListBind();
    }

    /// <summary>
    /// 显示个人粉丝
    /// </summary>
    protected void setPersonFans(string person)
    {
        string getFansStr = "select ConcernNickName from Concern where ConcernBackName='"+person+"'";
        manager.openConn();
        manager.setCmdStr(getFansStr, manager.myConn);
        SqlDataReader reader = manager.exeRead();
        TableRow row = new TableRow();
        int curNum = 0;
            while (reader.Read())
            {
                TableCell cell = new TableCell();
                curNum++;
                Web_ManagerModule_MyFans temp = (Web_ManagerModule_MyFans)Page.LoadControl("MyFans.ascx");
                temp.userName = reader[0].ToString();
                cell.Controls.Add(temp);
                row.Controls.Add(cell);
                if (curNum % 3 == 0)
                {
                    fansTable.Controls.Add(row);
                    row = new TableRow();
                }
                else {
                    fansTable.Controls.Add(row);
                }
            }
            fansContain.Controls.Add(fansTable);
        reader.Close();
        manager.closeConn();
    }

    /// <summary>
    /// 显示个人好友
    /// </summary>
    protected void setPersonFriend(string person) {
        Table friendsTable = new Table();
        string getFriendStr = "select FriendNickName,FriendBackName from Friends where FriendNickName=@personName or FriendBackName=@personName";
        SqlParameter para = new SqlParameter("@personName",person);
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
                Web_ManagerModule_MyFans temp = (Web_ManagerModule_MyFans)Page.LoadControl("MyFans.ascx");
                temp.userName = (reader[0].ToString().Equals(person) ? reader[1].ToString() : reader[0].ToString());
                cell.Controls.Add(temp);
                row.Controls.Add(cell);
                if (curNum % 3 == 0)
                {
                    friendsTable.Controls.Add(row);
                    row = new TableRow();
                }
                else {
                    friendsTable.Controls.Add(row);
                }
            }
            friendContain.Controls.Add(friendsTable);
        reader.Close();
        manager.closeConn();
    }

    private void DataListBind()
    {  
        int current_page = Convert.ToInt32(lblCurrent.Text);
        manager.openConn();
        SqlDataAdapter oda = manager.getAdapter(pageStr, manager.myConn);
        DataSet ds = new DataSet();
        oda.Fill(ds);

        PagedDataSource ps = new PagedDataSource();
        ps.DataSource = ds.Tables[0].DefaultView;
        ps.AllowPaging = true;
        ps.PageSize = 8;
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
    {
        if (txtItemName.Text != "")
        {
            String postAuthor = Session["author"].ToString();
            string getTypeIdStr = "select PostClassId from PostClass where PostClassName='" + txtItemName.Text + "'";
            string type = "";
            manager.openConn();
            manager.setCmdStr(getTypeIdStr, manager.myConn);
            SqlDataReader read = manager.exeRead();
            if (read.Read())
            {
                type = read[0].ToString();
            }
            read.Close();
            manager.closeConn();
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostNickName='" + postAuthor + "' and PostFlId='" + type + "' ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = "1";
        DataListBind();
    }
    protected void lbtnDown_Click(object sender, EventArgs e)
    {
        if (txtItemName.Text != "")
        {
            String postAuthor = Session["author"].ToString();
            string getTypeIdStr = "select PostClassId from PostClass where PostClassName='" + txtItemName.Text + "'";
            string type = "";
            manager.openConn();
            manager.setCmdStr(getTypeIdStr, manager.myConn);
            SqlDataReader read = manager.exeRead();
            if (read.Read())
            {
                type = read[0].ToString();
            }
            read.Close();
            manager.closeConn();
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostNickName='" + postAuthor + "' and PostFlId='" + type + "' ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) + 1).ToString();
        DataListBind();
    }
    protected void lbntUp_Click(object sender, EventArgs e)
    {
        if (txtItemName.Text != "")
        {
            String postAuthor = Session["author"].ToString();
            string getTypeIdStr = "select PostClassId from PostClass where PostClassName='" + txtItemName.Text + "'";
            string type = "";
            manager.openConn();
            manager.setCmdStr(getTypeIdStr, manager.myConn);
            SqlDataReader read = manager.exeRead();
            if (read.Read())
            {
                type = read[0].ToString();
            }
            read.Close();
            manager.closeConn();
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostNickName='" + postAuthor + "' and PostFlId='" + type + "' ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) - 1).ToString();
        DataListBind();
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        if (txtItemName.Text != "")
        {
            String postAuthor = Session["author"].ToString();
            string getTypeIdStr = "select PostClassId from PostClass where PostClassName='" + txtItemName.Text + "'";
            string type = "";
            manager.openConn();
            manager.setCmdStr(getTypeIdStr, manager.myConn);
            SqlDataReader read = manager.exeRead();
            if (read.Read())
            {
                type = read[0].ToString();
            }
            read.Close();
            manager.closeConn();
            pageStr = "SELECT [PostZy], [PostTitle], [PostNickName], [PostLastTime], [PostVisited], [PostPl] FROM [Posts] WHERE PostNickName='" + postAuthor + "' and PostFlId='" + type + "' ORDER BY [PostLastTime] DESC";
        }
        lblCurrent.Text = lblTotal.Text;
        DataListBind();
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {   /////////////////// 在当前的个人页面中点击博文记录，跳转到看博文( 还是在当前页面中) ////////////////////////
        Session["postTitle"] = e.CommandName;
        Session["author"] = postAuthor;
        Response.Redirect("PersonalPage.aspx");
    }
    protected void initPersonInfo()
    {  ////////////////// 初始化个人信息/////////////////////////
        string testUser = Session["author"].ToString();
        manager.openConn();
        string strGetUser = "select Head,NickName,Sex,Speciality,UserVisited,Signature from Users where UserName=  '" + testUser + "'";
        manager.setCmdStr(strGetUser, manager.myConn);
        SqlDataReader reader = manager.exeRead();
        if (reader.Read())
        {
            personImg.ImageUrl = "../" + reader[0].ToString();
            nickName.Text = reader[1].ToString();
            sex.Text = reader[2].ToString();
            visiter.Text = reader[4].ToString();
            saying.Text = reader[5].ToString();
            reader.Close();
        }
    }
    protected void toBeFans_Click(object sender, EventArgs e)
    { ///////////////////// 添加关注////////////////////////////////
        if (Session["userName"] == null)
        {/////////////// 没登录 不能添加关注//////////////////////
            Response.Write("<script>alert('您未登录！')</script>");
        }
        else
        {////////////// 登录了 可以添加关注////////////////////
            if (Session["userName"].ToString().Equals(Session["author"].ToString()))
            {/////////////////自己不可添加自己为关注///////////////////////// 
                Response.Write("<script>alert('不可关注自己！')</script>");
            }
            else
            {  ////////////可以添加别人为关注
                string concernStr = "select count(*) from Concern where ConcernNickName=@concernNickName and ConcernBackName=@concernBackName";
                SqlParameter[] para = new SqlParameter[]{new SqlParameter("@concernNickName", Session["userName"].ToString()),
                                                                                 new SqlParameter("@concernBackName",Session["author"].ToString())};
                manager.openConn();
                manager.myCmd.Parameters.AddRange(para);
                manager.setCmdStr(concernStr, manager.myConn);
                SqlDataReader reader = manager.exeRead();
                int res = 0;
                if (reader.Read()) {
                    res = Int32.Parse(reader[0].ToString());
                }
                reader.Close();
                if (res >= 1)
                { ////////////// 关注过用户//////////////////////
                    Response.Write("<script>alert('您已关注过该用户！')</script>");
                }
                else {
                    ////////////////////以前未关注过该用户////////////////////////
                    string addConcernStr = "insert into Concern(ConcernNickName,ConcernBackName) values (@concernNickName,@concernBackName)";
                    string addLyStr = "insert into Ly values(@concernNickName,@concernBackName,@lyLastTime,@lyNr)";
                    string time = getNowTime();
                    string lyNr = Session["userName"].ToString() + " 关注了 " + Session["author"].ToString();
                    SqlParameter[] para2 = new SqlParameter[] {new SqlParameter("@lyLastTime",time),new SqlParameter("@lyNr",lyNr) };
                    manager.myCmd.Parameters.AddRange(para2);
                    manager.setCmdStr(addConcernStr, manager.myConn);
                    manager.exeNoQuery();

                    manager.setCmdStr(addLyStr, manager.myConn);
                    manager.exeNoQuery();
                    manager.closeConn();
                    Response.Write("<script>alert('您已成功关注该用户！')</script>");
                }
            }
        }
        //Response.Redirect("PersonalPage.aspx");
    }

    protected void toBeFriend_Click(object sender, EventArgs e)
    {////////////////////// 添加好友///////////////////////////////
        if (Session["userName"] == null)
        {
            /////////////////没登录///////////////////
            Response.Write("<script>alert('您未登录!')</script>");
        }
        else 
        { 
            ///////////////登录////////////////////////
            if (Session["userName"].ToString().Equals(Session["author"].ToString()))
            {/////////////////自己不可添加自己为好友///////////////////////// 
                Response.Write("<script>alert('不可添加自己为好友！')</script>");
            }
            else
            { 
                //////////////////可以添加别人//////////////////////////////
                string otherName = Session["author"].ToString();
                string selfName = Session["userName"].ToString();
                string friendStr = "select FriendNickName,FriendBackName from Friends where FriendNickName=@selfName or FriendBackName=@selfName";
                SqlParameter para = new SqlParameter("@selfName",selfName);
                manager.openConn();
                manager.myCmd.Parameters.Add(para);
                manager.setCmdStr(friendStr, manager.myConn);
                SqlDataReader reader = manager.exeRead();
                bool flg = false;
                while(reader.Read()){
                    if (reader[0].ToString().Equals(otherName) || reader[1].ToString().Equals(otherName)) {
                        flg = true;
                        break;
                    }
                }
                reader.Close();
                if(flg){
                    /////////////////已经添加过该好友///////////////////
                    Response.Write("<script>alert('您已添加过该好友！')</script>");
                }
                else 
                {
                    ////////////没添加过该好友 可以添加//////////////
                    string insertStr = "insert into Apply values(@friendNickName,@friendBackName,@applyLastTime,@applyState)";
                    string time = getNowTime();
                    SqlParameter[] para2 = new SqlParameter[] {new SqlParameter("@applyLastTime",time),
                                                                                    new SqlParameter("@applyState","同意"),
                                                                                  new SqlParameter("@friendNickName",selfName),
                                                                                new SqlParameter("@friendBackName",otherName)};
                    manager.myCmd.Parameters.AddRange(para2);
                    manager.setCmdStr(insertStr, manager.myConn);
                    manager.exeNoQuery();
                    manager.closeConn();
                    Response.Write("<script>alert('好友申请已发送！')</script>");
                }
            }
        }
    }
    protected void btExit_Click(object sender, EventArgs e)
    { //////////////////// 退出///////////////////////////////////////
        Session["userName"] = null;
        Session["author"] = null;
        Session["postTitle"] = null;
        Response.Redirect("../index.aspx");
    }
    protected void btSubmitPl_Click(object sender, EventArgs e)
    {////////////////////发表评论///////////////////////////////////
        if (Session["userName"] == null)
        {
            Response.Redirect("../ClientModule/Login.aspx");
            return;
        }
        string plWho = "";//评论对象名字
        string insertPlStr = "";
        int postId = 0;
        DateTime timeNow = DateTime.Now;
        string time = timeNow.Year.ToString();
        time = time + "/" + timeNow.Month.ToString();
        time = time + "/" + timeNow.Day.ToString();
        string second = " " + timeNow.Hour.ToString();
        second = second + ":" + timeNow.Minute.ToString();
        second = second + ":" + timeNow.Second.ToString();
        time += second;
        if (dropPlName.SelectedValue == "")
        {
            plWho = "文章";
        }
        else {
            plWho = dropPlName.SelectedValue;
        }

        postId = Int32.Parse(Session["commentPostId"].ToString());

        /////////////////////// 比较安全的数据库insert 语句//////////////////////////////
        insertPlStr = "insert into Comments(CommentPostId,CommentNickName,CommentLastTime,CommentBackName,CommentNr) values(@commentId,@commentNickName,@commentLastTime,@commentBackName,@commentNr)";
        SqlParameter[] para2 = new SqlParameter[] { new SqlParameter("@commentId",postId),new SqlParameter("@commentNickName",Session["userName"].ToString()),
                                                                          new SqlParameter("@commentLastTime",time),new SqlParameter("@commentBackName",plWho),
                                                                            new SqlParameter("@commentNr",plBox.Text)};
        manager.openConn();
        manager.myCmd.Parameters.AddRange(para2);
        manager.setCmdStr(insertPlStr, manager.myConn);
        manager.exeNoQuery();

        /////////////////////// 不安全的数据库update语句//////////////////////////////
        string strPostPl = "update Posts set PostPl = (select count(*) from Comments where CommentPostId = '" + Session["commentPostId"].ToString() + "') where PostId ='"+postId+"'";
        manager.setCmdStr(strPostPl, manager.myConn);
        manager.exeNoQuery();
        manager.closeConn();
        Response.Redirect("PersonalPage.aspx");
    }
    protected void dropPlName_DataBound(object sender, EventArgs e)
    {
        ListItem item = new ListItem("文章", "");
        dropPlName.Items.Insert(0, item);
    }
}