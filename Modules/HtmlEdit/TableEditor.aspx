<%@ Page %>
<html>
<head>
	<title>
	Edit Table
	</title>
	<link rel="stylesheet" href="../../Portal.css" type="text/css">
</head>

<script language="javascript">
	
	function IsPosInt(sInput)//must be empty or number greater than or equal 1
		{
		if(sInput=="") return true;
		var sTmp = sInput.toString();
		for(var i=0;i<sTmp.length;i++)
			{
			var sChar = sTmp.charAt(i);
			if(sChar<"0"||sChar>"9") return false;
			}
		
		return true;
		}

	function IsPosIntNotZero(sInput)//must be empty or number greater than or equal 1
		{
		if(sInput=="") return true;
		var sTmp = sInput.toString();
		for(var i=0;i<sTmp.length;i++)
			{
			var sChar = sTmp.charAt(i);
			if(sChar<"0"||sChar>"9") return false;
			}
		if(sInput*1==0) {return false}
		else {return true};
		}
		
		
	function TableInsert()
		{
		window.close();
		
		if(!(IsPosIntNotZero(NewTable.Rows.value) &&
			IsPosIntNotZero(NewTable.Columns.value) &&
			IsPosIntNotZero(NewTable.Width.value) &&
			IsPosIntNotZero(NewTable.Height.value) &&
			IsPosInt(NewTable.Border.value) &&
			IsPosInt(NewTable.Cellpadding.value) &&
			IsPosInt(NewTable.Cellspacing.value)
			)) 
			{
			alert("Invalid input.");
			return false;
			}
				
		var sHTML = ""
			+ "<TABLE "
			+ (((NewTable.Border.value=="") || (NewTable.Border.value=="0")) ? "class=\"NOBORDER\"" : "")
			//+	(NewTable.inpTblAlign.value != "" ? "align=\"" + NewTable.inpTblAlign.value + "\" " : "")		
			+	(NewTable.Width.value != "" ? "width=\"" + NewTable.Width.value + NewTable.WidthUnit.value + "\" " : "")
			+	(NewTable.Height.value != "" ? "height=\"" + NewTable.Height.value + NewTable.HeightUnit.value + "\" " : "")		
			+	(NewTable.Cellpadding.value != "" ? "cellPadding=\"" + NewTable.Cellpadding.value + "\" " : "")
			+	(NewTable.Cellspacing.value != "" ? "cellSpacing=\"" + NewTable.Cellspacing.value + "\" " : "")
			+	(NewTable.Border.value != "" ? "border=\"" + NewTable.Border.value + "\" " : "")
			+	(NewTable.BorderColor.value != "" ? "bordercolor=\"" + NewTable.BorderColor.value + "\" " : "")
			//+	(NewTable.inpBgImage.value != "" ? "background=\"" + NewTable.inpBgImage.value + "\" " : "")
			//+	(NewTable.inpBgColor.value != "" ? "bgColor=\"" + NewTable.inpBgColor.value + "\" " : "")
			+ ">"
		for (var i=0; i < NewTable.Rows.value; i++) 
			{
			sHTML += "<TR>"
			for (var j=0; j < NewTable.Columns.value; j++)
			sHTML += "<TD>&nbsp;</TD>"
			sHTML += "</TR>"
			}
		sHTML += "</TABLE>"
		
		window.opener.InsertTable(sHTML);
		
		
		}

	function TableUpdate()
		{
				
				window.close();	
				
				if(!(IsPosIntNotZero(EditTable.Width.value) &&
					IsPosIntNotZero(EditTable.Height.value) &&
					IsPosInt(EditTable.Cellpadding.value) &&
					IsPosInt(EditTable.Cellspacing.value) &&
					IsPosInt(EditTable.Border.value))) 
					{
					alert("Invalid input.");
					return false;
					}
					
					var oSel	= window.opener.selectionSave
					var sType = window.opener.selectionType
						
					var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
					if (oBlock!=null) 
						{
						//oBlock.align = EditTable.inpTblAlign2.value
						if(EditTable.Width.value!="")
							{
							oBlock.style.width=""; //remove style width
							oBlock.width = EditTable.Width.value + EditTable.WidthUnit.value;
							}
						else
							{
							oBlock.width = ""
							}
						if(EditTable.Height.value!="")
							{
							oBlock.style.height=""; //remove style height
							oBlock.height = EditTable.Height.value + EditTable.HeightUnit.value;
							}
						else
							{
							oBlock.height = ""
							}		
						oBlock.cellPadding = EditTable.Cellpadding.value
						oBlock.cellSpacing = EditTable.Cellspacing.value
						oBlock.border = EditTable.Border.value
						oBlock.borderColor = EditTable.BorderColor.value
					
						}
		}		
		
		
	function UpdateCell()
		{
				window.close();
		
				if(!(IsPosIntNotZero(EditCell.Width.value) &&
					IsPosIntNotZero(EditCell.Height.value))) 
					{
					alert("Invalid input.");
					return false;
					}
			
				var oSel	= window.opener.selectionSave
				var sType = window.opener.selectionType
							
				var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))
				if (oTD!=null)
					{
					if(EditCell.Width.value!="")
						{
						oTD.style.width=""; //remove style width 
						oTD.width = EditCell.Width.value + EditCell.WidthUnit.value;
						}
					else
						{
						oTD.width = ""
						}
					if(EditCell.Height.value!="")
						{
						oTD.style.height=""; //remove style height
						oTD.height = EditCell.Height.value + EditCell.HeightUnit.value;
						}
					else
						{
						oTD.height = ""
						}		
					
					oTD.align = EditCell.Align.value ;
					oTD.vAlign = EditCell.Valign.value;					
					oTD.bgColor = EditCell.BgColor.value;
					oTD.noWrap = !(EditCell.WrapText.checked);
					}
		}
		
		
