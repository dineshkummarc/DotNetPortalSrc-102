<%@ Page language="c#" Codebehind="EditModuleTable.aspx.cs" AutoEventWireup="false" Inherits="Portal.EditModuleTable" %>
<%@ Register TagPrefix="portal" TagName="PortalHeader" Src="PortalHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModuleEdit" Src="Modules/AdminPortal/ModuleEdit.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EditModuleTable</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="Portal.css" type="text/css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="EditModuleTable" method="post" runat="server">
			<table border="0" width="100%" cellpadding="0" cellspacing="0" height="100%">
				<tr>
					<td width="100%">
						<portal:PortalHeader id="header" runat="server"></portal:PortalHeader>
					</td>
				</tr>
				<tr valign="top" height="100%">
					<td>
						<table width="100%" cellpadding="0" cellspacing="0" class="PortalEditTab">
							<tr valign="top">
								<td width="100">&nbsp;</td>
								<td id="tdEdit" runat="server">
									<br>
									<uc1:ModuleEdit id="ModuleEditCtrl" runat="server" OnSave="OnSave" OnCancel="OnCancel" OnDelete="OnDelete"></uc1:ModuleEdit>
								</td>
								<td width="100">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
