<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Web_index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>L2_Blog</title>
    <link href="CSS/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  src="JS/index.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--Title-->
    <div class="a">
        <div class="a1">
            <asp:Panel ID="loginPanel" runat="server" Enabled="true" Visible="true">
                    <a href="./ClientModule/Login.aspx">登录</a> <a href="./ClientModule/Sign.aspx">注册</a>
                    <a href="about.aspx">关于</a>
            </asp:Panel>
            <asp:Panel ID="userPanel" runat="server" Enabled="false" Visible="false" >
                <asp:Image ID="userHead" runat="server" Height="30" Width="30"/>
                <asp:Label ID="userNickName" runat="server" Font-Bold="true"></asp:Label>
                <asp:LinkButton ID="userExit" runat="server" onclick="userExit_Click">退出</asp:LinkButton>
            </asp:Panel>
        </div>
        <div class="a2">
            <div class="a3">
                <a href="index.aspx" class="logoA">L2Blog</a>
            </div>
            <p>
                <div class="myBlog" id="myBlogId">
                    <asp:LinkButton ID="toMyBlog" runat="server" onclick="toMyBlog_Click">我的博客</asp:LinkButton>
                </div>
            </p>
        </div>
    </div>
    <!--MainDiv-->
    <div class="b">
        <!--Content-->
        <div class="b1">
            <!--Search-->
            <div class="b2">
            </div>
            <!--Posts-->
            <div class="b3">
                <!--title-->
                <h3>
                    博文</h3>
                <div class="b4">
                    <asp:DataList ID="DataList1" runat="server" 
                       OnItemCommand="DataList1_ItemCommand">
                        <ItemTemplate>
                            <div class="b6">
                                <asp:Label ID="Label1" runat="server" Text="《"></asp:Label>
                                <asp:LinkButton ID="titleButton" runat="server" ForeColor="#669900" Font-Size="X-Large" Font-Underline="True" Font-Overline="False" 
                                     CommandName='<%# Eval("PostTitle").ToString()+"$"+Eval("PostNickName").ToString()%>' ><%# Eval("PostTitle") %></asp:LinkButton>
                                <asp:Label ID="Label2" runat="server" Text="》"></asp:Label>
                            </div>
                            <div class="b5">
                                <asp:Label ID="Label3" runat="server" Text="-------作者:"></asp:Label>
                                <asp:LinkButton ID="authorButton" runat="server" ForeColor="#0099CC" Font-Size="Larger" CommandName='<%# Eval("PostNickName") %>' ><%# Eval("PostNickName") %></asp:LinkButton>
                            </div>
                            <br />
                            <div class="b7">
                                <asp:Label ID="PostZyLabel" runat="server" Text='<%# Eval("PostZy") %>' />
                            </div>
                            <div class="b8">访问量：<asp:Label ID="PostVisitedLabel" runat="server" Text='<%# Eval("PostVisited") %>' /></div>
                            <div class="b9">评论量：<asp:Label ID="PostPlLabel" runat="server" Text='<%# Eval("PostPl") %>' /></div>
                            <div class="b10">时间：<asp:Label ID="PostLastTimeLabel" runat="server" Text='<%# Eval("PostLastTime") %>' /></div>
                            <br/>
                            <br />
                            <div class="itemHr">
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <div class="pageFooter">
                    当前页：<asp:Label ID="lblCurrent" runat="server" Text="1"></asp:Label>
                    总页数：<asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
                    <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">第一页</asp:LinkButton>
                    <asp:LinkButton ID="lbntUp" runat="server" OnClick="lbntUp_Click">上一页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDown" runat="server" OnClick="lbtnDown_Click">下一页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">最后一页</asp:LinkButton>
                    </div>
                </div>
            </div>
            <asp:TextBox ID="txtType" runat="server" Visible="false"></asp:TextBox>
        </div>
        <!--Sidebar-->
        <div class="sideBar">
            <h3>推荐博文</h3>
            <div class="b12">
            <asp:DataList ID="DataList3" runat="server" DataSourceID="SqlDataSource3" 
                OnItemCommand="DataList3_ItemCommand">
                <ItemTemplate>
                    <div class="b11">
                    <asp:LinkButton ID="LinkButton1" runat="server"  ForeColor="#0099CC"
                    CommandName='<%# Eval("PostTitle").ToString()+"$"+Eval("PostNickName").ToString()%>'><%# Eval("PostTitle") %>----作者:<%# Eval("PostNickName") %></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            </div>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:L2BlogConnectionString %>" 
                SelectCommand="SELECT top 5 [PostTitle], [PostNickName] FROM [Posts] ORDER BY [PostVisited] DESC, [PostNickName]">
            </asp:SqlDataSource>
            <!--Categories-->
            <!--Title-->
            <h3>博文分类</h3>
            <div class="blogClass">
                <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlDataSource2" 
                    Width="75px" OnItemCommand="DataList2_ItemCommand">
                    <ItemTemplate>
                        <div class="blogItem">
                            <asp:LinkButton ID="blogItemButton" runat="server" ForeColor="#907952" CommandName='<%# Eval("PostClassName") %>'  ><%# Eval("PostClassName") %></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString=" <%$ ConnectionStrings:L2BlogConnectionString %>"
                SelectCommand="SELECT [PostClassName] FROM [PostClass] ORDER BY [PostClassId]"> 
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <!--Footer-->
    </form>
</body>
</html>
