<%@ Control Language="c#" AutoEventWireup="true" Inherits="Portal.API.EditModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="System.IO" %>
<%@ Import namespace="System.Data" %>
<script runat="server">
DataSet ds = null;

void Page_Load(Object Sender, EventArgs e)
{
	lbLinks.Visible = true;
	
	if(!IsPostBack)
	{
		ds = ReadConfig();
		BindGrid();
	}
	else
	{
		ds = (DataSet)ViewState["DataSet"];
	}
}

protected void OnAdd(object sender, EventArgs args)
{
	lbLinks.Visible = false;
	DataTable td = ds.Tables["links"];
	DataRow dr = td.NewRow();
	dr["Position"] = td.Rows.Count + 1;
	dr["Text"] = "";
	dr["URL"] = "http://";
	td.Rows.Add(dr);

	grid.EditItemIndex = td.Rows.Count-1;
	BindGrid();
}

protected void Grid_CartCommand(Object sender, DataGridCommandEventArgs e)
{
	if(e.CommandName == "Delete")
	{
		ds.Tables["links"].Rows.RemoveAt(e.Item.ItemIndex);
		BindGrid();
	}
}

protected void Grid_Edit(Object sender, DataGridCommandEventArgs e) 
{
	lbLinks.Visible = false;
	grid.EditItemIndex = e.Item.ItemIndex;
	BindGrid();
}

protected void Grid_Cancel(Object sender, DataGridCommandEventArgs e) 
{
	grid.EditItemIndex = -1;
	BindGrid();
}

protected void Grid_Update(Object sender, DataGridCommandEventArgs e) 
{
	int idx = grid.EditItemIndex;
	DataRow dr = ds.Tables["links"].Rows[idx];
	dr["Position"] = Int32.Parse(((TextBox)e.Item.Cells[2].Controls[0]).Text);
	dr["Text"] = ((TextBox)e.Item.Cells[3].Controls[0]).Text;
	dr["URL"] = ((TextBox)e.Item.Cells[4].Controls[0]).Text;

	grid.EditItemIndex = -1;
	BindGrid();
}

protected void OnUpdate(object sender, EventArgs args)
{
	WriteConfig(ds);
	RedirectBack();
}
protected void OnCancel(object sender, EventArgs args)
{
	RedirectBack();
}

private void BindGrid()
{
	grid.DataSource = ds;
	grid.DataBind();
	
	ViewState["DataSet"] = ds;
}

</script>
<div id="lbLinks" runat="server">
	<asp:LinkButton id="lnkAdd" runat="server" OnClick="OnAdd" cssclass="LinkButton">Add</asp:LinkButton> | 
	<asp:LinkButton id="lnkUpdate" runat="server" OnClick="OnUpdate" cssclass="LinkButton">Save</asp:LinkButton> | 
	<asp:LinkButton id="lnkCancel" runat="server" OnClick="OnCancel" cssclass="LinkButton">Cancel</asp:LinkButton> | 
</div>
<br>
<asp:datagrid id="grid" runat="server" AutogenerateColumns="false" Width="100%" 
		OnEditCommand="Grid_Edit" OnCancelCommand="Grid_Cancel" 
		OnUpdateCommand="Grid_Update" OnItemCommand="Grid_CartCommand">
	<HeaderStyle CssClass="ListHeader"></HeaderStyle>
	<ItemStyle CssClass="ListLine"></ItemStyle>
	<Columns>
		<asp:EditCommandColumn ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="List_Header" 
			EditText="<img src=images/Edit.gif>" CancelText="<img src=images/cancel.gif>" UpdateText="<img src=images/save.gif>" 
			ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ButtonType="LinkButton" 
			ItemStyle-CssClass="List_Sub_Cell_1" ItemStyle-HorizontalAlign="Center" 
			HeaderStyle-Width="1%" HeaderText="" />
		<asp:ButtonColumn ButtonType="LinkButton" Text="<img src='images/Delete.gif' alt='Delete'>" 
			CommandName="Delete"  HeaderText=""
			HeaderStyle-Width="1%" ItemStyle-CssClass="LinkButton" />
		<asp:BoundColumn DataField="Position" HeaderText="Position" HeaderStyle-Width="1px" ItemStyle-HorizontalAlign="Center" />
		<asp:BoundColumn DataField="Text" HeaderText="Text" />
		<asp:BoundColumn DataField="URL" HeaderText="URL" />
	</Columns>
</asp:datagrid>
