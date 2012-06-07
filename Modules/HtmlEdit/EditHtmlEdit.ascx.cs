namespace HtmlEdit
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;

	/// <summary>
	///		Summary description for EditHtml.
	/// </summary>
	public abstract class EditHtml : Portal.API.EditModule
	{
		protected CWebRun.Editor.YAHENet YAHENet1;

		private string GetPath()
		{
			return ModulePhysicalPath + ModuleRef + ".htm";
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// Open file            
				if(File.Exists(GetPath()))
				{
					FileStream fs = File.OpenRead(GetPath());
					StreamReader sr = new StreamReader(fs);
					YAHENet1.HTMLValue = sr.ReadToEnd();
					fs.Close();
				}
			}
		}
		protected void OnSave(object sender, CWebRun.Editor.YAHENet.EditorEventArgs  args)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(GetPath(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
				fs.SetLength(0); // Truncate
				StreamWriter sw = new StreamWriter(fs);
				sw.Write(YAHENet1.HTMLValue);
				sw.Close();
            
			}
			finally
			{
				if(fs != null)
				{
					fs.Close();
				}
			}
			RedirectBack();
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
