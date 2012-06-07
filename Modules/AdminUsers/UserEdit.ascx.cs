using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.Modules.AdminUsers
{
	/// <summary>
	///		Summary description for UserEdit.
	/// </summary>
	public abstract class UserEdit : System.Web.UI.UserControl
	{
		protected TextBox txtLogin;
		protected TextBox txtPassword;
		protected TextBox txtFirstName;
		protected System.Web.UI.WebControls.LinkButton lnkBack;
		protected System.Web.UI.WebControls.LinkButton lnkSave;
		protected System.Web.UI.WebControls.LinkButton lnkDelete;
		protected TextBox txtSurName;
		protected DataGrid gridRoles;

		private Users.UserRow user = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected void OnBack(object sender, System.EventArgs args)
		{
			((AdminUsers)Parent).ShowUserList();
		}
		protected void OnSave(object sender, System.EventArgs args)
		{
			ArrayList roles = new ArrayList();
			Users.RoleDataTable rolesTbl = UserManagement.GetUsers().Role;
			foreach(DataGridItem gi in gridRoles.Items)
			{
				CheckBox chkBox = (CheckBox)gi.Cells[0].Controls[1];
				if(chkBox.Checked)
				{
					roles.Add(rolesTbl[gi.DataSetIndex].name);
				}
			}

			UserManagement.SaveUser(txtLogin.Text, txtPassword.Text, txtFirstName.Text, txtSurName.Text, roles);
			((AdminUsers)Parent).ShowUserList();
		}
		protected void OnDelete(object sender, System.EventArgs args)
		{
			UserManagement.DeleteUser(txtLogin.Text);
			((AdminUsers)Parent).ShowUserList();
		}

		protected bool HasRole(DataRowView item)
		{
			if(user == null) return false;

			Users.RoleRow role = (Users.RoleRow)item.Row;
			Users.UserRoleRow[] roles = user.GetUserRoleRows();
			foreach(Users.UserRoleRow r in roles)
			{
				if(r.name == role.name)
				{
					return true;
				}
			}

			return false;
		}

		internal void EditUser(string account)
		{
			if(account != "")
			{
				user = UserManagement.GetUsers().User.FindBylogin(account);
				txtLogin.Text = user.login;
				txtPassword.Text = user.password;
				txtFirstName.Text = user.firstName;
				txtSurName.Text = user.surName;
				txtLogin.Enabled = false;
			}
			else
			{
				txtLogin.Text = "";
				txtPassword.Text = "";
				txtFirstName.Text = "";
				txtSurName.Text = "";
				txtLogin.Enabled = true;
			}

			gridRoles.DataSource = UserManagement.GetUsers().Role;
			gridRoles.DataBind();
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
