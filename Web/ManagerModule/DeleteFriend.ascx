<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeleteFriend.ascx.cs" Inherits="Web_ManagerModule_DeleteFriend" %>
<div style="border:1px solid white;margin:5px;width:55px;height:45px;text-align:center; background:#EEE8AA;">
    <div style="margin:0px auto;">
        <asp:Label ID="friendName" runat="server" Text="" Font-Size="Smaller"></asp:Label>
    </div>
    <div style="margin:0px auto;">
        <asp:Button ID="btDelete" runat="server" Text="删除" onclick="btDelete_Click" />
    </div>
</div>