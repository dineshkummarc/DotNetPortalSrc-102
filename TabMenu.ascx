<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TabMenu.ascx.cs" Inherits="Portal.TabMenu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%" class="TabMenu" cellspacing=0 cellpadding=0>
	<tr>
		<asp:Repeater ID=Tabs Runat=server>
			<ItemTemplate>
				<td	class="<%# ((bool)DataBinder.Eval(Container.DataItem, "CurrentTab"))?"TabMenu_CurrentTab":"TabMenu_Tab" %>"	>
					<a 
						href="<%# DataBinder.Eval(Container.DataItem, "URL") %>"
						class="<%# ((bool)DataBinder.Eval(Container.DataItem, "CurrentTab"))?"TabMenu_CurrentLink":"TabMenu_Link" %>"
					><%# ((string)DataBinder.Eval(Container.DataItem, "Text")).Replace(" ", "&nbsp;") %></a>
				</td>
			</ItemTemplate>
		</asp:Repeater>
		<td width="100%"></td>
	</tr>
</table>
<script language="javascript">
	//<!--
	function SelectTab(tabRef)
	{
		window.open('RenderFrame.aspx?TabRef=' + tabRef, 'main');
		navigate('HeaderFrame.aspx?TabRef=' + tabRef);
	}
	//-->
</script>

