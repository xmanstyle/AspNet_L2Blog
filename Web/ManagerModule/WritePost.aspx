<%@ Page Title="" Language="C#" MasterPageFile="~/Web/ManagerModule/ManageMasterPage.master" AutoEventWireup="true" CodeFile="WritePost.aspx.cs" Inherits="Web_ManagerModule_WritePost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="../CSS/WritePost.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="borderDiv2">
    <table>
        <tr>
            <td class="right" >标题：</td>
            <td class="left"><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="right">分类：</td>
            <td class="left" >
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="PostClassName" 
                    DataValueField="PostClassId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr >
            <td class="right">摘要：</td>
            <td class="left">
                <asp:TextBox ID="txtZy" runat="server" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="right">内容：</td>
            <td class="left">
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" 
                    Width="599px" Height="118px" style="margin-left: 0px"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td class="btSubmit">
                <asp:Button ID="btSave" runat="server" Text="保存" onclick="btSave_Click" />
                <asp:Button ID="btSubmi" runat="server" Text="提交" 
                    onclick="btSubmi_Click" />
             </td>
        </tr>
    </table>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
      ConnectionString="<%$ ConnectionStrings:L2BlogConnectionString %>" 
       SelectCommand="SELECT [PostClassId], [PostClassName] FROM [PostClass] ORDER BY [PostClassId]">
    </asp:SqlDataSource>
</asp:Content>

