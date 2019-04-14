using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;
using System.IO;

public partial class Web_ManagerModule_MyImg : System.Web.UI.UserControl
{
    public string imgName;
    MyConnection manager = new MyConnection();
    public string _imgName
    {
        set
        {
            this.imgName = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bodyImg.ImageUrl = "../Images/" + imgName;
    }
    protected void bodyBtDel_Click(object sender, EventArgs e)
    {
        ///////////////////// 删除数据库中的照片 ///////////////////////////
        string delStr = "delete from Photos where PhotoPath=@delName";
        SqlParameter[] para = new SqlParameter[]{new SqlParameter("@delName", imgName)};
        manager.myCmd.Parameters.AddRange(para);
        manager.openConn();
        manager.setCmdStr(delStr, manager.myConn);
        manager.exeNoQuery();
        manager.closeConn();

        ////////////////// 删除本地服务器的照片 /////////////////////////
        try
        {
            FileInfo fi = new FileInfo(Server.MapPath("~/Web/Images/"+imgName));
            //文件存在则删除
            if (fi.Exists)
            {
                fi.Delete();
            }
        }
        catch (Exception ex) {
            throw ex;
        }
        Response.Redirect("ManagePhoto.aspx");
    }
}