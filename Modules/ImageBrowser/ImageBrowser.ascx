<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ImageBrowser.ascx.cs" Inherits="ImageBrowser.Controls.ImageBrowser" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:panel id="ImageBrowserPanel" runat="server"></asp:panel>
<asp:Panel ID="ImageViewPanel" Runat="server">
	<br>
	<div align="center">
		<asp:HyperLink id="image" runat="server" Target="_blank">image</asp:HyperLink>
	</div>
	<div align="center">
		<asp:Label ID="lbImageText" Runat="server"></asp:Label>
	</div>
	<asp:TextBox ID="txtImageText" Runat="server" Width="100%" Height="100px" TextMode="MultiLine"></asp:TextBox>
	<asp:HyperLink ID="lnkSaveImageText" Runat="server">Save</asp:HyperLink>&nbsp;|&nbsp;<asp:HyperLink ID="lnkSetFolderImage" Runat="server">Set as Folder Image</asp:HyperLink>
	<div style="border-bottom: solid 1px black; width: 100%">&nbsp;
	</div>
	<a href="<%=back%>">Back</a>
</asp:Panel>
