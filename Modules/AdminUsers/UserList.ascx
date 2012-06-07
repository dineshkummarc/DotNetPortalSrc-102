<%@ Import namespace="System.Data" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UserList.ascx.cs" Inherits="Portal.Modules.AdminUsers.UserList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<asp:LinkButton CssClass="LinkButton" ID="lnkAddUser" Runat="server" OnClick="OnAddUser">Add User</asp:LinkButton>
<asp:datagrid id="gridUsers" runat="server" AutogenerateColumns="false" OnItemCommand="Grid_CartCommand" Width="100%">
	<HeaderStyle CssClass="ListHeader"></HeaderStyle>
	<ItemStyle CssClass="ListLine"></ItemStyle>
	<Columns>
		<asp:ButtonColumn ButtonType="LinkButton" Text="<img src='Images/Edit.gif' alt='Edit'>" CommandName="Edit" HeaderStyle-Width="16px" HeaderText="&nbsp;" ItemStyle-CssClass="LinkButton" />
		<asp:BoundColumn DataField="login" HeaderText="Account" HeaderStyle-Width="100px" />
		<asp:BoundColumn DataField="firstName" HeaderText="First Name" HeaderStyle-Width="200px" />
		<asp:BoundColumn DataField="surName" HeaderText="Sur Name" HeaderStyle-Width="200px" />
		<asp:TemplateColumn HeaderText="Roles">
			<ItemTemplate>
				<asp:Label Runat="server" ID="Label">
					<%# GetRoles((DataRowView)Container.DataItem) %>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
