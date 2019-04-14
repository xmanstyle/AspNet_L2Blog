<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ly.aspx.cs" Inherits="Web_YL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ly个人介绍</title>
    <link rel="Stylesheet" type="text/css" href="CSS/author.css" />
</head>
<body>
    <div class="backAbout">
        <a href="about.aspx">返回</a>
    </div>
    <div class="ly">
        <img src="Images/lc.jpg" width="400px" height="400px"/>
    </div>
        <table id="tab" onmouseover="onTabHover()" onmouseout="onTabOut()">
        <caption>个人信息</caption>
            <tr>
                <td class="right">姓名:</td>
                <td class="left">李宇</td>
            </tr>
            <tr>
                <td class="right">年龄:</td>
                <td class="left">20</td>
            </tr>
            <tr>
                <td class="right">学校:</td>
                <td class="left">西南民族大学</td>
            </tr>
            <tr>
                <td class="right">院系:</td>
                <td class="left">计科学院</td>
            </tr>
            <tr>
                <td class="right"><img src="Images/tel_c.jpg" /></td>
                <td class="left">o3o3oo@vip.qq.com</td>
            </tr>
            <tr>
                <td class="right"><img src="Images/tel_b.jpg" /></td>
                <td class="left">304016332@qq.com</td>
            </tr>
            <tr>
                <td class="right"><img src="Images/tel_a.jpg" /></td>
                <td class="left">13540791174</td>
            </tr>
        </table>
    <script type="text/javascript">
        function onTabHover() {
            var tempTab = document.getElementById("tab");
            tempTab.style.border = "1px solid white";
            tempTab.style.color = "#71C671";
            tempTab.style.fontsize = "32px";
        }
        function onTabOut() {
            var tempTab = document.getElementById("tab");
            tempTab.style.border = "0px";
            tempTab.style.color = "#fff";
            tempTab.style.fontsize = "30px";
        }
    </script>
</body>
</html>
