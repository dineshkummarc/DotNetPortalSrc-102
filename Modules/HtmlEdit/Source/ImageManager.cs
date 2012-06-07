using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace CWebRun.ImageManager
{
	/// <summary>
	/// Summary description for FileManager.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ImageManager runat=server></{0}:ImageManager>")]
	public class ImageManager : System.Web.UI.WebControls.WebControl
	{
		internal string propertyCSS = "FileManager";
		public string PropertyCSS
		{
			get{return propertyCSS;}
			set{propertyCSS = value;}
		}
		
		Table fileTable = new Table();

		TableRow fileMgrRow = new TableRow();
		TableCell fileMgrCell = new TableCell();
		
		TableRow altTextRow = new TableRow();
		TableCell altTextCell = new TableCell();

		TableRow propRow1 = new TableRow();
		TableCell cell1Row1 = new TableCell();
		TableCell cell2Row1 = new TableCell();
		TableCell cell3Row1 = new TableCell();
		TableCell cell4Row1 = new TableCell();

		TableRow propRow2 = new TableRow();
		TableCell cell1Row2 = new TableCell();
		TableCell cell2Row2 = new TableCell();
		TableCell cell3Row2 = new TableCell();
		TableCell cell4Row2 = new TableCell();

		TableRow propRow3 = new TableRow();
		TableCell cell1Row3 = new TableCell();
		TableCell cell2Row3 = new TableCell();
		TableCell cell3Row3 = new TableCell();
		TableCell cell4Row3 = new TableCell();

		AWS.FilePicker srcTextBox = new AWS.FilePicker();
		protected override void CreateChildControls()
		{
			//cellSpacing="1" cellPadding="1" width="300" border="0"
			fileTable.CellPadding = 1;
			fileTable.CellSpacing = 1;
			fileTable.BorderStyle = BorderStyle.None;
			fileTable.Width = 300;
			fileTable.Attributes.Add("align","center");

			srcTextBox.ID="FilePicker1";
			srcTextBox.Enabled = false;
			srcTextBox.fpAllowedUploadFileExts = "gif,jpg,jpeg,png";
			srcTextBox.fpUploadDir="/Uploads";
			srcTextBox.fpPopupURL = "FilePicker.aspx";
			srcTextBox.fpImageURL= "/images/browse.gif";
			srcTextBox.fpAllowCreateDirs = false;
			srcTextBox.fpAllowDelete = false;
			
			fileMgrCell.Controls.Add(new LiteralControl("Image Source: "));
			fileMgrCell.Attributes.Add("colSpan","4");
			fileMgrCell.Controls.Add(srcTextBox);
			fileMgrRow.Cells.Add(fileMgrCell);
			fileTable.Rows.Add(fileMgrRow);

			altTextCell.Controls.Add(new LiteralControl("Alternate Text:"));
			HtmlInputText altText = new HtmlInputText();
			altText.ID="AltText";
			altTextCell.Wrap = false;
			altTextCell.Attributes.Add("colSpan","4");
			altTextCell.Controls.Add(altText);
			altTextRow.Cells.Add(altTextCell);
			fileTable.Rows.Add(altTextRow);

			HtmlSelect selAlign = new HtmlSelect();
			selAlign.Items.Add("Select");
			selAlign.Items.Add("Left");
			selAlign.Items.Add("Middle");
			selAlign.Items.Add("Right");
			selAlign.Items.Add("Absbottom");
			selAlign.Items.Add("Absmiddle");
			selAlign.Items.Add("Baseline");

			HtmlInputText borderSize = new HtmlInputText();
			borderSize.Size = 1;
			borderSize.ID="border";
			
			cell1Row1.Controls.Add(new LiteralControl("Alignment:"));
			cell2Row1.Controls.Add(selAlign);
			cell3Row1.Controls.Add(new LiteralControl("Border Size:"));
			cell4Row1.Controls.Add(borderSize);

			propRow1.Cells.Add(cell1Row1);
			propRow1.Cells.Add(cell2Row1);
			propRow1.Cells.Add(cell3Row1);
			propRow1.Cells.Add(cell4Row1);
			
			fileTable.Rows.Add(propRow1);
			Controls.Add(fileTable);

		}
		

	}
}
