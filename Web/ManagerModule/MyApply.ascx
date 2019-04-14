<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyApply.ascx.cs" Inherits="Web_ManagerModule_MyApply" %>
<div style="border:1px solid white;background:#EEE8AA;width:400px;height:40px;margin:0px auto;margin-top:10px;">
    <div style="display:inline">
        <asp:Label ID="applyLab" runat="server" Text=""></asp:Label>
    </div>
    <div style="display:inline;float:right;margin:10px;">
        <asp:Button ID="btRefuse" runat="server" Text="拒绝" onclick="btRefuse_Click" />
    </div>
    <div style="display:inline;float:right;margin:10px;">
        <asp:Button ID="btAgree" runat="server" Text="同意" onclick="btAgree_Click" />
    </div>
</div>