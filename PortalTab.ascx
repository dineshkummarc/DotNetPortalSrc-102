<%@ Register TagPrefix="ucm" TagName="OVM" Src="OverlayMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TabPath" Src="TabPath.ascx" %>
<%@ Control Language="c#" autoeventwireup="false" codebehind="PortalTab.ascx.cs" Inherits="Portal.PortalTab" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" border="0" class="PortalTab" cellpadding="0" cellspacing="0">
	<tbody>
		<tr>
			<td colspan="5" class="TabPath">
				<uc1:TabPath id="TabPathCtrl" runat="server"></uc1:TabPath>
			</td>
		</tr>
		<tr>
			<td colspan="5">
				<table width="100%">
					<tr>
						<td width="100%" align="right">
							<ucm:OVM id="ovm" runat="server" RootText="Edit Tab">
								<MenuItem Text="Add Tab" Icon="Images/new.gif" OnClick="OnAddTab"></MenuItem>
								<MenuItem Text="Edit Tab" Icon="Images/edit.gif" OnClick="OnEditTab"></MenuItem>
								<MenuItem Text="Delete Tab" Icon="Images/delete.gif" OnClick="OnDeleteTab"></MenuItem>
								<SeparatorItem></SeparatorItem>
								<MenuItem Text="Add left Module" Icon="Images/new.gif" OnClick="OnAddLeftModule"></MenuItem>
								<MenuItem Text="Add middle Module" Icon="Images/new.gif" OnClick="OnAddMiddleModule"></MenuItem>
								<MenuItem Text="Add right Module" Icon="Images/new.gif" OnClick="OnAddRightModule"></MenuItem>
							</ucm:OVM>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr height="100%">
			<td id="left" valign="top" width="200" runat="server">
			</td>
			<td width="20"></td>
			<td id="middle" valign="top" width="*" runat="server">
			</td>
			<td width="20"></td>
			<td id="right" valign="top" width="200" runat="server">
			</td>
		</tr>
	</tbody>
</table>
