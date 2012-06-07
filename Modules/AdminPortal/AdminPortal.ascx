<%@ Register TagPrefix="iiuga" Namespace="iiuga.Web.UI" Assembly="TreeWebControl" %>
<%@ Register TagPrefix="uc1" TagName="Tab" Src="Tab.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TabList" Src="TabList.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdminPortal.ascx.cs" Inherits="Portal.Modules.AdminPortal.AdminPortal" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<tr>
		<td width="1" valign="top">
			<iiuga:treeweb id="tree" runat="server" CollapsedElementImage="Images/plus.jpg" ExpandedElementImage="Images/minus.jpg">
				<ImageList>
					<iiuga:ElementImage ImageURL="Images/Bullet.gif" />
				</ImageList>
				<Elements>
					<iiuga:treeelement text="Portal" CssClass="" />
				</Elements>
			</iiuga:treeweb>
		</td>
		<td width="40">&nbsp;</td>
		<td valign="top">
			<uc1:Tab id="TabCtrl" runat="server"></uc1:Tab>
			<!--			<hr> -->
			<uc1:TabList id="TabListCtrl" runat="server" OnSave="OnSave" OnCancel="OnCancel" OnDelete="OnDelete"></uc1:TabList>
		</td>
	</tr>
</table>
