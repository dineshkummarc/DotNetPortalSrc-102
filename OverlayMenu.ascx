<%@ Control Language="c#" AutoEventWireup="false" Codebehind="OverlayMenu.ascx.cs" Inherits="Portal.OverlayMenu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="MenuRoot" runat="server" class="OverlayMenuRoot">
	<tr>
		<td><a href="javascript:OpenOverlayMenu(document.all.<%=Menu.ClientID%>, document.all.<%=MenuRoot.ClientID%>)"><%=RootText%></a></td>
	</tr>
</table>
<table id="Menu" runat="server" style="display: none; position: absolute;" class="OverlayMenu">
	<tr>
		<td>
			<table>
				<asp:Repeater id="MenuRepeater" Runat="server">
					<ItemTemplate>
						<tr>
							<td>
								<table 
									width="100%"
									cellpadding="0"
									cellspacing="0"
									runat="server"
									visible='<%# Container.DataItem as Portal.OverlayMenuSeparatorItem == null %>'
									class="OverlayMenuItem"
									onmouseover="javascript:OverlayMenuOnMouseOver(this)" 
									onmouseout="javascript:OverlayMenuOnMouseOut(this)"									
									onclick='<%# Page.GetPostBackClientHyperlink(this, DataBinder.Eval(Container.DataItem, "MenuItemIndex").ToString() ) %>'>
									<tr>
										<td width="16px"><img src='<%# DataBinder.Eval(Container.DataItem, "Icon") %>' ></td>
										<td width="5px">&nbsp;</td>
										<td nowrap width="100%"><%# DataBinder.Eval(Container.DataItem, "Text") %></td>
									</tr>
								</table>
								<table 
									cellpadding="0"
									cellspacing="0"
									width="100%"
									runat="server"
									visible='<%# Container.DataItem as Portal.OverlayMenuSeparatorItem != null %>'>
									<TR>
										<TD colspan="3">
											<hr class="OverlayMenuSeparator">
										</TD>
									</TR>
								</table>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</td>
	</tr>
</table>
<script language="javascript">
	function OverlayMenuOnMouseOver(ctrl)
	{
		ctrl.style.color = '#ffffff';
		ctrl.style.background = '#333399';
	}
	function OverlayMenuOnMouseOut(ctrl)
	{
		ctrl.style.color = '#000000';
		ctrl.style.background = '#ffffff';
	}

	function getLeft(l)
	{
		if (l.offsetParent) return (l.offsetLeft + getLeft(l.offsetParent));
		else return (l.offsetLeft);
	}
	function getTop(l)
	{
		if (l.offsetParent) return (l.offsetTop + getTop(l.offsetParent));
		else return (l.offsetTop);
	}

	function CloseOverlayMenu(ctrl)
	{
		ctrl.style.display = 'none';
	}
	function CloseAllOverlayMenu()
	{
		for(i=0;i<document.all.length;i++)
		{
			if(document.all.item(i).className == 'OverlayMenu')
			{
				CloseOverlayMenu(document.all.item(i))
			}
		}
	}	
	function OpenOverlayMenu(ctrl, root)
	{
		if(ctrl.style.display == 'block') 
		{
			CloseOverlayMenu(ctrl);
			return;
		}
		
		CloseAllOverlayMenu();

		ctrl.style.display = 'block';
		ctrl.style.left = getLeft(root) - ctrl.clientWidth - 4;
		ctrl.style.top = getTop(root);
	}
</script>
