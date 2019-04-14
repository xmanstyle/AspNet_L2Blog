<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyImg.ascx.cs" Inherits="Web_ManagerModule_MyImg" %>
<div style="text-align:center;width:150px;height:200px; margin:10px;">
<div>
    <asp:Image ID="bodyImg" runat="server" Width="100%" Height="180px"/> 
</div>
<div style="margin:0px auto;background:#F3F3F3;height:12%;">
    <asp:Button ID="bodyBtDel" runat="server" Text="删除" 
        onclick="bodyBtDel_Click" />
</div>
</div>