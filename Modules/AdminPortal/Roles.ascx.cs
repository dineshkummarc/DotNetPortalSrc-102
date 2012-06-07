using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.Modules.AdminPortal
{
	/// <summary>
	///		Summary description for Roles.
	/// </summary>
	public abstract class Roles : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cbAddRoleType;
		protected System.Web.UI.WebControls.DropDownList cbAddRole;
		protected Repeater gridRoles;
		protected HtmlTableCell tdAddRoleType;
		protected HtmlTableCell tdHeaderRoleType;

		public bool ShowRoleType = true;

		private ArrayList roleList = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!ShowRoleType)
			{
				tdAddRoleType.Visible = false;
				tdHeaderRoleType.Visible = false;
			}
			else
			{
				tdAddRoleType.Visible = true;
				tdHeaderRoleType.Visible = true;
			}
		}

		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			roleList = (ArrayList)ViewState["roleList"];
		}
		protected override object SaveViewState()
		{
			ViewState["roleList"] = roleList;
			return base.SaveViewState();
		}

		public void LoadData(ArrayList roles)
		{
			// Init Data
			roleList = roles;

			// Init first time
			cbAddRoleType.SelectedIndex = 0;

			cbAddRole.DataSource = UserManagement.GetUsers().Role;
			cbAddRole.DataBind();
			cbAddRole.Items.Insert(0, "");
			cbAddRole.SelectedIndex = 0;

			// Bind Repeater
			Bind();
		}

		public ArrayList GetData()
		{
			return roleList;
		}

		protected void OnAddRole(object sender, EventArgs args)
		{
			if(ShowRoleType && (cbAddRole.SelectedIndex == 0 || cbAddRoleType.SelectedIndex == 0)) return;
			if(!ShowRoleType && cbAddRole.SelectedIndex == 0 ) return;

			PortalDefinition.Role role = null;
			if(ShowRoleType)
			{
				if(cbAddRoleType.SelectedItem.Value == "view")
				{
					role = new PortalDefinition.ViewRole();
				}
				else
				{
					role = new PortalDefinition.EditRole();
				}
			}
			else
			{
				role = new PortalDefinition.ViewRole();
			}

			role.name = cbAddRole.SelectedItem.Value;
			roleList.Add(role);

			Bind();
		}

		protected void OnDelete(Object sender, CommandEventArgs args)
		{
			roleList.RemoveAt(Int32.Parse((string)args.CommandArgument));
			Bind();
		}

		private void Bind()
		{
			gridRoles.DataSource = roleList;
			gridRoles.DataBind();

			cbAddRole.SelectedIndex = 0;
			cbAddRoleType.SelectedIndex = 0;
		}

		protected void OnDataBind(object sender, RepeaterItemEventArgs args)
		{
			if(	args.Item.ItemType != ListItemType.Item && 
				args.Item.ItemType != ListItemType.AlternatingItem )
			{
				return;
			}
			Label lRoleType = (Label)args.Item.FindControl("lRoleType");
			Label lRole = (Label)args.Item.FindControl("lRole");
			HtmlTableCell tdRoleType = (HtmlTableCell)args.Item.FindControl("tdRoleType");
			LinkButton lnkDelete = (LinkButton)args.Item.FindControl("lnkDelete");

			if(!ShowRoleType)
			{
				tdRoleType.Visible = false;
			}
			else
			{
				if(args.Item.DataItem as PortalDefinition.EditRole != null)
				{
					lRoleType.Text = "Edit";
				}
				else
				{
					lRoleType.Text = "View";
				}
			}

			lnkDelete.CommandArgument = args.Item.ItemIndex.ToString();
			lRole.Text = ((PortalDefinition.Role)args.Item.DataItem).name;
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
