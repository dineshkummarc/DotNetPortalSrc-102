<%@ Register TagPrefix="uc1" TagName="PortalTab" Src="PortalTab.ascx" %>
<%@ Page Language="c#" codebehind="RenderFrame.aspx.cs" autoeventwireup="false" Inherits="Portal.RenderFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link rel="stylesheet" href="Portal.css" type="text/css">
	</HEAD>
	<body>
		<form runat="server" enctype="multipart/form-data">
			<uc1:PortalTab id="PortalTab" runat="server"></uc1:PortalTab>
		</form>
	</body>
</HTML>
