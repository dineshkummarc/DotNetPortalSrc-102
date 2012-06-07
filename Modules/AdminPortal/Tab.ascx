<%@ Register TagPrefix="uc1" TagName="ModuleList" Src="ModuleList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Module" Src="ModuleEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Roles" Src="Roles.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Tab.ascx.cs" Inherits="Portal.Modules.AdminPortal.Tab" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="ModuleTitle" style="BORDER-BOTTOM: black 1px solid">Tab Data</div>
<asp:LinkButton ID="lnkSave" Runat="server" CssClass="LinkButton" OnClick="OnSave">Save</asp:LinkButton>
|
<asp:LinkButton ID="lnkCancel" Runat="server" CssClass="LinkButton" OnClick="OnCancel">Cancel</asp:LinkButton>
|
<asp:LinkButton ID="lnkDelete" Runat="server" CssClass="LinkButton" OnClick="OnDelete">Delete</asp:LinkButton>
<br>
<br>
<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td width="50%" valign="top">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="Label">Title</td>
					<td class="Data"><asp:TextBox ID="txtTitle" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="Label">Reference</td>
					<td class="Data"><asp:TextBox ID="txtReference" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
			</table>
		</td>
		<td width="20">&nbsp;</td>
		<td>
			<uc1:Roles id="RolesCtrl" runat="server" ShowRoleType="False"></uc1:Roles>
		</td>
	</tr>
</table>
<uc1:Module id="ModuleEditCtrl" runat="server" Visible="False" OnCancel="OnCancelEditModule" OnDelete="OnDeleteModule" OnSave="OnSaveModule"></uc1:Module>
<uc1:ModuleList id="ModuleListCtrl_Left" runat="server" Title="Modules Left"></uc1:ModuleList>
<uc1:ModuleList id="ModuleListCtrl_Middle" runat="server" Title="Modules Middle"></uc1:ModuleList>
<uc1:ModuleList id="ModuleListCtrl_Right" runat="server" Title="Modules Right"></uc1:ModuleList>
