using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Portal.API;

namespace Portal
{
	/// <summary>
	/// Startup Page. Redirects in dependents of the PortalType to the Pages "RenderTable.aspx" or "FrameSet.htm"
	/// </summary>
	public class StartPage : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Where do we come from?
			if(Config.LogUrlReferrer && Request.UrlReferrer != null)
			{
				StreamWriter sw = new StreamWriter(Server.MapPath("~/UrlReferrer.log.txt"), true);
				sw.WriteLine("{0};{1}", DateTime.Now.ToString(), Request.UrlReferrer);
				sw.Close();
			}

			// Check web.config for render method
			if(Config.GetPortalType() == Config.PortalType.Table)
			{
			     Response.Redirect(Config.GetMainPage());
			}
			else
			{
			     Response.Redirect("FrameSet.htm");
			}
		}

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
	}
}
