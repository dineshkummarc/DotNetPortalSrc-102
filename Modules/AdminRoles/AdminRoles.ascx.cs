using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.Modules.AdminRoles
{
	/// <summary>
	///		Summary description for AdminRoles.
	/// </summary>
	public abstract class AdminRoles : API.Module
	{
		protected DataGrid gridRoles;
		protected TextBox txtNewRole;
		protected LinkButton lnkAddRole; 
		protected HtmlGenericControl msgDeleteAdmin;
	
		private Users roleList = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			msgDeleteAdmin.Visible = false;
			if(!IsPostBack)
			{
				BindGrid();
			}
		}
		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			roleList = (Users)ViewState["RoleList"];
		}
		protected override object SaveViewState()
		{
			ViewState["RoleList"] = roleList;
			return base.SaveViewState();
		}

		protected void OnAddRole(object sender, EventArgs args)
		{
			if(txtNewRole.Text != "")
			{
				roleList.Role.AddRoleRow(txtNewRole.Text);
				UserManagement.SetUsers(roleList);
				BindGrid();
				txtNewRole.Text = "";
			}
		}

		internal void BindGrid()
		{
			roleList = UserManagement.GetUsers();
			gridRoles.DataSource = roleList.Role;
			gridRoles.DataBind();
		}

		protected void Grid_CartCommand(Object sender, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				Users.RoleRow role = roleList.Role.FindByname(roleList.Role[e.Item.ItemIndex].name);
				if(role.name == "Admin")
				{
					msgDeleteAdmin.Visible = true;
					return;
				}
				role.Delete();
				UserManagement.SetUsers(roleList);
				BindGrid();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
