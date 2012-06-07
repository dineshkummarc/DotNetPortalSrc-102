<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ModuleFailed.ascx.cs" Inherits="Portal.ModuleFailed" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%" style="BORDER: red 3px solid; color: red;">
	<tr>
		<td style="font-weight:bold;">Error loading Module!</td>
	</tr>
	<tr>
		<td><asp:Label ID="lbMessage" Runat="server"></asp:Label></td>
	</tr>
</table>