<%@ Page language="c#" Codebehind="FileManager.aspx.cs" AutoEventWireup="false" Inherits="CWebRun.FileManager.FileManager" %>
<%@ Register TagPrefix="cc1" Namespace="AWS" Assembly="HtmlEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FileManager</title>
		<LINK href="http://localhost/portal.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			function onButtonClick()
			{
				var Width = document.FileManager.Width.value;
				var Height = document.FileManager.Height.value;
				var AltText = document.FileManager.AltText.value;
				var src = FileManager.FilePicker1.value;
				var HSpace = document.FileManager.HSpace.value;
				var Align = FileManager.Align.value;
				var VSpace = FileManager.VSpace.value;
				var Border = FileManager.Border.value;
				
				var imageTag = "src=\"Modules/HtmlEdit/" + src + "\" ";
				
				if(Width!==""){
					imageTag += "width='" + Width + "' ";
				} 
				if(Height!=""){
					imageTag += "height='" + Height + "' "; 
				}
				if(AltText!==""){
					imageTag += "alt='" + AltText + "' "; 
				}
				if(HSpace!==""){ 
					imageTag += "hspace='" + HSpace + "' "; 
				}
				if(Align!=""){
					imageTag += "align='" + Align + "' ";
				} 
				if(VSpace!=""){
					imageTag += "Vspace='" + VSpace + "' ";
				} 
				if(Border!=""){
					imageTag += "border='" + Border + "' ";
				}
				 
				opener.imageURL = imageTag;
				opener.InsertNewImage();
				window.close();
			}
			
			function onUpdateClick()
			{
				var Width = document.FileManager.Width.value;
				var Height = document.FileManager.Height.value;
				var AltText = document.FileManager.AltText.value;
				var src = FileManager.FilePicker1.value;
				var HSpace = document.FileManager.HSpace.value;
				var Align = FileManager.Align.value;
				var VSpace = FileManager.VSpace.value;
				var Border = FileManager.Border.value;
				
				window.opener.UpdateImage(src,AltText,Align,Border,Width,Height,HSpace,VSpace);
				window.close();
			
			
			}
			
			
		</script>
	</HEAD>
	<body onload="checkImage()">
		<form id="FileManager" method="post" runat="server">
			<P align="center">
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="0">
					<TR>
						<TD class="FileManager" align="middle" width="50%" colSpan="4">Image Source:
							<cc1:FilePicker id="FilePicker1" runat="server" fpAllowedUploadFileExts="gif,jpg,jpeg,png" Enabled="False" fpUploadDir="uploads/images" fpPopupURL="FilePicker.aspx" fpImageURL="images/browse.gif" fpAllowCreateDirs="False" fpAllowDelete="False"></cc1:FilePicker></TD>
					</TR>
					<TR>
						<TD class="FileManager" align="middle" colSpan="4">Alternate Text: <INPUT type="text" id="AltText"></TD>
					</TR>
					<tr>
						<TD class="FileManager" noWrap>Alignment:</TD>
						<TD class="FileManager" noWrap><SELECT name="Align">
								<OPTION value="" selected>Select</OPTION>
								<OPTION value="left">Left</OPTION>
								<OPTION value="middle">Middle</OPTION>
								<OPTION value="right">Right</OPTION>
								<OPTION value="absbottom">Absbottom</OPTION>
								<OPTION value="absmiddle">Absmiddle</OPTION>
								<OPTION value="baseline">baseline</OPTION>
								<OPTION value=""></OPTION>
							</SELECT></TD>
						<TD class="FileManager" noWrap>Border Size:</TD>
						<TD class="FileManager" noWrap>
							<INPUT id="Border" type="text" size="1" name="Border"></TD>
					</tr>
					<TR>
						<TD class="FileManager">Height:</TD>
						<TD class="FileManager">
							<INPUT id="Height" type="text" size="1" name="Height"></TD>
						<TD class="FileManager" noWrap>Horizontal Space:</TD>
						<TD class="FileManager" noWrap>
							<INPUT id="HSpace" type="text" size="1" name="HSpace"></TD>
					</TR>
					<TR>
						<TD class="FileManager">Width:
						</TD>
						<TD class="FileManager">
							<INPUT id="Width" type="text" size="1" name="Width"></TD>
						<TD class="FileManager" noWrap>Vertical Space:</TD>
						<TD class="FileManager" noWrap>
							<INPUT id="VSpace" type="text" size="1" name="VSpace"></TD>
					</TR>
				</TABLE>
				<span id="btnImgInsert" style="display:none">
					<INPUT onclick="onButtonClick();" type="button" value="Insert">
				</span>
				<span id="btnImgUpdate" style="display:none">
					<INPUT onclick="onUpdateClick();" type="button" value="Update">
				</span>
			</P>
			<P align="center">&nbsp; <INPUT id="iURL" name="IURL" style="WIDTH: 363px; HEIGHT: 22px" type="hidden" size="55" disabled readOnly></P>
		</form>
		
		
		<SCRIPT  language="javascript">
			function checkImage()
			{
			//alert (window.opener.HTMLEditor.InnerHTML);
			var oSel = window.opener.HTMLEditor.document.selection.createRange()
			var sType = window.opener.HTMLEditor.document.selection.type		
			if ((oSel.item) && (oSel.item(0).tagName=="IMG")) //If image is selected 
				{
			//	selectImage(oSel.item(0).src)
				document.all.FilePicker1.value = oSel.item(0).src
				document.all.AltText.value = oSel.item(0).alt
				document.all.Align.value = oSel.item(0).align
				document.all.Border.value = oSel.item(0).border
				document.all.Width.value = oSel.item(0).width
				document.all.Height.value = oSel.item(0).height
				document.all.HSpace.value = oSel.item(0).hspace
				document.all.VSpace.value = oSel.item(0).vspace
				document.all.btnImgUpdate.style.display="block";
				//document.all.btnImgUpdate.style.visible=false;
				}
			else
				{
				document.all.btnImgInsert.style.display="block";
				//document.all.btnImgInsert.visible=false;
				}		
			}
		</SCRIPT>
		
		
	</body>
</HTML>
