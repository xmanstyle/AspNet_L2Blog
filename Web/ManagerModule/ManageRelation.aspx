<%@ Page Title="" Language="C#" MasterPageFile="~/Web/ManagerModule/ManageMasterPage.master" AutoEventWireup="true" CodeFile="ManageRelation.aspx.cs" Inherits="Web_ManagerModule_ManagePostType" %>
<%@ Reference Control="~/Web/ManagerModule/MyApply.ascx" %>
<%@ Reference Control="~/Web/ManagerModule/DeleteFriend.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="../CSS/ManageRelation.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="applyDiv">
        <p>好友申请</p>
        <asp:Panel ID="applyShow" runat="server" HorizontalAlign="Center">
        </asp:Panel>
    </div>
    <div class="applyDiv2">
        <p>好友删除</p>
        <asp:Panel ID="deleteShow" runat="server" HorizontalAlign="Center">
        </asp:Panel>
    </div>
</asp:Content>

