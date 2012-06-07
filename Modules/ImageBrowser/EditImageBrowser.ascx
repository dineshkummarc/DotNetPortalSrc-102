<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EditImageBrowser.ascx.cs" Inherits="ImageBrowser.Controls.EditImageBrowser" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:LinkButton ID="lnkSave" Runat="server">Save</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton id="lnkCancel" runat="server">Cancel</asp:LinkButton>
<table width="100%">
	<colgroup>
		<col class="Label" width="100">
		<col class="Data">
	</colgroup>
	<tr>
		<td nowrap>Picture&nbsp;VirtualDirectory</td>
		<td><asp:TextBox ID="txtPictureVirtualDirectory" Runat="server" Width="100%"></asp:TextBox></td>
	</tr>
	<tr>
		<td nowrap>Picture&nbsp;RootName</td>
		<td><asp:TextBox ID="txtRootName" Runat="server" Width="100%"></asp:TextBox></td>
	</tr>
</table>
