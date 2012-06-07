<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TabList.ascx.cs" Inherits="Portal.Modules.TabList.TabList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<asp:Repeater ID="Tabs" Runat="server">
		<ItemTemplate>
			<tr>
				<td width="1px">
					<img src="Images/Bullet.gif">
				</td>
				<td>
					<asp:HyperLink Runat="server" 
						CssClass="LinkButton"
						NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "URL")%>'>
						<%# DataBinder.Eval(Container.DataItem, "Text") %>
					</asp:HyperLink>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
