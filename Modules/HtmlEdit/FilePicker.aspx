<%@ Page Language="c#" CodeBehind="FilePicker.aspx.cs" AutoEventWireup="false" EnableViewState="false" Inherits="AWS.FilePickerPage" clientTarget="Uplevel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>Select a File</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link rel="stylesheet" href="../../Portal.css" type="text/css">
		<style>
	.inputArea { FONT-SIZE: 12px; COLOR: #000000; FONT-FAMILY: tahoma; HEIGHT: 20px }
		</style>
	</HEAD>
	<body>
		<form runat="server" ID="Form1" enctype="multipart/form-data">
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td vAlign="top" colspan="2" width="100%" BackColor="#000066">
						<img src="images/icon.gif" Align="left" hSpace="0">
						<asp:label id="Header" style="TEXT-ALIGN: center" runat="server" Height="20px" ForeColor="#ffffff" font-name="Arial" font-size="12px" Width="100%" BackColor="#000066"></asp:label>
						<asp:imagebutton id="UpBtn" runat="server" ToolTip="Up one level" ImageUrl="images/up.gif" CommandName="ChangePath" CommandArgument="../"></asp:imagebutton><asp:imagebutton id="GoRoot" runat="server" ImageUrl="images/home.gif" CommandName="ChangePath" CommandArgument="/" AlternateText="Back to Root Directory"></asp:imagebutton>
					</td>
				</tr>
				<tr>
					<td align="left" vAlign="top" colspan="2">
						<asp:DataGrid id="dgFSList" runat="server" BorderStyle="None" BorderWidth="1px" BorderColor="#666666" BackColor="White" CellPadding="4" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" PagerStyle-HorizontalAlign="Right" PagerStyle-NextPageText="Next >>" PagerStyle-PrevPageText="<< Prev" HorizontalAlign="Center">
							<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
							<ItemStyle Font-Size="12px" Font-Names="Tahoma" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="13px" Font-Names="Arial" Font-Bold="True" ForeColor="#FFCC00" BackColor="#000099"></HeaderStyle>
							<FooterStyle ForeColor="#FFCC00" BackColor="#000099"></FooterStyle>
							<Columns>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:Image runat="server" id="imgFSObject" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="File Name">
									<ItemTemplate>
										<asp:LinkButton runat="server" id="lbFileName" CommandName="FSOClick" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Size">
									<ItemTemplate>
										<asp:Label runat="server" id="lblSize" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Modified">
									<ItemTemplate>
										<asp:Label runat="server" id="lblModified" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Action">
									<ItemTemplate>
										<asp:HyperLink runat="server" id="hlView" ImageUrl="images/view.gif" ToolTip="View" Target="_Blank" />
										<asp:LinkButton CommandName="Del" runat="server" id="imbDel" Text="<img src='/images/del.gif' border='0' align='center'/>" ToolTip="Delete" Visible="False" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="Next &gt;&gt;" Font-Size="13px" Font-Names="Arial" Font-Bold="True" PrevPageText="&lt;&lt; Prev" HorizontalAlign="Center" ForeColor="#FFCC00" BackColor="#000099" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
					</td>
				</tr>
				<asp:PlaceHolder runat="server" Id="UploadArea" Visible="false">
					<TR>
						<TD bgColor="#000066" height="30"><IMG src="images/CreateFolder.gif" align="left"><FONT face="Arial" color="#ffffff" size="2">Upload 
								a file</FONT></TD>
						<TD bgColor="#000066" height="30">
							<INPUT class="inputArea" id="fileToUpload" type="file" size="15" name="fileToUpload" runat="server">
							<asp:button id="UploadBtn" runat="server" Text="Upload"></asp:button></TD>
					</TR>
				</asp:PlaceHolder>
				<asp:PlaceHolder runat="server" Id="CreateDirArea" Visible="false">
					<TR>
						<TD bgColor="#000066" height="30"><IMG src="images/CreateFolder.gif" align="left"><FONT face="Arial" color="#ffffff" size="2">Create 
								new folder</FONT></TD>
						<TD bgColor="#000066" height="30">
							<asp:textbox class="inputArea" id="NewFolderText" runat="server" Size="15"></asp:textbox>&nbsp;
							<asp:button id="NewFolderBtn" runat="server" Text="New Folder"></asp:button></TD>
					</TR>
				</asp:PlaceHolder>
			</table>
		</form>
	</body>
</HTML>