// TAB MANAGEMENT 
		
		function TabInsertTable()
			{
			//window.divBorderColorPick.style.display = "none";
			//window.divBgColorPick.style.display = "none";	
				
			window.divNewTable.style.display="block";
			window.divEditTable.style.display="none";
			window.divEditCell.style.display="none";
		
			window.tab1.style.cursor = "";
			window.tab1.style.background = "#ececec";
			window.tab1.style.color = "darkslateblue";
			window.tab1.style.borderBottom = "";
			window.tab1.innerHTML = "<b>Create Table</b>"
		
			window.tab2.style.background = "#bebebe";
			window.tab2.style.color = "darkslateblue";
			window.tab2.style.borderBottom = "darkgray 1px solid";
			window.tab2.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditTable()'><b><u>Edit Table</u></b></div>"
			
			window.tab3.style.background = "#bebebe";
			window.tab3.style.color = "darkslateblue";
			window.tab3.style.borderBottom = "darkgray 1px solid";
			window.tab3.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditCell()'><b><u>Edit Cell</u></b></div>"
			
			}
			
		function TabEditTable()
		{	
			//window.divBorderColor2Pick.style.display = "none";
			//window.divBgColor2Pick.style.display = "none";	
				
			window.divNewTable.style.display="none";
			window.divEditTable.style.display="block";
			window.divEditCell.style.display="none";
		
			window.tab1.style.background = "#bebebe";
			window.tab1.style.color = "darkslateblue";
			window.tab1.style.borderBottom = "darkgray 1px solid";
			window.tab1.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabInsertTable()'><b><u>Create Table</u></b></div>"
		
			window.tab2.style.cursor = "";
			window.tab2.style.background = "#ececec";
			window.tab2.style.color = "darkslateblue";
			window.tab2.style.borderBottom = "";
			window.tab2.innerHTML = "<b>Edit Table</b>"
			
			window.tab3.style.background = "#bebebe";
			window.tab3.style.color = "darkslateblue";
			window.tab3.style.borderBottom = "darkgray 1px solid";
			window.tab3.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditCell()'><b><u>Edit Cell</u></b></div>"
			
		}
		
		function TabEditCell()
			{
			
			window.divNewTable.style.display="none";
			window.divEditTable.style.display="none";
			window.divEditCell.style.display="block";
			
			window.tab1.style.background = "#bebebe";
			window.tab1.style.color = "darkslateblue";
			window.tab1.style.borderBottom = "darkgray 1px solid";
			window.tab1.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabInsertTable()'><b><u>Create Table</u></b></div>"
			
			window.tab2.style.background = "#bebebe";
			window.tab2.style.color = "darkslateblue";
			window.tab2.style.borderBottom = "darkgray 1px solid";
			window.tab2.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditTable()'><b><u>Edit Table</u></b></div>"
			
			window.tab3.style.cursor = "";
			window.tab3.style.background = "#ececec";
			window.tab3.style.color = "darkslateblue";
			window.tab3.style.borderBottom = "";
			window.tab3.innerHTML = "<b>Edit Cell</b>"	
			
			//when the control (table) is selected, the focus shouldn't be displayed. 
			//Then set the focus to another control.
			//inpCellBgColor.focus()	
			}
		
		
		function EnableEditTableOnly()
		{
			window.tab1.style.cursor = "";
			window.tab1.style.background = "#bebebe";
			window.tab1.style.color = "darkgray";
			window.tab1.style.borderBottom = "";
			window.tab1.innerHTML = "<b><u>Create Table</u></b>"
		
			window.tab3.style.cursor = "";
			window.tab3.style.background = "#bebebe";
			window.tab3.style.color = "darkgray";
			window.tab3.style.borderBottom = "";
			window.tab3.innerHTML = "<b><u>Edit Cell</u></b>"
			}
			
			
		function DisableEditTable()
		{
		
			window.tab2.style.cursor = "";
			window.tab2.style.background = "#bebebe";
			window.tab2.style.color = "darkgray";
			window.tab2.style.borderBottom = "";
			window.tab2.innerHTML = "<b><u>Edit Table</u></b>"
		
			window.tab3.style.cursor = "";
			window.tab3.style.background = "#bebebe";
			window.tab3.style.color = "darkgray";
			window.tab3.style.borderBottom = "";
			window.tab3.innerHTML = "<b><u>Edit Cell</u></b>"		
		}
		
		
		
		
