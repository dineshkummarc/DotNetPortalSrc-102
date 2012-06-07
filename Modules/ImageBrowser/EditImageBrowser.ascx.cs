/// Found at codeproject.com
/// http://www.codeproject.com/useritems/ImageBrowser.asp
/// 
/// Original Author: 
///		Dan Glass
///		http://www.danglass.com/Web/
///		
///	Changed to a Portal Module by:
///		Arthur Zaczek
///		http://www.zaczek.net


namespace ImageBrowser.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Entities;

	/// <summary>
	///		Summary description for ImageBrowser.
	/// </summary>
	public abstract class EditImageBrowser : Portal.API.EditModule
	{
		protected System.Web.UI.WebControls.TextBox txtPictureVirtualDirectory;
		protected System.Web.UI.WebControls.LinkButton lnkSave;
		protected System.Web.UI.WebControls.LinkButton lnkCancel;
		protected System.Web.UI.WebControls.TextBox txtRootName;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				ImageBrowserConfig cfg = (ImageBrowserConfig)ReadConfig(typeof(ImageBrowserConfig));
				if(cfg != null)
				{
					txtPictureVirtualDirectory.Text = cfg.PictureVirtualDirectory;
					txtRootName.Text = cfg.RootName;
				}
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
			this.lnkSave.Click += new System.EventHandler(this.lnkSave_Click);
			this.lnkCancel.Click += new System.EventHandler(this.lnkCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void lnkSave_Click(object sender, System.EventArgs e)
		{
			ImageBrowserConfig cfg = (ImageBrowserConfig)ReadConfig(typeof(ImageBrowserConfig));
			if(cfg == null)
			{
				cfg = new ImageBrowserConfig();
			}

			cfg.PictureVirtualDirectory = txtPictureVirtualDirectory.Text;
			cfg.RootName = txtRootName.Text;

			WriteConfig(cfg);
			RedirectBack();
		}

		private void lnkCancel_Click(object sender, System.EventArgs e)
		{
            RedirectBack();		
		}
	}
}
