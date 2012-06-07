<%@ Control Language="c#" AutoEventWireup="true" Inherits="Portal.API.Module" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script runat="server">

	public override bool IsVisible()
	{
		return !Page.User.Identity.IsAuthenticated;
	}

	void OnLogin(object sender, EventArgs args)
	{
		if(Portal.UserManagement.Login(account.Text, password.Text))
		{
			Response.Redirect(Request.RawUrl);
		}
		else
		{
			lError.Text = "Invalid Login";
		}
	}
</script>
<table width="100%">
	<tr>
		<td><b>Account</b></td>
	</tr>
	<tr>
		<td><asp:TextBox Width="100%" ID="account" Runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td><b>Password</b></td>
	</tr>
	<tr>
		<td><asp:TextBox width="100%" ID="password" Runat="server" TextMode="Password" onkeydown="javascript:CheckEnterKey()"></asp:TextBox></td>
	</tr>
</table>
<div><asp:Label ID=lError Runat=server CssClass="Error"></asp:Label></div>
<asp:LinkButton Runat="server" CssClass="LinkButton" 
	ID="lnkLogin" OnClick="OnLogin">Login</asp:LinkButton>
<script language=javascript>
	document.all.<%=account.ClientID%>.focus();
	
	function CheckEnterKey()
	{
		if(window.event.keyCode == 13)
		{
			<%= Page.GetPostBackClientHyperlink(lnkLogin, "") %>
		}
	}
</script>