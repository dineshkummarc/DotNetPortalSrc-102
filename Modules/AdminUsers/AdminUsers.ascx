<%@ Control Language="c#" AutoEventWireup="false" Inherits="Portal.Modules.AdminUsers.AdminUsers" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="AdminUsers.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UserList" Src="UserList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserEdit" Src="UserEdit.ascx" %>
<uc1:UserList id="ctrlUserList" runat="server"></uc1:UserList>
<uc1:UserEdit id="ctrlUserEdit" runat="server" visible="false"></uc1:UserEdit>
