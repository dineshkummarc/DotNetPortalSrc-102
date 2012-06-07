using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using System.Resources;
using System.Reflection;

//#define ALLOWVIEWHTML

namespace CWebRun.Editor
{

	[ToolboxData("<{0}:YAHENet runat=server></{0}:YAHENet>")]
	[Designer(typeof(YAHENetDesigner))]
	
	public class YAHENet : Control, INamingContainer
	{
		/// <summary>
		/// The YAHENet .Net HTML Editor Built in C#
		/// </summary>
		public class EditorEventArgs :System.EventArgs
		{
			public string EditorHTMLValue;
		}

		public delegate void EditorHTMLSavedHandler(object sender,EditorEventArgs e);
		public event EditorHTMLSavedHandler EditorHTMLSaved;
		ImageButton saveButton = new ImageButton();
		
		internal string colorPikcerURL = "/ColorPicker.aspx";
		[
		Browsable(true),
		Category("FileManager"),
		Description("The location of the File Manager"),
		Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(UITypeEditor))]
		public string ColorPickerURL
		{
			get{return colorPikcerURL;}
			set{colorPikcerURL = value;}
		}

		internal string fileManager = "/FileManager/FileManager.aspx";
		[
		Browsable(true),
		Category("FileManager"),
		Description("The location of the File Manager"),
		Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(UITypeEditor))]
		public string FileManager
		{
			get{return fileManager;}
			set{fileManager = value;}
		}
		
		// WES GRANT ADDED 01/18/03
		// Location of Window to Edit Tables
		internal string tableEditor = "TableEditor.aspx";
		[
		Browsable(true),
		Category("TableEditor"),
		Description("The location of the Table Editor"),
		Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(UITypeEditor))]
		public string TableEditor
		{
			get{return tableEditor;}
			set{tableEditor = value;}
		}
		// WES GRANT END


		internal string saveButtonImage = "images/WYSIWYG/Save.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Save Button"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]

		public string SaveButtonImage
		{
			get{return saveButtonImage;}
			set{saveButtonImage = value;}
		}

		internal string textColorButtonImage = "images/WYSIWYG/fgcolor.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Text Color Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string TextColorButtonImage
		{
			get{return textColorButtonImage;}
			set{textColorButtonImage = value;}
		}


		internal string cutButtonImage = "images/WYSIWYG/Cut.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Cut Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]

		public string CutButtonImage
		{
			get{return cutButtonImage;}
			set{cutButtonImage = value;}
		}

		internal string boldImage = "images/WYSIWYG/Bold.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Bold Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]

		public string BoldImage
		{
			get{return boldImage;}
			set {boldImage = value;}
		}

		internal string italicImage ="images/WYSIWYG/Italic.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Italic Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string ItalicImage
		{
			get{return italicImage;}
			set{italicImage = value;}
		}

		internal string underlineImage = "images/WYSIWYG/under.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Underline Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string UnderlineImage
		{
			get{return underlineImage;}
			set{underlineImage = value;}
		}

		internal string leftImage = "images/WYSIWYG/left.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Left Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]

		public string LeftImage
		{
			get{return leftImage;}
			set{leftImage = value;}
		}

		internal string centerImage = "images/WYSIWYG/center.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Center Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]

		public string CenterImage
		{
			get{return centerImage;}
			set{centerImage = value;}
		}

		internal string rightImage = "images/WYSIWYG/right.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Right Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string RightImage
		{
			get{return rightImage;}
			set{rightImage = value;}
		}

		internal string numListImage = "images/WYSIWYG/numlist.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Numbered List Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string NmListImage
		{
			get{return numListImage;}
			set{numListImage = value;}
		}

		internal string bulletListImage = "images/WYSIWYG/bullist.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Bullet List Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string BulletListImage
		{
			get{return bulletListImage;}
			set{bulletListImage = value;}
		}

		internal string indentImage = "images/WYSIWYG/inindent.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Indent Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string IndentImage
		{
			get{return indentImage;}
			set{indentImage = value;}
		}

		internal string deindentImage = "images/WYSIWYG/deindent.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Deindent Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string DeindentImage
		{
			get {return deindentImage;}
			set {deindentImage = value;}
		}

		internal string copyImage = "images/WYSIWYG/Copy.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Copy Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string CopyImage
		{
			get{return copyImage;}
			set{copyImage = value;}
		}

		internal string pasteImage = "images/WYSIWYG/Paste.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Paste Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string PasteImage
		{
			get{return pasteImage;}
			set{pasteImage = value;}
		}

		internal string undoImage = "images/WYSIWYG/undo.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Undo Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string UndoImage
		{
			get{return undoImage;}
			set{undoImage = value;}
		}

		internal string redoImage = "images/WYSIWYG/redo.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Redo Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string RedoImage
		{
			get{return redoImage;}
			set{redoImage = value;}
		}

		internal string subscriptImage = "images/WYSIWYG/subscript.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Subscript Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string SubscriptImage
		{
			get{return subscriptImage;}
			set{subscriptImage = value;}
		}

		internal string superscriptImage = "images/WYSIWYG/superscript.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Superscript Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string SuperscriptImage
		{
			get{return superscriptImage;}
			set{superscriptImage = value;}
		}

		internal string linkImage = "images/WYSIWYG/link.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Link Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string LinkImage
		{
			get{return linkImage;}
			set{linkImage = value;}
		}

		internal string hrImage = "images/WYSIWYG/hr.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Horizontal Rule Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string HrImage
		{
			get{return hrImage;}
			set{hrImage = value;}
		}

		internal string imageImage = "images/WYSIWYG/image.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Image Image"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string ImageImage
		{
			get{return imageImage;}
			set{imageImage = value;}
		}


		// WES GRANT ADDED 12/29/02
		internal string tableImage = "images/WYSIWYG/table.gif";
		[
		Browsable(true),
		Category("Images"),
		Description("The Image For the Table Button"),
		Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
		public string TableImage
		{
			get{return tableImage;}
			set{tableImage = value;}
		}
		// END WES GRANT ADDED
		


		internal Table mainTable = new Table();
		internal Table editorTable = new Table();
		internal Table editorTable2 = new Table();
		internal Color _mouseOverColor;
		internal Color _mouseDownColor;
		internal Color _editorBackGround;
		internal Color _imageBackground;
		internal Color _imageBorderColor;
		internal Color _ContentEditorBackground = Color.White ;
		internal string _HTMLValue;
		

		[
		Browsable(true),
		Category("Appearance"),
		Description("Background Color of image on mouseover.")
		]
		public Color MouseOverColor
		{
			get{return _mouseOverColor;}
			set{_mouseOverColor = value;}
		}

		[
		Browsable(true),
		Category("Appearance"),
		Description("Color of image on mousedown.")
		]
		public Color MouseDownColor
		{
			get{return _mouseDownColor;}
			set{_mouseDownColor = value;}
		}

		[
		Browsable(true),
		Category("Appearance"),
		Description("Background Color of Editor.")
		]
		public Color EditorBackGround
		{
			get{return _editorBackGround;}
			set{_editorBackGround = value;}
		}

		[
		Browsable(true),
		Category("Appearance"),
		Description("Image Background")
		]
		public Color ImageBackground
		{
			get{return _imageBackground;}
			set{_imageBackground = value;}
		}

		[
		Browsable(true),
		Category("Appearance"),
		Description("Image Border Color")
		]
		public Color ImageBorderColor
		{
			get{return _imageBorderColor;}
			set{_imageBorderColor = value;}
		}

		[
		Browsable(true),
		Category("HTML Editor value"),
		Description("The initial value of the Editor")
		]
		public string HTMLValue
		{
			get
			{
				return _HTMLValue;
			}
			set
			{
				_HTMLValue = value;
				
			}
		}

		[
		Browsable(true),
		Category("Content Editor Background Color"),
		Description("The background color of the Editor")
		]
		public Color ContentEditorBackground
		{
			get
			{
				return _ContentEditorBackground;
			}
			set
			{
				_ContentEditorBackground = value;
			}
		}


		/// <summary>
		/// This event is fired when the save button is clicked
		/// </summary>
		/// <param name="e">Event Args</param>
		protected virtual void OnEditorHTMLSaved(EditorEventArgs e)
		{
			if (EditorHTMLSaved !=null)
				EditorHTMLSaved(this,e);
		}
		/// <summary>
		/// Handles the events when save image is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.HTMLValue= Page.Request.Params["HTMLContent"];
			EditorEventArgs editorEventArgs = new EditorEventArgs();
			editorEventArgs.EditorHTMLValue = Page.Request.Params["HTMLContent"];
			OnEditorHTMLSaved(editorEventArgs);
		}

		/// <summary>
		/// Sets value of the Editor
		/// </summary>
		/// 
		public void PopulateHTMLContent()
		{
			Page.RegisterHiddenField("HTMLContent",this.HTMLValue);	
		}

