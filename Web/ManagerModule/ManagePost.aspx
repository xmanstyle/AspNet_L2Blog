<%@ Page Title="" Language="C#" MasterPageFile="~/Web/ManagerModule/ManageMasterPage.master" AutoEventWireup="true" CodeFile="ManagePost.aspx.cs" Inherits="Web_ManagerModule_ManagePost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="../CSS/ManagePost.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="mainContain">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="2" DataSourceID="SqlDataSource1" ForeColor="Black" 
            GridLines="None" HorizontalAlign="Center" 
            onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" Width="630px" 
            >
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:BoundField DataField="PostTitle" HeaderText="文章标题" 
                    SortExpression="PostTitle" />
                <asp:BoundField DataField="PostZy" HeaderText="文章摘要" 
                    SortExpression="PostZy" />
                <asp:BoundField DataField="PostLastTime" HeaderText="更改时间" 
                    SortExpression="PostLastTime" />
                <asp:CommandField ButtonType="Button" HeaderText="删除" ShowDeleteButton="True" />
                <asp:CommandField HeaderText="编辑" ShowCancelButton="False" 
                    ShowEditButton="True" ShowHeader="True" />
            </Columns>
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:L2BlogConnectionString %>" 
           DeleteCommand="DELETE FROM Posts where ([PostNickName] = @PostNickName) and ([PostTitle] = '<%= temp %>')"
            SelectCommand="SELECT [PostTitle], [PostZy], [PostLastTime] FROM [Posts] WHERE ([PostNickName] = @PostNickName) ORDER BY [PostId] DESC, [PostLastTime] DESC">
            <DeleteParameters>
                <asp:Parameter Name="PostNickName" />

            </DeleteParameters>
            <SelectParameters>
                <asp:SessionParameter Name="PostNickName" SessionField="userName" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    
</asp:Content>

