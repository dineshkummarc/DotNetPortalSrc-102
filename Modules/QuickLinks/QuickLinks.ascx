<%@ Control Language="c#" AutoEventWireup="true" Inherits="Portal.API.Module" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="System.IO" %>
<%@ Import namespace="System.Data" %>
<script runat="server">

void Page_Load(Object sender, EventArgs e) 
{
	// Schema exists - cannot be null!
	DataSet ds = ReadConfig();

	links.DataSource = ds.Tables["links"].Select("1=1", "Position");
	links.DataBind();
}
</script>
<table width="100%">
	<asp:Repeater id="links" runat="server">
		<ItemTemplate>
			<tr>
				<td width="1px"><img src="images/bullet.gif" alt="bullet"></td>
				<td nowrap>
					<a 
						href="<%# ((DataRow)Container.DataItem)["URL"] %>"
						class="LinkButton"
					><%# ((DataRow)Container.DataItem)["Text"] %></a>
				</a></td>
			</tr>			
		</ItemTemplate>
	</asp:Repeater>
</table>