#if ALLOWVIEWHTML
		/// <summary>
		/// Creates the View HTML Checkbox
		/// </summary>
		/// <returns>A TableCell is returned that contains the View HTML CheckBox</returns>
		private TableCell AddHtmlView()
		{
			TableCell htmlViewCell = new TableCell();
			htmlViewCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"font-family: Arial; font-size: 9pt;\"><INPUT onclick=\"setMode(this.checked);\" type=\"checkbox\">&nbsp;View HTML</font>";
			return htmlViewCell;
		}
#endif


		/// <summary>
		/// Adds standard attributes for the image
		/// handles mouseover, mousedown, mouseup, and mouse out
		/// sets background color and border color
		/// </summary>
		/// <param name="theButton">A valid ImageButton</param></param>
		/// <returns>A TableCell that contains the imagebutton with all attributes applied</returns>
		private TableCell AddCellAttributes(ImageButton theButton)
		{
			TableCell attribCell = new TableCell();
			
			attribCell.Attributes.Add("onmouseup","mouseUp(this)");
			attribCell.Attributes.Add("onmousedown","mouseDown(this)");
			attribCell.Attributes.Add("onmouseover","mouseOver(this)");
			attribCell.Attributes.Add("onmouseout","mouseOut(this)");
			attribCell.BackColor= _imageBackground;
			attribCell.BorderWidth = 1;
			attribCell.BorderStyle = BorderStyle.Solid;
			attribCell.BorderColor = _imageBorderColor;
			attribCell.Attributes.Add("class","EditBTN");
			attribCell.Controls.Add(theButton);

			return attribCell;
		}

		protected override void CreateChildControls()
		{
			Controls.Clear();
			if(!Page.IsPostBack)
			{
				//HTMLValue ="";
				
			}

			mainTable.CellSpacing = 1;
			mainTable.CellPadding = 1;
			mainTable.BorderWidth = 0;
			mainTable.BackColor = _editorBackGround;

			TableRow mainTableRow = new TableRow();
			TableCell mainTableCell = new TableCell();

			editorTable.CellPadding = 0;
			editorTable.CellSpacing = 2;
			editorTable.BorderWidth = 0;
			saveButton.ID = "saveButton";
			saveButton.ImageUrl = saveButtonImage;
			saveButton.Attributes.Add("onClick", "save()");
			saveButton.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			TableRow headerRow1 = new TableRow();

			
			
			// headerRow1.Cells.Add(StyleCell());
			headerRow1.Cells.Add(FontCell());
			headerRow1.Cells.Add(SizeCell());

			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("foreColor()","Foreground Color",textColorButtonImage)));
			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Bold')","Bold Text",boldImage)));
			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Italic')","Italic Text",italicImage)));
			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Underline')","Underline Text",underlineImage)));
			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('JustifyLeft')","Left Justify",leftImage)));
			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('JustifyCenter')","Center",centerImage)));
			headerRow1.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('JustifyRight')","Right Justify",rightImage)));
		
			TableCell TD = new TableCell();
			TD.Controls.Add(new LiteralControl("&nbsp;"));

			headerRow1.Cells.Add(TD);
			headerRow1.Cells.Add(TD);
			headerRow1.Cells.Add(TD);


			headerRow1.Cells.Add(AddCellAttributes(saveButton));

			editorTable.Rows.Add(headerRow1);

			

			mainTableCell.Controls.Add(editorTable);

			//
			editorTable2.CellPadding = 0;
			editorTable2.CellSpacing = 2;
			editorTable2.BorderWidth = 0;
			TableRow headerRow2 = new TableRow();


			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Cut')","Cut",cutButtonImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Copy')","Copy",copyImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Paste')","Paste",pasteImage)));		
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Undo')","Undo",undoImage)));	
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Redo')","Redo",redoImage)));	
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Subscript')","Subscript Text",subscriptImage)));				
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Superscript')","Superscript Text",superscriptImage)));				
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('CreateLink')","Create Link",linkImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('InsertHorizontalRule')","Horizontal Row",hrImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('InsertImage')","Insert Image",imageImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('EditTable')","Edit Table",tableImage)));

			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('InsertOrderedList')","Insert Numbered List",numListImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('InsertUnorderedList')","Insert Unordered List",bulletListImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Indent')","Indent Text",indentImage)));
			headerRow2.Cells.Add(AddCellAttributes(
				CreateCommandButton("cmdExec('Outdent')","Outdent Text",deindentImage)));

