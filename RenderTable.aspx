<%@ Page Language="c#" codebehind="RenderTable.aspx.cs" autoeventwireup="false" Inherits="Portal.RenderTable" %>
<%@ Register TagPrefix="portal" TagName="PortalHeader" Src="PortalHeader.ascx" %>
<%@ Register TagPrefix="portal" TagName="PortalTab" Src="PortalTab.ascx" %>
<%@ Register TagPrefix="portal" TagName="PortalFooter" Src="PortalFooter.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>.NET Portal</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link rel="stylesheet" href="Portal.css" type="text/css">
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="RenderTable" method="post" runat="server" enctype="multipart/form-data">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="100%">
							<portal:PortalHeader id="header" runat="server"></portal:PortalHeader>
						</td>
					</tr>
					<tr height="100%" valign="top">
						<td>
							<portal:PortalTab id="tab" runat="server"></portal:PortalTab>
						</td>
					</tr>
					<tr>
						<td>
							<portal:PortalFooter id="PortalFooter1" runat="server"></portal:PortalFooter>
						</td>
					</tr>
				</tbody>
			</table>
		</form>
	</body>
</HTML>
