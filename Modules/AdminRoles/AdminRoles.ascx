<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdminRoles.ascx.cs" Inherits="Portal.Modules.AdminRoles.AdminRoles" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<div id="msgDeleteAdmin" class="Error" runat="server">You cannot delete the Admin Role</div>
<asp:TextBox ID="txtNewRole" Runat="server"></asp:TextBox>
<asp:LinkButton CssClass="LinkButton" ID="lnkAddRole" Runat="server" OnClick="OnAddRole">Add Role</asp:LinkButton>
<asp:datagrid id="gridRoles" runat="server" AutogenerateColumns="false" OnItemCommand="Grid_CartCommand">
	<HeaderStyle CssClass="ListHeader"></HeaderStyle>
	<ItemStyle CssClass="ListLine"></ItemStyle>
	<Columns>
		<asp:ButtonColumn ButtonType="LinkButton" Text="<img src='images/Delete.gif' alt='Delete'>" CommandName="Delete" HeaderStyle-Width="1px" ItemStyle-CssClass="LinkButton" />
		<asp:BoundColumn DataField="name" HeaderText="Name" HeaderStyle-Width="200px" />
	</Columns>
</asp:datagrid>