#if ALLOWVIEWHTML
			headerRow2.Cells.Add(AddHtmlView());
#endif

			editorTable2.Rows.Add(headerRow2);

			mainTableCell.Controls.Add(editorTable2);
			mainTableRow.Cells.Add(mainTableCell);
			mainTable.Rows.Add(mainTableRow);

			TableRow editorTableRow = new TableRow();
			TableCell editorTableCell = new TableCell();
			
			mainTable.Height = System.Web.UI.WebControls.Unit.Percentage(100);
			editorTableCell.Height = System.Web.UI.WebControls.Unit.Percentage(100);
			mainTable.Width = System.Web.UI.WebControls.Unit.Percentage(100);
			editorTableCell.BackColor = _ContentEditorBackground;

			editorTableCell.Controls.Add(
				new LiteralControl( 
				"<div style=\"BORDER-RIGHT: #333333 thin solid;" +
				"BORDER-TOP: #666666 thin solid; BORDER-LEFT: #666666 thin solid;" +
				"BORDER-BOTTOM: #333333 thin solid; " +
				"HEIGHT: 100%; overflow: auto;" +
				"BACKGROUND-COLOR: " + _ContentEditorBackground.ToString() + ";\" id=\"HTMLEditor\"" +
				"onmouseover=\"enableContext();\" contentEditable=\"true\" " +
				"onmouseout=\"disableContext();\"></div>"));

			editorTableRow.Cells.Add(editorTableCell);
			mainTable.Rows.Add(editorTableRow);

			Controls.Add(mainTable);

			
			PopulateHTMLContent();
			AddClientScript();
		}
		/// <summary>
		/// Adds the onClick attribute to the buttons
		/// </summary>
		/// <param name="command">the JavaScript function to be executed must be in the client side script</param>
		/// <param name="altText">Alt text for the image</param>
		/// <param name="imageURL">the Url for the image</param>
		/// <returns></returns>
		public ImageButton CreateCommandButton(string command,string altText, string imageURL)
		{
			ImageButton editorButton = new ImageButton();
			editorButton.Attributes.Add("onClick", command + " ;return false;");
			editorButton.ImageUrl = imageURL;
			editorButton.ToolTip = altText;
			return editorButton;
		}
		/// <summary>
		/// Creates the Style cell drop down
		/// </summary>
		/// <returns>A TableCell with the populated Style Dropdown</returns>
		public TableCell StyleCell()
		{

			DropDownList styleDropDown = new DropDownList();
			styleDropDown.Attributes.Add("onChange","cmdExec('formatBlock',this[this.selectedIndex].value)");
			styleDropDown.Items.Add("Style");
			styleDropDown.Items.Add("Normal");
			styleDropDown.Items.Add("Heading 1");
			styleDropDown.Items.Add("Heading 2");
			styleDropDown.Items.Add("Heading 3");
			styleDropDown.Items.Add("Heading 4");
			styleDropDown.Items.Add("Heading 5");
			styleDropDown.Items.Add("Address");
			styleDropDown.Items.Add("Formatted");
			styleDropDown.Items.Add("Definition Term");

			TableCell styleCell = new TableCell();
			styleCell.Controls.Add(styleDropDown);
			return styleCell;
		}

		/// <summary>
		/// Creates the Font drop down
		/// </summary>
		/// <returns>A TableCell with the populated Font dropdown</returns>
		public TableCell FontCell()
		{
			DropDownList fontDropDown = new DropDownList();
			fontDropDown.Attributes.Add("OnChange","cmdExec('fontname',this[this.selectedIndex].value);");
			fontDropDown.Items.Add("Font");
			fontDropDown.Items.Add("Arial");
			fontDropDown.Items.Add("Arial Black");
			fontDropDown.Items.Add("Arial Narrow");
			fontDropDown.Items.Add("Comic Sans MS");
			fontDropDown.Items.Add("Courier New");
			fontDropDown.Items.Add("System");
			fontDropDown.Items.Add("Tahoma");
			fontDropDown.Items.Add("Times New Roman");
			fontDropDown.Items.Add("Verdana");
			fontDropDown.Items.Add("Wingdings");
			TableCell fontCell = new TableCell();
			fontCell.Controls.Add(fontDropDown);

			return fontCell;
		}
		/// <summary>
		/// Creates the Size dropdown
		/// </summary>
		/// <returns>A TableCell with the populated Size dropdown</returns>
		public TableCell SizeCell()
		{
			DropDownList sizeDropDown = new DropDownList();
			sizeDropDown.Attributes.Add("OnChange","cmdExec('fontsize',this[this.selectedIndex].value);");
			sizeDropDown.Items.Add("Size");
			for(int i=1; i<15; i++)
			{
				sizeDropDown.Items.Add(i.ToString());
			}
			TableCell sizeCell = new TableCell();
			sizeCell.Controls.Add(sizeDropDown);

			return sizeCell;
		}
		/// <summary>
		/// Injects the JavaScript to the client
		/// </summary>
		public void AddClientScript()
		{
			if(!Page.IsClientScriptBlockRegistered("clientScript"))
			{
				string stringScript ="" ;
				stringScript += "<script language=\"jscript\">\n";
				stringScript += "var imageURL;\n";				
				stringScript += "var isHTMLMode=false;\n";
				stringScript += "var sInitColor = null;\n";
				stringScript += "document.oncontextmenu=new Function(\"return false\");\n";
				stringScript += "\n";

				// WES GRANT ADDED 12/29/02
				stringScript += "var selectionSave;\n";
				stringScript += "var selectionType;\n"; 
				stringScript += "function saveSelection() {\n";
				stringScript += "selectionSave = document.selection.createRange();\n";
				stringScript += "selectionType = document.selection.type;\n";
				stringScript += "}\n";
				//stringScript += "function restoreSelection() { selectionSave.select(); }\n";
				// WES GRANT END


				stringScript += "function disableSave(){\n";
				stringScript += "document.all." + saveButton.ClientID.ToString() + ".disabled = true;\n";
				stringScript += "}\n";
				stringScript += "function enableSave(){\n";
				stringScript += "document.all." + saveButton.ClientID.ToString() + ".disabled = false;\n";
				stringScript += "}\n";
				stringScript += "function enableContext()\n";
				stringScript += "	{\n";
				stringScript += "	document.oncontextmenu=new Function(\"return true\");\n";
				stringScript += "	}\n";
				stringScript += "function disableContext()\n";
				stringScript += "	{\n";
				stringScript += "	document.oncontextmenu=new Function(\"return false\");\n";
				stringScript += "	}\n";
				stringScript += "function mouseOver(eButton)\n";
				stringScript += "	{\n";
				stringScript += "	eButton.style.backgroundColor = \"" + 
					ColorTranslator.ToHtml(_mouseOverColor) + "\";\n";
				stringScript += "	}\n";
				stringScript += "function mouseOut(eButton)\n";
				stringScript += "	{\n";
				stringScript += "	eButton.style.backgroundColor = \"" + 
					ColorTranslator.ToHtml(_imageBackground) + "\";\n";
				stringScript += "	}\n";
				stringScript += "function mouseDown(eButton)\n";
				stringScript += "	{\n";
				stringScript += "	eButton.style.backgroundColor = \"" + 
					ColorTranslator.ToHtml(_mouseDownColor) + "\";\n";
				stringScript += "	}\n";
				stringScript += "function mouseUp(eButton)\n";
				stringScript += "	{\n";
				stringScript += "	eButton.style.backgroundColor = \"" + 
					ColorTranslator.ToHtml(_imageBackground) + "\";\n";
				stringScript += "	eButton = null; \n";
				stringScript += "	}\n";
				stringScript += "function cmdExec(cmd,opt) \n";
				stringScript += "	{\n";
				stringScript += "  	if (isHTMLMode){alert(\"Please uncheck 'View HTML'\");return ;}\n";
				stringScript += "  	HTMLEditor.focus()\n";
				stringScript += "  	switch (cmd)\n";
				stringScript += "  	{\n";
				stringScript += "  	case \"InsertImage\":\n";
				stringScript += "		saveSelection();\n";
				//stringScript += "  		HTMLEditor.focus()\n";
				stringScript += "		var chasm = screen.availWidth;\n";
				stringScript += "		var mount = screen.availHeight;\n";
				stringScript += "		var w = 400;\n";
				stringScript += "		var h = 400;\n";
				stringScript += "		window.open ('" + fileManager + "','','menubar=yes, status=yes, width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));\n";
				stringScript += "  		break;\n";
				
				// WES GRANT ADDED 12/29/02
				// EDIT TABLE FUNCTIONS
				stringScript += "   case \"EditTable\":\n";
				stringScript += "		saveSelection();\n";
				stringScript += "  		HTMLEditor.focus()\n";
				stringScript += "		var chasm = screen.availWidth;\n";
				stringScript += "		var mount = screen.availHeight;\n";
				stringScript += "		var w = 500;\n";
				stringScript += "		var h = 400;\n";
				stringScript += "		window.open ('" + tableEditor + "','','menubar=yes, status=yes, width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));\n";
				stringScript += "       break;\n";
				// END EDIT TABLE FUNCTIONS
					
				stringScript += "  	default:\n";
				stringScript += "  			HTMLEditor.focus()\n";
				stringScript += "   		HTMLEditor.document.execCommand(cmd,false,opt);  	\n";
				stringScript += "  			break;\n";
				stringScript += "  	}\n";
				stringScript += "  \n";
				stringScript += "	}\n";
			

				// WES GRANT ADDED 01/19/03
				// TABLE MANIPULATION FUNCTIONS
				stringScript += "	function InsertTable (sHTML) \n";
				stringScript += "	{  \n";
				stringScript += "		window.close;\n";
				stringScript += "  		HTMLEditor.focus()\n";
				stringScript += "		if (selectionType==\"Control\") selectionSave.item(0).outerHTML = sHTML \n";
				stringScript += "			else selectionSave.pasteHTML(sHTML);\n";	
				stringScript += "  	}\n";
				stringScript += "  \n";
				//END TABLE MANIPULATION FUNCTIONS



				// WES GRANT ADDED 01/13/03
				// UPDATE IMAGE INFORMATION 
				stringScript += "function UpdateImage(inpImgURL,inpImgAlt,inpImgAlign,inpImgBorder,inpImgWidth,inpImgHeight,inpHSpace,inpVSpace)\n";
				stringScript += "		{\n";
				stringScript += "		var oSel	= document.selection.createRange()\n";
				stringScript += "		var sType = document.selection.type	\n";
				stringScript += "		if ((oSel.item) && (oSel.item(0).tagName==\"IMG\")) \n";
				stringScript += "			{\n";
				stringScript += "			oSel.item(0).style.width=\"\";\n";
				stringScript += "			oSel.item(0).style.height=\"\";\n";								
				stringScript += "			oSel.item(0).src = inpImgURL\n";
				stringScript += "			if(inpImgAlt!=\"\")\n";
				stringScript += "				oSel.item(0).alt = inpImgAlt\n";
				stringScript += "			else\n";
				stringScript += "				oSel.item(0).removeAttribute(\"alt\",0)\n";
				stringScript += "			oSel.item(0).align = inpImgAlign\n";
				stringScript += "			oSel.item(0).border = inpImgBorder\n";
				stringScript += "			if(inpImgWidth!=\"\")\n";
				stringScript += "				oSel.item(0).width = inpImgWidth\n";
				stringScript += "			else\n";
				stringScript += "				oSel.item(0).removeAttribute(\"width\",0)\n";			
				stringScript += "			if(inpImgHeight!=\"\")\n";
				stringScript += "				oSel.item(0).height = inpImgHeight\n";
				stringScript += "			else\n";
				stringScript += "				oSel.item(0).removeAttribute(\"height\",0)	\n";		
				stringScript += "			if(inpHSpace!=\"\")\n";
				stringScript += "				oSel.item(0).hspace = inpHSpace\n";
				stringScript += "			else\n";
				stringScript += "				oSel.item(0).removeAttribute(\"hspace\",0)	\n";		
				stringScript += "			if(inpVSpace!=\"\")\n";
				stringScript += "				oSel.item(0).vspace = inpVSpace\n";
				stringScript += "			else\n";
				stringScript += "				oSel.item(0).removeAttribute(\"vspace\",0)	\n";		
				stringScript += "			}	\n";
				stringScript += "		}\n";				
				// END UPDATE IMAGE INFORMATION
				

				
				stringScript += "function InsertNewImage(){\n";
				// stringScript += "   alert(imageURL);\n";
				stringScript +=	"	if (selectionType==\"Control\") HTMLEditor.innerHTML = HTMLEditor.innerHTML + \"<IMG \" + imageURL + \" >\";\n";
				stringScript += "		else selectionSave.pasteHTML(\"<IMG \" + imageURL + \" >\")\n";	
				
				stringScript += "\n}\n";

				stringScript +="function foreColor(){\n";
				stringScript +="var arr = showModalDialog(\"" + colorPikcerURL + "\",\"\",\"font-family:arial; font-size:10; dialogWidth:40em; dialogHeight:50em\" );\n";
				stringScript +="if (arr != null) cmdExec(\"ForeColor\",arr);";	
				stringScript +="}\n";

				stringScript += "function setMode(bHTMLMode)\n";
				stringScript += "{\n";
				stringScript += "	var sHTMLContent;\n";
				stringScript += "  	isHTMLMode = bHTMLMode;\n";
				stringScript += "  	if (isHTMLMode){sHTMLContent=HTMLEditor.innerHTML;HTMLEditor.innerText=sHTMLContent;disableSave();} \n";
				stringScript += "	else {sHTMLContent=HTMLEditor.innerText;HTMLEditor.innerHTML=sHTMLContent;enableSave();};\n";
				stringScript += "}\n";
				stringScript += "function save()\n";
				stringScript += "{\n";
				stringScript += "  	if (isHTMLMode){alert(\"Please uncheck 'View HTML'\");return false;}\n";
				stringScript += "	document.all.HTMLContent.value = HTMLEditor.innerHTML;\n";
				stringScript += "}\n";
				stringScript += "function unableToSave(){\n";
				stringScript += "	if (isHTMLMode){alert(\"Please uncheck 'View HTML'\");}\n";
				stringScript += "}\n";
				stringScript += "</script>";

				Page.RegisterClientScriptBlock("clientScript", stringScript);

				stringScript ="";
				stringScript += "<script language=\"jscript\">\n";
				stringScript += "      document.all.HTMLEditor.innerHTML = document.all.HTMLContent.value;\n";
				stringScript += "</script>";

				Page.RegisterStartupScript("StartScript",stringScript);
			}

		}
	
		public void RenderAtDesignTime()
		{
			
			CreateChildControls();
		}
		
	}
	public class YAHENetDesigner : ControlDesigner 
	{

		public override string GetDesignTimeHtml() 
		{
			// Get the instance this designer applies to
			//
			YAHENet vControl = (YAHENet) Component;

			// Render the control at design time
			//
				
				vControl.Controls.Clear();
				vControl.RenderAtDesignTime();

				string vHTML = base.GetDesignTimeHtml();
				vControl.Controls.Clear();
				return vHTML;
			


		}  

	}

}