<%@ Control Language="c#" AutoEventWireup="true" Inherits="Portal.API.Module" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="System.IO" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="System.Xml" %>
<script runat="server">

void Page_Load(Object sender, EventArgs e) 
{
	// Schema exists - cannot be null!
	DataSet ds = ReadConfig();

	DateTime t = DateTime.Today;
	string today = t.Month + "/" + t.Day + "/" + t.Year;
	news.DataSource = ds.Tables["news"].Select("Expires > #" + today + "#", "Created DESC");
	news.DataBind();
}
</script>
<table width="100%">
	<asp:Repeater id="news" runat="server">
		<ItemTemplate>
			<tr>
				<td width="1px"><img src="images/bullet.gif" alt="bullet"></td>
				<td nowrap>
					<span class="Emph">
						<%# ((DateTime)((DataRow)Container.DataItem)["Created"]).ToShortDateString() %>
					</span>
				</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<%# ((DataRow)Container.DataItem)["Text"] %>
				</td>
			</tr>			
		</ItemTemplate>
	</asp:Repeater>
</table>