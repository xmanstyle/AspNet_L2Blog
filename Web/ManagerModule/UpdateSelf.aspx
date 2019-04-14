<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateSelf.aspx.cs" Inherits="Web_ManagerModule_UpdateSelf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改个人资料</title>
    <link rel="Stylesheet" type="text/css" href="../CSS/Update.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="indexDiv"> 
        <a class="indexA" href="../index.aspx">L2Blog</a>
    </div>
    <div class="center">
        <table>
            <tr>
                <td class="tdStyle">
                    头像：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload" runat="server" 
                        />
                    <asp:Label ID="labUpError" runat="server" Text="" ForeColor="#FF5050"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    密码：
                </td>
                <td>
                    <asp:TextBox ID="pass" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="密码不能为空" ControlToValidate="pass" 
                        Display="Dynamic" ForeColor="#FF5050"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    确认密码：
                </td>
                <td>
                    <asp:TextBox ID="repass" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1"
                        runat="server" ErrorMessage="两次输入不一致" Display="Dynamic" 
                        ValueToCompare="pass" Operator="NotEqual" ForeColor="#FF5050" ControlToValidate="repass" 
                        ></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    邮箱：
                </td>
                <td>
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="邮箱格式不正确" 
                        ControlToValidate="email" Display="Dynamic" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ForeColor="#FF5050"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    性别：
                </td>
                <td class="left">
                    <asp:CheckBoxList ID="Sex" runat="server" RepeatDirection="Horizontal" text-align="left">
                        <asp:ListItem>男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                        <asp:ListItem>保密</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    年龄：
                </td>
                <td>
                    <asp:TextBox ID="age" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    个性签名：
                </td>
                <td>
                    <asp:TextBox ID="saying" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    电话：
                </td>
                <td>
                    <asp:TextBox ID="tel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    擅长：
                </td>
                <td>
                    <asp:TextBox ID="goodat" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    职业：
                </td>
                <td>
                    <asp:TextBox ID="job" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    地址：
                </td>
                <td>
                    <asp:TextBox ID="address" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btsubmit" runat="server" Text="提交" onclick="btsubmit_Click" />&nbsp;&nbsp;<asp:Button ID="btreset" runat="server"
                    Text="重置" />
            </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
