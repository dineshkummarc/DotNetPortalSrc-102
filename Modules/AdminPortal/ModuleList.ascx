<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ModuleList.ascx.cs" Inherits="Portal.Modules.AdminPortal.ModuleList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="ModuleTitle" id="divTitle" runat="server" style="border-bottom: solid 1px black;"></div>
<asp:LinkButton ID="lnkAddModule" Runat="server" OnClick="OnAddModule" CssClass="LinkButton">Add Module</asp:LinkButton>
<asp:datagrid id="gridModules" runat="server" AutogenerateColumns="false" Width="100%">
	<HeaderStyle CssClass="ListHeader"></HeaderStyle>
	<ItemStyle CssClass="ListLine"></ItemStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkModule" 
					OnCommand="OnEditModule" 
					CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Reference") %>' >
					<img src="Images/Edit.gif" alt="Edit">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkUp" 
					OnCommand="OnModuleUp" 
					CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/up.gif" alt="Up">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkDown" 
					OnCommand="OnModuleDown" 
					CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/down.gif" alt="Down">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Title" HeaderText="Title" />
		<asp:BoundColumn DataField="Reference" HeaderText="Reference" />
		<asp:BoundColumn DataField="ModuleType" HeaderText="Type" />
	</Columns>
</asp:datagrid>
<br>