using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal
{
	/// <summary>
	/// Summary description for EditTabTable.
	/// </summary>
	public class EditTabTable : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdEdit;
		protected Portal.Modules.AdminPortal.Tab TabCtrl;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				TabCtrl.LoadData(Request["TabRef"]);
			}
		}

		protected void OnSave(object sender, System.EventArgs args)
		{
			Response.Redirect(Portal.API.Config.GetTabURL(Request["TabRef"]));
		}
		protected void OnCancel(object sender, System.EventArgs args)
		{
			Response.Redirect(Portal.API.Config.GetTabURL(Request["TabRef"]));
		}
		protected void OnDelete(object sender, System.EventArgs args)
		{
			Response.Redirect(Portal.API.Config.GetTabURL(Request["TabRef"]));
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
