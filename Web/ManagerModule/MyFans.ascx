<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyFans.ascx.cs" Inherits="Web_ManagerModule_MyFans" %>
<div style="text-align:center;width:55px;height:65px;" >
    <div style="margin:0px auto;">
        <asp:Image ID="fansHead" runat="server" Width="40px" Height="40px"/>
    </div>
    <div style="font-size:smaller">
        <asp:Label ID="fansName" runat="server" Text="" ></asp:Label>
    </div>
</div>