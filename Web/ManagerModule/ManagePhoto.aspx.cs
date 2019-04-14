using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_ManagePhoto : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    Table imgTable = new Table();
    protected void Page_Load(object sender, EventArgs e)
    {
        ///////////////// 加载用户控件 (并给用户控件传图片名字)////////////////////////
        //if (!IsPostBack)
        //{/////////刷新以后会继续添加相同的照片，所以这里不使用

            /////////////跳转到该页面时，先获取该用户已有的照片
            string getImgStr = "select PhotoPath from Photos where PhotoOwnerName = @nickName1";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@nickName1", Session["userName"].ToString()) };
            manager.openConn();
            manager.myCmd.Parameters.AddRange(para);
            manager.setCmdStr(getImgStr, manager.myConn);
            SqlDataReader reader = manager.exeRead();
            int curNum = 0;
            TableRow row = new TableRow();
            if (reader.FieldCount!=0)
            {  /////////////如果有读到照片，就动态添加
                while (reader.Read())
                {  /////////////动态加载用户控件
                    TableCell cell = new TableCell();
                    curNum++;
                    Web_ManagerModule_MyImg img = (Web_ManagerModule_MyImg)Page.LoadControl("MyImg.ascx");
                    img.imgName = reader[0].ToString();
                    cell.Controls.Add(img);
                    row.Controls.Add(cell);
                    if (curNum % 6 == 0)
                    {  //////////////////////每行显示六张照片
                        imgTable.Controls.Add(row);
                        row = new TableRow();
                    }
                    else
                    {
                        imgTable.Controls.Add(row);
                    }
                }
                Panel1.Controls.Add(imgTable);
            }
            else
            {   ////////////////没读到照片时的处理
                Label showInfo = new Label();
                showInfo.Text = "您还没有上传图片";
                showInfo.ControlStyle.Font.Size = 30;
                showInfo.ControlStyle.Font.Bold = true;
                showInfo.Attributes.Add("style", "color:White");
                Panel1.Controls.Add(showInfo);
            }
            reader.Close();
            manager.closeConn();
        //}
    }
    protected void btUpload_Click(object sender, EventArgs e)
    {  /////////// 上传图片到数据库 及本地服务器 ///////////////////////////
        string fileName = this.FileUpload1.PostedFile.FileName;///获取上传的图片的文件名
        string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
        if (type == "jpg" || type == "png" || type == "PNG" || type == "JPG" || type == "JPEG" || type == "jpeg" || type == "gif" || type == "GIF")
        {
            labUpError.Visible = false;
            string imgPath = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + type;
            this.FileUpload1.SaveAs(Server.MapPath("~/Web/Images/" + imgPath));////////////上传的照片存在服务器中的路径
            string insertStr = "insert into Photos(PhotoOwnerName,PhotoPath) values(@nickName,@photoPath)";
            SqlParameter[] para = new SqlParameter[]{new SqlParameter("@nickName", Session["userName"].ToString()),
                                                         new SqlParameter ("@photoPath", imgPath)};
            manager.myCmd.Parameters.AddRange(para);
            manager.openConn();
            manager.setCmdStr(insertStr, manager.myConn);
            manager.exeNoQuery();
            manager.closeConn();
        }
        else {
            labUpError.Visible = true;
        }
        Response.Redirect("ManagePhoto.aspx");
    }
}