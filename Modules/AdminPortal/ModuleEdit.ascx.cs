namespace Portal.Modules.AdminPortal
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Portal.API;

	/// <summary>
	///		Summary description for Module.
	/// </summary>
	public abstract class ModuleEdit : System.Web.UI.UserControl
	{
		protected Roles RolesCtrl;
		protected DropDownList cbType;
		protected TextBox txtTitle;
		protected TextBox txtReference;

		protected System.Web.UI.WebControls.LinkButton lnkSave;
		protected System.Web.UI.WebControls.LinkButton lnkCancel;
		protected System.Web.UI.WebControls.LinkButton lnkDelete;
		protected System.Web.UI.WebControls.ValidationSummary validation;
		protected System.Web.UI.WebControls.RequiredFieldValidator validator1;
		protected System.Web.UI.WebControls.CustomValidator validator2;
	
		private string CurrentReference = "";
		public string Reference
		{
			get { return CurrentReference; } 
		}

		private string CurrentTabReference = "";
		public string TabReference
		{
			get { return CurrentTabReference; } 
		}

		public event EventHandler Save = null;
		public event EventHandler Cancel = null;
		public event EventHandler Delete = null;
		
		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			CurrentReference = (string)ViewState["CurrentReference"];
			CurrentTabReference = (string)ViewState["CurrentTabReference"];
		}
		protected override object SaveViewState()
		{
			ViewState["CurrentReference"] = CurrentReference;
			ViewState["CurrentTabReference"] = CurrentTabReference;
			return base.SaveViewState();
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void OnValidateCBType(object sender, ServerValidateEventArgs args)
		{
			args.IsValid = cbType.SelectedIndex > 0;
		}

		protected void OnCancel(object sender, EventArgs args)
		{
			if(Cancel != null)
			{
				Cancel(this, new EventArgs());
			}
		}

		protected void OnSave(object sender, EventArgs args)
		{
			if(Page.IsValid)
			{
				// Save
				PortalDefinition pd = PortalDefinition.Load();
				PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);
				PortalDefinition.Module m = t.GetModule(CurrentReference);

				m.reference = txtReference.Text;
				m.title = txtTitle.Text;
				m.type = cbType.SelectedItem.Value;
				m.roles = RolesCtrl.GetData();

				pd.Save();

				if(Save != null)
				{
					Save(this, new EventArgs());
				}
			}
		}
		protected void OnDelete(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);

			t.DeleteModule(CurrentReference);

			pd.Save();

			if(Delete != null)
			{
				Delete(this, new EventArgs());
			}

			// Hopefully we where redirected here!
		}

		public void LoadData(string tabRef, string moduleRef)
		{
			CurrentTabReference = tabRef;
			CurrentReference = moduleRef;

			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);
			PortalDefinition.Module m = t.GetModule(CurrentReference);

			txtTitle.Text = m.title;
			txtReference.Text = m.reference;

			cbType.ClearSelection();
			ListItem li = cbType.Items.FindByValue(m.type);			
			if(li != null)
			{
				li.Selected = true;
			}

			RolesCtrl.LoadData(m.roles);
		}

		private void LoadModuleTypes()
		{
			// Get Module List
			string[] dirs = System.IO.Directory.GetDirectories(Config.GetModulePhysicalPath());
			int idx = Config.GetModulePhysicalPath().Length;
			for(int i=0;i<dirs.Length;i++)
			{
				dirs[i] = dirs[i].Substring(idx);
			}
			// Add empty ListItem
			ArrayList dirList = new ArrayList();
			dirList.Add("");
			dirList.AddRange(dirs);

			// Bind
			cbType.DataSource = dirList;
			cbType.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			if(!IsPostBack)
			{
				LoadModuleTypes();
			}
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
