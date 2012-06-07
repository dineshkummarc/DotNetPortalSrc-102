<%@ Page Language="c#" CodeBehind="HeaderFrame.aspx.cs" AutoEventWireup="false" Inherits="Portal.HeaderFrame" %>
<%@ Register TagPrefix="uc0" TagName="PortalHeader" Src="PortalHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link rel="stylesheet" href="Portal.css" type="text/css">
	</head>
	<body>
		<form id="HeaderFrame" method="post" runat="server">
			<uc0:PortalHeader id="UserControl1" runat="server"></uc0:PortalHeader>
		</form>
	</body>
</html>
