<%@ Control Language="c#" autoeventwireup="false" Inherits="HtmlEdit.EditHtml" CodeBehind="EditHtmlEdit.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="CWebRun.Editor" Assembly="HtmlEdit" %>
<cc1:YAHENet 
	id="YAHENet1" runat="server" 
	BoldImage="images/Bold.GIF" EditorBackGround="Gainsboro" 
	ContentEditorBackground="White" ImageBackground="Gainsboro" ImageBorderColor="Gainsboro" 
	MouseDownColor="#EEEEEE" MouseOverColor="#CCCCCC" ColorPickerURL="Modules/HtmlEdit/ColorPicker.aspx" 
	TableEditor="Modules/HtmlEdit/TableEditor.aspx" FileManager="Modules/HtmlEdit/FileManager.aspx"
	OnEditorHTMLSaved="OnSave"></cc1:YAHENet><BR>
