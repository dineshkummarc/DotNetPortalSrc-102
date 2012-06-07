<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Roles.ascx.cs" Inherits="Portal.Modules.AdminPortal.Roles" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellspacing="0" rules="all" border="1" style="border-collapse:collapse;">
	<tr>
		<td class="ListHeader">&nbsp;</td>
		<td id="tdHeaderRoleType" runat="server" class="ListHeader" width="100px">Role Type</td>
		<td class="ListHeader" width="100px">Role</td>
	</tr>
	<asp:Repeater id="gridRoles" runat="server" OnItemDataBound="OnDataBind">
		<ItemTemplate>
			<tr>
				<td class="ListLine">
					<asp:LinkButton ID="lnkDelete" Runat="server" OnCommand="OnDelete" CausesValidation="False"><img src="Images/delete.gif" alt="Delete"></asp:LinkButton>
				</td>
				<td class="ListLine" id="tdRoleType" runat="server">
					<asp:Label Runat="server" ID="lRoleType">
					</asp:Label>
				</td>
				<td class="ListLine">
					<asp:Label Runat="server" ID="lRole"></asp:Label>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<tr>
		<td><asp:LinkButton ID="lnkAddRole" OnClick="OnAddRole" Runat="server" CausesValidation="False"><img src="Images/new.gif" alt="Add"></asp:LinkButton></td>
		<td class="ListLine" id="tdAddRoleType" runat="server">
			<asp:DropDownList Runat="server" ID="cbAddRoleType" Width="100%">
				<asp:ListItem Value=""></asp:ListItem>
				<asp:ListItem Value="view">View</asp:ListItem>
				<asp:ListItem Value="edit">Edit</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td class="ListLine" >
			<asp:DropDownList Runat="server" ID="cbAddRole" DataTextField="name" DataValueField="name" Width="100%"></asp:DropDownList>
		</td>
	</tr>
</table>
