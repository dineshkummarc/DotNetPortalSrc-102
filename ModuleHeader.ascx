<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ModuleHeader.ascx.cs" Inherits="Portal.ModuleHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="portal" assembly="Portal" Namespace="Portal" %>
<%@ Register TagPrefix="ucm" TagName="OVM" Src="OverlayMenu.ascx" %>
<table style="BORDER-BOTTOM: black 1px solid" width="100%">
	<tr>
		<td width="100%">
			<span class="ModuleTitle">
				<%= ModuleDef.title %>
			</span>
		</td>
		<td id="tdEdit" runat="server">
			<portal:EditLink Class="LinkButton" id="lnkEditLink" runat="server" Text="Edit"></portal:EditLink>
			<ucm:OVM id="ovm" runat="server" RootText="Edit">
				<MenuItem Text="Edit Content" Icon="Images/edit.gif" OnClick="OnEditContent"></MenuItem>
				<MenuItem Text="Edit Module" Icon="Images/edit.gif" OnClick="OnEditModule"></MenuItem>
				<SeparatorItem></SeparatorItem>
				<MenuItem Text="Move Up" Icon="Images/up.gif" OnClick="OnMoveUp"></MenuItem>
				<MenuItem Text="Move Down" Icon="Images/down.gif" OnClick="OnMoveDown"></MenuItem>
				<MenuItem Text="Move Left" Icon="Images/left.gif" OnClick="OnMoveLeft"></MenuItem>
				<MenuItem Text="Move Right" Icon="Images/right.gif" OnClick="OnMoveRight"></MenuItem>
			</ucm:OVM>
		</td>
	</tr>
</table>
