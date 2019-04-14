using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SelfConnection;
using System.Data.SqlClient;

public partial class Web_ManagerModule_UpdateSelf : System.Web.UI.Page
{
    MyConnection manager = new MyConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void FileUpload1()
    {
        string fileName = FileUpload.PostedFile.FileName;
        string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
        if (type == "jpg" || type == "png" || type == "PNG" || type == "JPG" || type == "JPEG" || type == "jpeg" || type == "gif" || type == "GIF")
        {
            labUpError.Visible = false;
            string imgPath = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + type;
            this.FileUpload.SaveAs(Server.MapPath("~/Web/Images/" + imgPath));
            string insertStr = "update Users set Head=@photoPath where NickName=@nickName";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@nickName", Session["userName"].ToString()),
                                                                            new SqlParameter("@photoPath", "Images/"+imgPath) };
            manager.myCmd.Parameters.AddRange(para);
            manager.openConn();
            manager.setCmdStr(insertStr, manager.myConn);
            manager.exeNoQuery();
            manager.closeConn();
        }
        else {
            labUpError.Visible = true;
        }
    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {
        FileUpload1();
        manager.openConn();
        manager.myCmd.Parameters.AddRange(new SqlParameter[]{
            new SqlParameter("@pass", repass.Text), new SqlParameter("@email", email.Text),
            new SqlParameter("@sex", Sex.SelectedValue), new SqlParameter("@age", age.Text), new SqlParameter("@saying", saying.Text),
            new SqlParameter("@tel", tel.Text), new SqlParameter("@goodat", goodat.Text), 
            new SqlParameter("@job", job.Text), new SqlParameter("@address", address.Text),
            new SqlParameter("@nickName1", Session["userName"].ToString())});
            string updateStr = "update Users set PassWord=@pass,Sex=@sex, Age=@age, Signature=@saying,Email=@email, Phone=@tel, Speciality=@goodat,Profession=@job, Address=@address where NickName=@nickName1";
            manager.setCmdStr(updateStr, manager.myConn);
            manager.exeNoQuery();
            manager.closeConn();
            Session["userName"] = null;
            Session["author"] = null;
            Response.Redirect("../ClientModule/Login.aspx");
    }
}