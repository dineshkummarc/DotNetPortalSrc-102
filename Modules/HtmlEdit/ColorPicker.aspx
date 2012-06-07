<%@ Page language="c#" Codebehind="ColorPicker.aspx.cs" AutoEventWireup="false" Inherits="CWebRun.Sample.ColorPicker" %>
<%@ Register TagPrefix="cc1" Namespace="CWebRun.ColorPicker" Assembly="HtmlEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ColorPicker</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="../../Portal.css" type="text/css">
	</HEAD>
	<body>
		<form id="ColorPicker" method="post" runat="server">
			<cc1:ColorPicker id="ColorPicker1" runat="server"></cc1:ColorPicker>
		</form>
	</body>
</HTML>