</script>

<body onload="checkTable()">

		<table width="100%">
		<tr><td ID="tab1">
			&nbsp;
		</td><td ID="tab2">
			&nbsp;
		</td><td ID="tab3">
			&nbsp;
		</td></tr>
		</table>

		<span ID="divNewTable">
			<h1>New Table</h1>
			<form id="NewTable">
			<table width="100%">
			<tr>
				<td>
					Rows:
				</td>
				<td>
					<input type="text" ID="Rows" style="Width:50px;" value=2>
				</td>
				<td>
					Columns:
				</td>
				<td>
					<input type="text" ID="Columns" style="Width:50px;" value=2>
				</td>
			</tr>
			<tr>
				<td>
					Cellpadding:
				</td>
				<td>
					<input type="text" ID="Cellpadding" style="Width:50px;" value=5>
				</td>
				<td>
					Cellspacing:
				</td>
				<td>
					<input type="text" ID="Cellspacing" style="Width:50px;" value=0>
				</td>
			</tr>
			<tr>
				<td>
					Width:
				</td>
				<td nowrap>
					<input type="text" ID="Width" style="Width:50px;">
					<select ID="WidthUnit">
						<option value="%">Percent
						<option>Pixels
					</select>
				</td>
				<td>
					Height:
				</td>
				<td nowrap>
					<input type="text" ID="Height" style="Width:50px;">
					<select ID="HeightUnit">
						<option value="%">Percent
						<option>Pixels
					</select>
				</td>
			</tr>
			<tr>
				<td nowrap>
					Border Size:
				</td>
				<td>
					<input type="text" ID="Border" style="Width:50px;" Value="1">
				</td>
				<td>
					Border Color:
				</td>
				<td>
					<input type="text" ID="BorderColor" style="Width:100px;">
				</td>
			</tr>
			
			<tr>
				<td colspan="4" align="center">
					<input type="Button" value="Insert" OnClick="TableInsert()">
				</td>
			</tr>
			</table>
			</form>
		</span>
		
		<span ID="divEditTable">
			<form id="EditTable">
				<h1>Edit Table</h1>
				<table width="100%">
					<tr>
						<td>
							Width:
						</td>
						<td nowrap>
							<input type="text" ID="Width" style="Width:50px;">
							<select ID="WidthUnit">
								<option value="%">Percent
								<option value="px">Pixels
							</select>
						</td>
						<td>
							Height:
						</td>
						<td nowrap>
							<input type="text" ID="Height" style="Width:50px;">
							<select ID="HeightUnit">
								<option value="%">Percent
								<option value="px">Pixels
							</select>
						</td>
					</tr>
					<tr>
						<td nowrap>
							Border Size:
						</td>
						<td>
							<input type="text" ID="Border" style="Width:50px;">
						</td>
						<td>
							Border Color:
						</td>
						<td>
							<input type="text" ID="BorderColor" style="Width:100px;">
						</td>
					</tr>
					<tr>
					<td>
						Cellpadding:
					</td>
					<td>
						<input type="text" ID="Cellpadding" style="Width:50px;">
					</td>
					<td>
						Cellspacing:
					</td>
					<td>
						<input type="text" ID="Cellspacing" style="Width:50px;">
					</td>
				</tr>
			
					<tr>
				<td colspan="4" align="center">
					<input type="Button" value="Update" OnClick="TableUpdate();">
				</td>
			</tr>
			
				</table>
			</form>
		</span>
		
		
		<span ID="divEditCell">
			<form ID="EditCell">
			<table width="100%">
				<tr>
					<td>
						Width:
					</td>
					<td nowrap>
						<input type="text" ID="Width" style="Width:50px;">
						<select ID="WidthUnit">
							<option value="%">Percent
							<option value="px">Pixels
						</select>
					</td>
					<td>
						Height:
					</td>
					<td nowrap>
						<input type="text" ID="Height" style="Width:50px;">
						<select ID="HeightUnit">
							<option value="%">Percent
							<option value="px">Pixels
						</select>
					</td>
				</tr>
				<tr>
				<td>
					Alignment:
				</td>
				<td nowrap>
					<select ID="Align">
						<option value="left">Left
						<option value="center">center
						<option value="right">Right
					</select>
				</td>
				<td nowrap>
					Vertical Alignment:
				</td>
				<td nowrap>
					<select ID="Valign">
						<option value="top">Top
						<option value="middle">Middle
						<option value="bottom">Bottom
					</select>
				</td>
			</tr>
			<tr>
				<td nowrap>
					Background Color:
				</td>
				<td>
					<input type="text" ID="BgColor" style="Width:100px;">
				</td>
				<td>
					Wrap Text
				</td>
				<td>
					<input type="checkbox" ID="WrapText">
				</td>
				</tr>
				
				<td colspan="4" align="center">
					<input type="Button" value="Update" OnClick="UpdateCell();">
				</td>
			
			</table>
			</form>
		
		</span>
		
		
		<SCRIPT  language="javascript">
		
			function GetElement(oElement,sMatchTag) 
				{
				while (oElement!=null && oElement.tagName!=sMatchTag) 
					{
					if(oElement.id=="HTMLEditor") return null;
					oElement = oElement.parentElement
					}
				return oElement
				}
		
			function checkTable()
			{
			
				//alert (window.opener.HTMLEditor.InnerHTML);
				var oSel = window.opener.HTMLEditor.document.selection.createRange()
				var sType = window.opener.HTMLEditor.document.selection.type		
				
				var SelTable
				
				
				
				if (!oSel.item) { // See if it is a table cell
					
					if(oSel.parentElement != null)
						{//Get existing cell properties
						var oTD = GetElement(oSel.parentElement(),"TD");
						
						if(oTD != null)
							{ 							
								var st3 = oTD.width //HTML : width
								if(st3.indexOf("%")!=-1)
									{
									EditCell.Width.value = st3.substring(0,st3.indexOf("%"))
									EditCell.WidthUnit.value = "%"
									}
								else
									{
									EditCell.Width.value = oTD.width
									EditCell.WidthUnit.value = "px"
									}
								var st4 = oTD.height //HTML : height
								if(st4.indexOf("%")!=-1)
									{
									EditCell.Height.value = st4.substring(0,st4.indexOf("%")) 
									EditCell.HeightUnit.value = "%"
									}
								else
									{
									EditCell.Height.value = oTD.height
									EditCell.HeightUnit.value = "px"
									}			
								
								EditCell.Align.value = oTD.align //HTML : align
								EditCell.Valign.value = oTD.vAlign //HTML : vAlign
								EditCell.BgColor.value = (oTD.bgColor).substring(1)
								EditCell.WrapText.checked = !oTD.noWrap;	
							}
						}
				
					SelTable = GetElement(oSel.parentElement(), "TABLE");
					TabEditTable();
				} else if ((oSel.item) && (oSel.item(0).tagName=="TABLE"))  {
					SelTable = oSel.item(0);
					TabEditTable();
					EnableEditTableOnly();
				}
				
				if (SelTable != null && SelTable.tagName=="TABLE")
					{						
							tableWidth = SelTable.width.toString();
							if (tableWidth.indexOf("%") > 0) {
									EditTable.Width.value = tableWidth.replace("%", "");
									
							} else {
									EditTable.Width.value = SelTable.width;
									EditTable.WidthUnit.value = "px";
							}
							
							tableHeight = SelTable.height.toString();
							if (tableHeight.indexOf("%") > 0) {
									EditTable.Height.value = tableHeight.replace("%", "");
									
							} else {
									EditTable.Height.value = SelTable.height;
									EditTable.HeightUnit.value = "px";
							}
							
							EditTable.Border.value = SelTable.border
							EditTable.BorderColor.value = SelTable.borderColor
							
							EditTable.Cellpadding.value = SelTable.cellPadding;
							EditTable.Cellspacing.value = SelTable.cellSpacing;
							
							
							document.all.divNewTable.style.display="none";	
							
					}
				else
				{
					TabInsertTable();
					DisableEditTable();
					document.all.divEditTable.style.display="none";
						
				}		
			}
		</SCRIPT>
			
</body>
</html>