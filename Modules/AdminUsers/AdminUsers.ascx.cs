using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.Modules.AdminUsers
{

	/// <summary>
	///		Summary description for AdminUsers.
	/// </summary>
	public abstract class AdminUsers : API.Module
	{
		protected UserList ctrlUserList;
		protected UserEdit ctrlUserEdit;

		void Page_Load(object Sender, EventArgs e)
		{
		}

		internal void EditUser(string account)
		{
			ctrlUserList.Visible = false;
			ctrlUserEdit.Visible = true;
			ctrlUserEdit.EditUser(account);
		}

		internal void ShowUserList()
		{
			ctrlUserList.Visible = true;
			ctrlUserEdit.Visible = false;
			ctrlUserList.BindGrid();
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
