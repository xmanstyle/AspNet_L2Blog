<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录</title>
    <link rel="stylesheet" href="../CSS/Login.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="indexDiv"> 
        <a class="indexA" href="../index.aspx">L2Blog</a>
    </div>
    <div id="userLogin"  >
         <h2>用户登录</h2>
        <p>
            <asp:Label ID="labUserName" runat="server" Text="用户名:"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" value="用户名/邮箱" onFocus="txtOnfocus(this)" ></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="labPassword" runat="server" Text="密码:"></asp:Label>
            &nbsp;<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <asp:Button ID="btLogin" runat="server" Text="登录" onclick="btLogin_Click" /><a href="Sign.aspx" id="aNotSign">还没注册？</a>
    </div>
    </form>
    <script type="text/javascript" src="../JS/Login.js"></script>
</body>
</html>
