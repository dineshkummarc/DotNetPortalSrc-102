using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iiuga.Web.UI;
using Portal.API;


namespace Portal.Modules.AdminPortal
{
	/// <summary>
	///		Summary description for AdminPortal.
	/// </summary>
	public abstract class AdminPortal : Module, IPostBackEventHandler
	{
		protected iiuga.Web.UI.TreeWeb tree;
		protected Tab TabCtrl;
		protected TabList TabListCtrl;
		private string CurrentReference = "";
		private string CurrentParentReference = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				TabCtrl.Visible = false;
				BuildTree();
				SelectTab("");
			}
		}
		protected void OnSave(object sender, System.EventArgs args)
		{
			BuildTree();
		}
		protected void OnCancel(object sender, System.EventArgs args)
		{
		}
		protected void OnDelete(object sender, System.EventArgs args)
		{
			BuildTree();
			SelectTab(CurrentParentReference);
		}

		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			CurrentReference = (string)ViewState["CurrentReference"];
			CurrentParentReference = (string)ViewState["CurrentParentReference"];
		}
		protected override object SaveViewState()
		{
			ViewState["CurrentReference"] = CurrentReference;
			ViewState["CurrentParentReference"] = CurrentParentReference;
			return base.SaveViewState();
		}

		internal void BuildTree()
		{
			PortalDefinition pd = PortalDefinition.Load();
			tree.Elements[0].Elements.Clear();

			InternalBuildTree(pd.tabs, tree.Elements[0]);
			
			tree.Elements[0].Expand();
			tree.Elements[0].Text = "<a class=\"LinkButton\" href=" + 
				Page.GetPostBackClientHyperlink(this, "") + 
				">Portal</a>";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tabs">IList - can be a Array or ArrayList</param>
		/// <param name="parent"></param>
		private void InternalBuildTree(ArrayList tabs, TreeElement parent)
		{
			foreach(PortalDefinition.Tab t in tabs)
			{
				int n = parent.Elements.Add(t.title);
				parent.Elements[n].Key = t.reference;
				parent.Elements[n].Text = "<a class=\"LinkButton\" href=" + 
					Page.GetPostBackClientHyperlink(this, t.reference) + 
					">" + t.title + "</a>";

				if(t.tabs != null && t.tabs.Count != 0)
				{
					InternalBuildTree(t.tabs, parent.Elements[n]);
					parent.Elements[n].Expand();
				}
				else
				{
					parent.Elements[n].ImageIndex = 0;
				}
			}
		}

		public void SelectTab(string reference)
		{
			PortalDefinition pd = PortalDefinition.Load();
			CurrentReference = reference;
			if(reference == "") // Root Node
			{
				CurrentParentReference = "";
				TabCtrl.Visible = false;
				TabListCtrl.LoadData(pd);
			}
			else
			{
				PortalDefinition.Tab t = pd.GetTab(reference);
				CurrentParentReference = t.parent != null ? t.parent.reference : "";
				TabListCtrl.LoadData(t);
				TabCtrl.Visible = true;
				TabCtrl.LoadData(reference);
			}
		}

		public void AddTab()
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = new PortalDefinition.Tab();
			t.reference = Guid.NewGuid().ToString();

			if(CurrentReference == "") // Root Node
			{
				pd.tabs.Add(t);
			}
			else
			{
				PortalDefinition.Tab pt = pd.GetTab(CurrentReference);
				pt.tabs.Add(t);
			}

			pd.Save();

			BuildTree();
			SelectTab(t.reference);
		}

		internal void MoveTabUp(int idx)
		{
			if(idx <= 0) return;

			PortalDefinition pd = PortalDefinition.Load();
			ArrayList a = null;
			if(CurrentReference == "")
			{
				// Root
				a = pd.tabs;
			}
			else
			{
				PortalDefinition.Tab pt = pd.GetTab(CurrentReference);
				a = pt.tabs;
			}

			PortalDefinition.Tab t = (PortalDefinition.Tab)a[idx];
			a.RemoveAt(idx);
			a.Insert(idx - 1, t);

			pd.Save();

			// Rebind
			BuildTree();
			SelectTab(CurrentReference);
		}

		internal void MoveTabDown(int idx)
		{
			PortalDefinition pd = PortalDefinition.Load();
			ArrayList a = null;
			if(CurrentReference == "")
			{
				// Root
				a = pd.tabs;
			}
			else
			{
				PortalDefinition.Tab pt = pd.GetTab(CurrentReference);
				a = pt.tabs;
			}

			if(idx >= a.Count-1) return;

			PortalDefinition.Tab t = (PortalDefinition.Tab)a[idx];
			a.RemoveAt(idx);
			a.Insert(idx + 1, t);

			pd.Save();

			// Rebind
			BuildTree();
			SelectTab(CurrentReference);
		}

		public void RaisePostBackEvent(string args)
		{
			SelectTab(args);
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
