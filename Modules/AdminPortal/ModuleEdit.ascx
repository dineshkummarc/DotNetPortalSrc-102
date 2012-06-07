<%@ Register TagPrefix="uc1" TagName="Roles" Src="Roles.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ModuleEdit.ascx.cs" Inherits="Portal.Modules.AdminPortal.ModuleEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="ModuleTitle" style="BORDER-BOTTOM: black 1px solid">Module Data</div>
<asp:LinkButton ID="lnkSave" Runat="server" CssClass="LinkButton" OnClick="OnSave" CausesValidation="True">Save</asp:LinkButton>
|
<asp:LinkButton ID="lnkCancel" Runat="server" CssClass="LinkButton" OnClick="OnCancel" CausesValidation="False">Cancel</asp:LinkButton>
|
<asp:LinkButton ID="lnkDelete" Runat="server" CssClass="LinkButton" OnClick="OnDelete" CausesValidation="False">Delete</asp:LinkButton>
<br>
<br>
<asp:ValidationSummary EnableClientScript="False" ID="validation" Runat="server"></asp:ValidationSummary>
<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td valign="top">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="Label">Title</td>
					<td class="Data"><asp:TextBox ID="txtTitle" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="Label" nowrap>Reference<asp:RequiredFieldValidator EnableClientScript="False" ID="validator1" Runat="server" ControlToValidate="txtReference" ErrorMessage="Reference is required">*</asp:RequiredFieldValidator>
					</td>
					<td class="Data"><asp:TextBox ID="txtReference" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="Label">Type<asp:CustomValidator ID="validator2" Runat="server" OnServerValidate="OnValidateCBType" EnableClientScript="False" ErrorMessage="Type is required">*</asp:CustomValidator>
					</td>
					<td class="Data"><asp:DropDownList ID="cbType" Runat="server"></asp:DropDownList></td>
				</tr>
			</table>
		</td>
		<td width="20">&nbsp;</td>
		<td>
			<uc1:Roles id="RolesCtrl" runat="server"></uc1:Roles>
		</td>
	</tr>
</table>
<br>
