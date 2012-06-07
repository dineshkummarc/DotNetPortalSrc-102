<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UserEdit.ascx.cs" Inherits="Portal.Modules.AdminUsers.UserEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="System.Data" %>
<br>
<asp:LinkButton CssClass="LinkButton" ID="lnkBack" OnClick="OnBack" Runat="server">Back</asp:LinkButton>
|
<asp:LinkButton CssClass="LinkButton" ID="lnkSave" OnClick="OnSave" Runat="server">Save</asp:LinkButton>
|
<asp:LinkButton CssClass="LinkButton" ID="lnkDelete" OnClick="OnDelete" Runat="server">Delete</asp:LinkButton>
<br>
<br>
<table width="400px">
	<tr>
		<td class="Label" width="200">Account</td>
		<td class="Data"><asp:TextBox Width="100%" ID="txtLogin" Runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td class="Label">Password</td>
		<td class="Data"><asp:TextBox Width="100%" ID="txtPassword" Runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td class="Label" nowrap>First name</td>
		<td class="Data"><asp:TextBox Width="100%" ID="txtFirstName" Runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td class="Label" nowrap>Sur name</td>
		<td class="Data"><asp:TextBox Width="100%" ID="txtSurName" Runat="server"></asp:TextBox></td>
	</tr>
</table>
<br>
<asp:datagrid id="gridRoles" runat="server" AutogenerateColumns="false" Width="400px">
	<HeaderStyle CssClass="ListHeader"></HeaderStyle>
	<ItemStyle CssClass="ListLine"></ItemStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="S">
			<ItemTemplate>
				<asp:CheckBox Runat="server" ID="chkRole" Checked=<%#HasRole((DataRowView)Container.DataItem)%> >					
				</asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="name" HeaderText="Role" HeaderStyle-Width="100%" />
	</Columns>
</asp:datagrid>

