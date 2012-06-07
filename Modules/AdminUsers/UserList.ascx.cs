using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.Modules.AdminUsers
{
	/// <summary>
	///		Summary description for UserList.
	/// </summary>
	public abstract class UserList : System.Web.UI.UserControl
	{
		protected DataGrid gridUsers;
		protected System.Web.UI.WebControls.LinkButton lnkAddUser;
		
		private Users userList = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				BindGrid();
			}
		}
		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			userList = (Users)ViewState["UserList"];
		}
		protected override object SaveViewState()
		{
			ViewState["UserList"] = userList;
			return base.SaveViewState();
		}

		protected void OnAddUser(object sender, EventArgs args)
		{
			((AdminUsers)Parent).EditUser("");
		}

		internal void BindGrid()
		{
			userList = UserManagement.GetUsers();
			gridUsers.DataSource = userList;
			gridUsers.DataBind();
		}
		protected void Grid_CartCommand(Object sender, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Edit")
			{
				((AdminUsers)Parent).EditUser(userList.User[e.Item.ItemIndex].login);
			}
		}
    
		protected string GetRoles(DataRowView u)
		{
			Users.UserRow ur = (Users.UserRow)u.Row;
			string retVal = "";
			foreach(Users.UserRoleRow r in ur.GetUserRoleRows())
			{
				retVal += r.RoleRow.name + ", ";
			}
			if(retVal.Length > 0)
			{
				retVal = retVal.Substring(0, retVal.Length-2);
			}
			return retVal;
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
