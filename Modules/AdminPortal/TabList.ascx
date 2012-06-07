<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TabList.ascx.cs" Inherits="Portal.Modules.AdminPortal.TabList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="ModuleTitle" style="border-bottom: solid 1px black;">Sub Tabs</div>
<asp:LinkButton ID="lnkAddModule" Runat="server" OnClick="OnAddTab" CssClass="LinkButton">Add Tab</asp:LinkButton>
<asp:datagrid id="Tabs" runat="server" AutogenerateColumns="false" Width="100%">
	<HeaderStyle CssClass="ListHeader"></HeaderStyle>
	<ItemStyle CssClass="ListLine"></ItemStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkTitle" 
					OnCommand="OnEditTab" 
					CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Reference") %>' >
					<img src="Images/Edit.gif" alt="Edit">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkUp" 
					OnCommand="OnTabUp" 
					CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/up.gif" alt="Up">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkDown" 
					OnCommand="OnTabDown" 
					CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/down.gif" alt="Down">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Title" HeaderText="Title" />
		<asp:BoundColumn DataField="Reference" HeaderText="Reference" />
	</Columns>
</asp:datagrid>
