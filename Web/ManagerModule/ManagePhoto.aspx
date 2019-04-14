<%@ Page Title="" Language="C#" MasterPageFile="~/Web/ManagerModule/ManageMasterPage.master" AutoEventWireup="true" CodeFile="ManagePhoto.aspx.cs" Inherits="Web_ManagerModule_ManagePhoto" %>
<%@ Reference Control="MyImg.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="../CSS/ManagePhoto.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="uploadDiv">
    <asp:FileUpload ID="FileUpload1" runat="server" BackColor="#FFFFCC" BorderWidth="1px" BorderColor="Black" />&nbsp;<asp:Button ID="btUpload" runat="server" Text="上传" onclick="btUpload_Click" />&nbsp;
    <asp:Label ID="labUpError" runat="server" Text="文件类型错误！" Visible = "false" ForeColor="Red"></asp:Label>
    </div>
     <div class="photos">
         <asp:Panel ID="Panel1" runat="server">
         </asp:Panel>
     </div>
</asp:Content>

