using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_ManagerModule_ManageMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   ////////显示管理页面中的登录用户
            lbtToPersonalPage.Text = Session["userName"].ToString() + "的博客";
        }
    }

    protected void lkManagePost_Click(object sender, EventArgs e)
    {  /////////跳转到管理博文
        Response.Redirect("ManagePost.aspx");
    }

    protected void lkManageType_Click(object sender, EventArgs e)
    {  /////////跳转到管理关系
        Response.Redirect("ManageRelation.aspx");
    }

    protected void lkManagePhoto_Click(object sender, EventArgs e)
    {  /////////跳转到管理照片
        Response.Redirect("ManagePhoto.aspx");
    }

    protected void lkManageWrite_Click(object sender, EventArgs e)
    {  /////////跳转到写文章
        if (Session["editPostTitle"] != null)
        {
            Session["editPostTitle"] = null;
        }
        Response.Redirect("WritePost.aspx");
    }

    protected void lbtToPersonalPage_Click(object sender, EventArgs e)
    {  //////////回到个人页面

        /////////// 还得清除当前页面中的 Session
        if (Session["editPostTitle"] != null)
        {
            Session["editPostTitle"] = null;
        }
        Response.Redirect("PersonalPage.aspx");
    }
    protected void lbtSetInfo_Click(object sender, EventArgs e)
    {  ////////跳转到修改个人资料
        Response.Redirect("UpdateSelf.aspx");
    }
}
