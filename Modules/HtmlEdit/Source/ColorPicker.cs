using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CWebRun.ColorPicker
{
	/// <summary>
	/// Summary description for ColorPicker.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ColorPicker runat=server></{0}:ColorPicker>")]
	public class ColorPicker : System.Web.UI.WebControls.WebControl
	{
		Table colorTable = new Table();
		TextBox colorTextBox = new TextBox();
		
		int columnCount;
		protected override void CreateChildControls()
		{
			Controls.Clear();
			colorTextBox.ID = "colorTextBox";
			RegisterJavaScript();
			colorTable.BorderWidth= 0;
			colorTable.CellPadding = 1;
			colorTable.CellSpacing = 1;
			colorTable.Attributes.Add("align","center");
			TableRow colorRow = new TableRow();
			TableCell colorCell = new TableCell();
			columnCount = 0;
			foreach (string colorName in Enum.GetNames(typeof(KnownColor))) 
			{	
					
				columnCount += 1;

				HtmlInputButton myButton = new HtmlInputButton();
				myButton.Style.Add("BORDER-RIGHT","black thin solid");
				myButton.Style.Add("BORDER-TOP","black thin solid");
				myButton.Style.Add("BORDER-LEFT","black thin solid");
				myButton.Style.Add("BORDER-BOTTOM","black thin solid");
				
				myButton.Style.Add("BACKGROUND-COLOR",colorName);
				
				myButton.Style.Add("MARGIN","1px");

				myButton.Attributes.Add("Title",colorName);
				myButton.Attributes.Add("onclick","UpdateColor(this);return false;");
				
				if(columnCount == 20)
				{
					colorCell.Controls.Add(myButton);
					colorCell.Controls.Add(new LiteralControl("<BR>"));
					columnCount=0;

				}
				else
				{
					colorCell.Controls.Add(myButton);
				}
			}

			colorRow.Cells.Add(colorCell);
			colorTable.Rows.Add(colorRow);
	
			TableRow colorRow2 = new TableRow();
			TableCell colorCell2 = new TableCell();
			colorCell2.Controls.Add(new LiteralControl("Color: "));

			colorCell2.Controls.Add(colorTextBox);
			colorCell2.Attributes.Add("align","center");
			colorRow2.Cells.Add(colorCell2);
			colorTable.Rows.Add(colorRow2);
			HtmlInputButton okButton = new HtmlInputButton();
			okButton.Value="OK";
			okButton.Attributes.Add("onclick","onButtonClick()");

			TableRow colorRow3 = new TableRow();			
			TableCell colorCell3 = new TableCell();
			colorCell3.Attributes.Add("align","center");
			colorCell3.Controls.Add(okButton);
			colorRow3.Controls.Add(colorCell3);
			colorTable.Rows.Add(colorRow3);
		
		}
		private void RegisterJavaScript()
		{
			if(!Page.IsClientScriptBlockRegistered("clientScript"))
			{
				StringBuilder javaScript = new StringBuilder("");
				javaScript.Append("<script language=\"javascript\">\n");
				javaScript.Append("		function UpdateColor(button){\n");
				javaScript.Append("		document.forms[0]." + colorTextBox.ClientID.ToString() + ".value = button.title;\n");
				javaScript.Append("}\n");
				javaScript.Append("function onButtonClick(){\n");
                javaScript.Append("window.returnValue = document.forms[0]." + colorTextBox.ClientID.ToString() + ".value;\n");
				javaScript.Append("window.close();\n}\n");
				javaScript.Append("</script>\n");

				Page.RegisterClientScriptBlock("clientScript", javaScript.ToString());
			}
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			System.IO.StringWriter tw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
			colorTable.RenderControl(hw);
			output.Write(tw);
		}
	}
}
