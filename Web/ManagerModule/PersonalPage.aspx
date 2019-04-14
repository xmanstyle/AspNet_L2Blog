<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonalPage.aspx.cs" Inherits="Web_ManagerModule_PersonalPage" %>
<%@ Reference Control="MyFans.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人主页</title>
    <link rel="Stylesheet" type="text/css" href="../CSS/PersonalPage.css" />
    <link rel="Stylesheet" type="text/css" href="../CSS/index.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainPage">
        <div class="header">
            <a class="indexA" href="../index.aspx">L2Blog</a>
            <asp:Panel ID="exit" runat="server" >
                <div class="divExit">
                    <asp:LinkButton ID="btExit" runat="server" Text="退出" onclick="btExit_Click" />
                </div>
            </asp:Panel>
        </div>
        <div class="personalSaying">
        <div style="width:80%;float:left;">
            <marquee><p>
                <asp:Label ID="saying" runat="server" Text="This is you saying"></asp:Label>
            </p></marquee>
        </div>
            <asp:Panel ID="operate" runat="server" >
                <div class="divManage"><a id="aManage" href="ManagePost.aspx">管理</a></div>
                <div class="divWrite"><a id="aWrite" href="WritePost.aspx">写文章</a></div>
            </asp:Panel>
        </div>
        <div class="borderDiv"></div>
        <div class="content">
            <div class="sideLeft">
                <p class="blogClassTitle">个人信息</p>
                <div class="blogClass2">
                    <div class="personDetail">    
                    <asp:Image ID="personImg" runat="server" ImageUrl="#" Height="80" Width="80" /><br />
                        昵称:<asp:Label ID="nickName" runat="server" Text=""></asp:Label><br />
                        性别:<asp:Label ID="sex" runat="server" Text=""></asp:Label><br />
<%--                        擅长:<asp:Label ID="goodAt" runat="server" Text=""></asp:Label><br />--%>
                        访客:<asp:Label ID="visiter" runat="server" Text=""></asp:Label><br />
                    </div>
                    <asp:Panel ID="toOther" runat="server">
                        <asp:Button ID="toBeFans" runat="server" Text="+关注" BackColor="Green" 
                            ForeColor="White" onclick="toBeFans_Click"/>
                        <asp:Button ID="toBeFriend" runat="server" Text="+好友" BackColor="Green"  
                            ForeColor="White" onclick="toBeFriend_Click"/>
                    </asp:Panel>
                </div>
                <asp:TextBox ID="txtItemName" runat="server" Visible="false"></asp:TextBox>
                <p class="blogClassTitle">博客分类</p>
                <div class="blogClass2">
                    <asp:Panel ID="blogClassContain" runat="server" BackColor="#C1CCDB" Width="100%" Height="100%">
                    </asp:Panel>
                </div>
            </div>
            <div class="allBlog">
                <div style="margin-left:15px;margin-right:15px;height:96%" runat="server" id="lookPostId">
                <div class="postTitleDiv">
                    <%-- 放置博文记录--%>
                    <asp:Panel ID="pnlpostTitle" runat="server" >
                    </asp:Panel>
                </div>
                <%-- 放置博文内容--%>
                <asp:Panel ID="pnlContain" runat="server" BorderColor="White" BackColor="#D3E5E5" Height="65%" BorderWidth="1">
                </asp:Panel>
                </div>

                <%-- 评论显示区域--%>
                <div class="showPl" runat="server" id="showPlId">
                    <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlDataSource2">
                        <ItemTemplate>
                            <asp:Label ID="CommentNickNameLabel" runat="server" 
                                Text='<%# Eval("CommentNickName") %>'  Font-Bold="true" ForeColor="#008E31"/>
                            对
                            <asp:Label ID="CommentBackNameLabel" runat="server" 
                                Text='<%# Eval("CommentBackName") %>'  ForeColor="#58A3B3" Font-Bold="true"/>
                            说:(<%# Eval("CommentLastTime") %>)
                            <br />
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="CommentNrLabel" runat="server" Text='<%# Eval("CommentNr") %>'  ForeColor="#48936C" Font-Bold="true"/>
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:L2BlogConnectionString %>" 
                        SelectCommand="SELECT [CommentNickName], [CommentBackName], [CommentNr], [CommentLastTime] FROM [Comments] WHERE ([CommentPostId] = @CommentPostId) ORDER BY [CommentLastTime]">
                        <SelectParameters>
                            <asp:SessionParameter Name="CommentPostId" SessionField="commentPostId" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                <%-- 评论区域--%>
                <div class="plZoon" runat="server" id="plZoonId">
                <asp:Panel ID="plPnl" runat="server">
                    对<asp:DropDownList ID="dropPlName" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="CommentNickName" 
                        DataValueField="CommentNickName" ondatabound="dropPlName_DataBound" ></asp:DropDownList>
                    说:<br />
                    <asp:TextBox ID="plBox" runat="server" TextMode="MultiLine" Width="90%" BorderColor="Blue"></asp:TextBox>
                    <asp:Button ID="btSubmitPl" runat="server" Text="发表评论" 
                        onclick="btSubmitPl_Click" />
                </asp:Panel>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:L2BlogConnectionString %>" 
                    
                    SelectCommand="SELECT distinct [CommentNickName] FROM [Comments] WHERE ([CommentPostId] = @CommentPostId)">
                    <SelectParameters>
                        <asp:SessionParameter Name="CommentPostId" SessionField="commentPostId" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:DataList ID="DataList1" runat="server" 
                       OnItemCommand="DataList1_ItemCommand">
                        <ItemTemplate>
                            <div class="b6">
                                <asp:Label ID="Label1" runat="server" Text="《"></asp:Label>
                                <asp:LinkButton ID="titleButton" runat="server" ForeColor="#669900" Font-Size="X-Large" Font-Underline="True" Font-Overline="False" 
                                     CommandName='<%# Eval("PostTitle").ToString()%>' ><%# Eval("PostTitle") %></asp:LinkButton>
                                <asp:Label ID="Label2" runat="server" Text="》"></asp:Label>
                            </div>
                            <br />
<%--                            <div class="b5">
                                <asp:Label ID="Label3" runat="server" Text="-------作者:"></asp:Label>
                                <asp:LinkButton ID="authorButton" runat="server" ForeColor="#0099CC" Font-Size="Larger" CommandName='<%# Eval("PostNickName") %>' ><%# Eval("PostNickName") %></asp:LinkButton>
                            </div>
                            <br />--%>
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
                <div class="blogFooter">
                   <asp:Panel ID="pnlFooter" runat="server">
                    <div>
                    当前页：<asp:Label ID="lblCurrent" runat="server" Text="1"></asp:Label>
                    总页数：<asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
                    <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">第一页</asp:LinkButton>
                    <asp:LinkButton ID="lbntUp" runat="server" OnClick="lbntUp_Click">上一页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDown" runat="server" OnClick="lbtnDown_Click">下一页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">最后一页</asp:LinkButton>
                    </div>
                    </asp:Panel>
                 </div>
            </div>
            <div class="relation">
                <p class="blogClassTitle">好友</p>
                <div class="blogClass2">
                    <div>
                        <asp:Panel ID="friendContain" runat="server">
                        </asp:Panel>
                    </div>
                </div>
                <p class="blogClassTitle">粉丝</p>
                <div class="blogClass2">
                    <div>
                        <asp:Panel ID="fansContain" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
