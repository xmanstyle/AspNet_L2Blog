<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="Web_about" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关于</title>
    <link rel="Stylesheet" type="text/css" href="CSS/about.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="siteInfo">
        <div class="logoDiv">
            <a href="index.aspx">L2Blog</a>
        </div>
        <p>
           &nbsp;&nbsp;L2Blog 是由Lc 和Ly  共同构建的网站 ，L2 的含义是两人的姓氏首字母，Blog 说明这是一个博客系统。
           该博客构建成功于2016.12.3，该博客实现了多用户使用，个人博客空间。可以发表博文，添加关注和好友，上传图片，
           对博文发表评论等。
        </p>
    </div>
    <div class="lyDiv">
        <a href="Ly.aspx"><img src="Images/Ly.jpg" width="80px" height="80px"/></a>
        <a href="Ly.aspx"><p>Ly</p></a>
    </div>
    <div class="lcDiv">
        <a href="Lc.aspx"><img src="Images/Lc.jpg" width="80px" height="80px"/></a>
        <a href="Lc.aspx" style="text-decoration:none"><p>Lc</p></a>
    </div>
    </form>
</body>
</html>
