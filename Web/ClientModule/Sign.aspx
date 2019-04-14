<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sign.aspx.cs" Inherits="Sign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户注册</title>
    <link rel="stylesheet" href="../CSS/Sign.css" type="text/css" />
    <script type="text/javascript" src="../JS/Sign.js"></script>
</head>
<body onload="firstChildFocus()">
    <div class="indexDiv"> 
        <a class="indexA" href="../index.aspx">L2Blog</a>
    </div>
    <div class="mainSign">
    <form id="form1" runat="server">
             <table id="userTable">
                <caption>用户注册</caption>
                <tr>
                    <td>用户名:</td>
                    <td><asp:TextBox ID="userName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密码:</td>
                    <td><asp:TextBox ID="userPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>确认密码:</td>
                    <td><asp:TextBox ID="userConfirmPass" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>性别</td>
                    <td id="sex">
                        <asp:RadioButton ID="userSexX" runat="server" GroupName="userSex"  Checked="true"/>保密
                        <asp:RadioButton ID="userSexMale" runat="server" GroupName="userSex"/>男
                        <asp:RadioButton ID="userSexFemale" runat="server" GroupName="userSex"/>女  
                    </td>
                </tr>
                <tr>
                    <td>邮箱:</td>
                    <td><asp:TextBox ID="userEmail" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Button ID="userSubmit" runat="server" Text="注册" OnClick="userSubmit_Click" /></td>
                    <td><input id="userReset" type="reset" value="重置" /></td>
                </tr>
             </table>
        <asp:Label ID="labError" runat="server" Text="" Visible="false" ForeColor="#ff5050" ></asp:Label>
    </form>
    </div>
</body>
</html>
