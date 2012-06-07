namespace Portal.Modules.AdminPortal
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Tab.
	/// </summary>
	public abstract class Tab : System.Web.UI.UserControl
	{
		protected TextBox txtTitle;
		protected TextBox txtReference;
		protected Roles RolesCtrl;
		protected ModuleList ModuleListCtrl_Left;
		protected ModuleList ModuleListCtrl_Middle;
		protected ModuleList ModuleListCtrl_Right;
		protected ModuleEdit ModuleEditCtrl;
		protected System.Web.UI.WebControls.LinkButton lnkSave;
		protected System.Web.UI.WebControls.LinkButton lnkCancel;
		protected System.Web.UI.WebControls.LinkButton lnkDelete;
		
		private string CurrentReference = "";

		public event EventHandler Save = null;
		public event EventHandler Cancel = null;
		public event EventHandler Delete = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			CurrentReference = (string)ViewState["CurrentReference"];
		}
		protected override object SaveViewState()
		{
			ViewState["CurrentReference"] = CurrentReference;
			return base.SaveViewState();
		}

		private void ShowEditModules()
		{
			ModuleEditCtrl.Visible = true;
			ModuleListCtrl_Left.Visible = false;
			ModuleListCtrl_Middle.Visible = false;
			ModuleListCtrl_Right.Visible = false;
		}
		private void ShowModulesList()
		{
			ModuleEditCtrl.Visible = false;
			ModuleListCtrl_Left.Visible = true;
			ModuleListCtrl_Middle.Visible = true;
			ModuleListCtrl_Right.Visible = true;
		}

		public void LoadData(string tabRef)
		{
			CurrentReference = tabRef;

			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentReference);

			txtTitle.Text = t.title;
			txtReference.Text = CurrentReference;

			RolesCtrl.LoadData(t.roles);
			ModuleListCtrl_Left.LoadData(t.left);
			ModuleListCtrl_Middle.LoadData(t.middle);
			ModuleListCtrl_Right.LoadData(t.right);
		}

		internal void EditModule(string reference)
		{
			ShowEditModules();
			ModuleEditCtrl.LoadData(CurrentReference, reference);
		}

		internal void AddModule(ModuleList list)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentReference);

			PortalDefinition.Module m = new PortalDefinition.Module();
			m.reference = Guid.NewGuid().ToString();

			if(list == ModuleListCtrl_Left)
			{
				t.left.Add(m);
			}
			else if(list == ModuleListCtrl_Middle)
			{
				t.middle.Add(m);
			} 
			else if(list == ModuleListCtrl_Right)
			{
				t.right.Add(m);
			}

			pd.Save();

			// Rebind
			LoadData(CurrentReference);

			EditModule(m.reference);
		}

		protected void OnCancelEditModule(object sender, EventArgs args)
		{
			ShowModulesList();
		}

		protected void OnSaveModule(object sender, EventArgs args)
		{
			// Rebind
			LoadData(CurrentReference);
			ShowModulesList();
		}

		protected void OnDeleteModule(object sender, EventArgs args)
		{
			// Rebind
			LoadData(CurrentReference);
			ShowModulesList();
		}

		internal void MoveModuleUp(int idx, ModuleList list)
		{
			if(idx <= 0) return;

			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentReference);

			ArrayList a = null;

			if(list == ModuleListCtrl_Left)
			{
				a = t.left;
			}
			else if(list == ModuleListCtrl_Middle)
			{
				a = t.middle;
			} 
			else if(list == ModuleListCtrl_Right)
			{
				a = t.right;
			}

			PortalDefinition.Module m = (PortalDefinition.Module)a[idx];
			a.RemoveAt(idx);
			a.Insert(idx - 1, m);

			pd.Save();

			// Rebind
			LoadData(CurrentReference);
		}

		internal void MoveModuleDown(int idx, ModuleList list)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentReference);

			ArrayList a = null;

			if(list == ModuleListCtrl_Left)
			{
				a = t.left;
			}
			else if(list == ModuleListCtrl_Middle)
			{
				a = t.middle;
			} 
			else if(list == ModuleListCtrl_Right)
			{
				a = t.right;
			}

			if(idx >= a.Count-1) return;

			PortalDefinition.Module m = (PortalDefinition.Module)a[idx];
			a.RemoveAt(idx);
			a.Insert(idx + 1, m);

			pd.Save();

			// Rebind
			LoadData(CurrentReference);
		}

		protected void OnCancel(object sender, EventArgs args)
		{
			if(Cancel != null)
			{
				Cancel(this, new EventArgs());
			}

			LoadData(CurrentReference);
			ShowModulesList();
		}


		protected void OnSave(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(CurrentReference);

			t.title = txtTitle.Text;
			t.reference = txtReference.Text;
			t.roles = RolesCtrl.GetData();

			CurrentReference = t.reference;

			pd.Save();

			if(Save != null)
			{
				Save(this, new EventArgs());
			}

			ShowModulesList();
		}

		protected void OnDelete(object sender, EventArgs args)
		{
			PortalDefinition.DeleteTab(CurrentReference);

			if(Delete != null)
			{
				Delete(this, new EventArgs());
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
