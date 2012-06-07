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
	public abstract class ImageBrowser : Portal.API.Module, IPostBackEventHandler //System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Panel ImageBrowserPanel;
		protected System.Web.UI.WebControls.Panel ImageViewPanel;
		protected System.Web.UI.WebControls.HyperLink image;
		protected Label lbImageText;
		protected TextBox txtImageText;
		protected HyperLink lnkSaveImageText;
		protected HyperLink lnkSetFolderImage;

		protected string back;
		private ImageTools imageTools = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				imageTools = new ImageTools(this);
				if(!IsPostBack)
				{
					BuildDirectories(imageTools.cfg.PictureVirtualDirectory);
				}
			}
			catch
			{
			}
		}

		public void RaisePostBackEvent(string args)
		{
			string[] a = args.Split(';');

			if(a[0].ToLower() == "picture")
			{
				BuildPicture(a[1]);
			}
			else if(a[0].ToLower() == "directory")
			{
				BuildDirectories(a[1]);
			}
			else if(a[0].ToLower() == "directorysave")
			{
				DirectoryWrapper data = new DirectoryWrapper(imageTools.GetPath(a[1]), imageTools);
				data.Blurb = Request[UniqueID + ":editblurb"];

				BuildDirectories(a[1]);
			}
			else if(a[0].ToLower() == "picturesave")
			{
				ImageWrapper i = imageTools.GetImageWrapper(imageTools.GetPath(a[1]));
				i.Blurb = txtImageText.Text;

				BuildDirectories(a[1].Substring(0, a[1].LastIndexOf('/')));
			}
			else if(a[0].ToLower() == "setfolderimage")
			{
				string path = Server.MapPath(a[1]);
				string newFile = path.Substring(0, path.LastIndexOf('\\')) + 
					"\\_dirimage." + path.Substring(path.LastIndexOf(".") + 1);
				string thumbFile = path.Substring(0, path.LastIndexOf('\\')) + 
					"\\thumbs\\_dirimage." + path.Substring(path.LastIndexOf(".") + 1);

				System.IO.File.Copy(path, newFile, true);
				System.IO.File.Delete(thumbFile);

				BuildDirectories(a[1].Substring(0, a[1].LastIndexOf('/')));
			}
		}

		private void BuildPicture(string vPath)
		{
			ImageViewPanel.Visible = true;
			ImageBrowserPanel.Visible = false;

			string path = imageTools.GetPath(vPath);
			ImageWrapper i = imageTools.GetImageWrapper(path);

			back = Page.GetPostBackClientHyperlink(this, "directory;" + vPath.Substring(0, vPath.Length - i.Name.Length - 1 ));

			image.NavigateUrl = i.FullImageHref;
			image.ImageUrl = i.WebImageHref;

			lbImageText.Visible = false;
			txtImageText.Visible = false;
			lnkSaveImageText.Visible = false;
			lnkSetFolderImage.Visible = false;
			
			if(ModuleHasEditRights)
			{
				txtImageText.Text = i.Blurb;
				txtImageText.Visible = true;

				lnkSaveImageText.Visible = true;
				lnkSaveImageText.NavigateUrl = Page.GetPostBackClientHyperlink(this, "picturesave;" + vPath);
				
				lnkSetFolderImage.Visible = true;			
				lnkSetFolderImage.NavigateUrl = Page.GetPostBackClientHyperlink(this, "setfolderimage;" + vPath);
			}
			else
			{
				lbImageText.Text = i.Blurb;
				lbImageText.Visible = true;
			}
		}

		private void BuildDirectories(string vPath)
		{
			ImageViewPanel.Visible = false;
			ImageBrowserPanel.Visible = true;

			string path = imageTools.GetPath(vPath);
			DirectoryWrapper data = new DirectoryWrapper(path, imageTools);

			// draw navigation
			HtmlTools.RendenderLinkPath( ImageBrowserPanel.Controls, path, this, imageTools.cfg );

			ImageBrowserPanel.Controls.Add(HtmlTools.HR);

			if(ModuleHasEditRights)
			{
				TextBox editblurb = new TextBox();
				editblurb.ID = "editblurb";
				editblurb.Text = data.Blurb;
				editblurb.Width = new Unit("100%");
				editblurb.Height = new Unit("100px");
				editblurb.TextMode = TextBoxMode.MultiLine;
				ImageBrowserPanel.Controls.Add(editblurb);

				HyperLink lnkSave = new HyperLink();
				lnkSave.NavigateUrl = Page.GetPostBackClientHyperlink(this, "directorysave;" + vPath);
				lnkSave.Text = "Save";
				ImageBrowserPanel.Controls.Add(lnkSave);

				ImageBrowserPanel.Controls.Add(HtmlTools.HR);
			}
			else
			{
				// pump out the blurb
				Label blurb = new Label();
				blurb.Text = data.Blurb;
				ImageBrowserPanel.Controls.Add(blurb);

				// draw a line if appropriate
				if ( blurb.Text.Length > 0 && ( data.Directories.Count > 0 || data.Images.Count > 0)  )
				{
					ImageBrowserPanel.Controls.Add(HtmlTools.HR);
				}
			}


			// draw subdirectories
			ImageBrowserPanel.Controls.Add( HtmlTools.RenderDirectoryTable(4,data, this) );


			// draw a line if appropriate
			if ( data.Directories.Count > 0 && data.Images.Count > 0 )
			{
				ImageBrowserPanel.Controls.Add(HtmlTools.HR);
			}

			// draw images
			ImageBrowserPanel.Controls.Add( HtmlTools.RenderImageTable(7,0,data, this ) );
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
