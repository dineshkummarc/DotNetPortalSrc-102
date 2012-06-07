using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.Modules.AdminPortal
{
	/// <summary>
	///		Summary description for TabList.
	/// </summary>
	public abstract class TabList : System.Web.UI.UserControl
	{
		protected DataGrid Tabs;

		/// <summary>
		/// Wrapper Class for the Tab Object.
		/// </summary>
		public class DisplayTabItem
		{
			/// <summary>
			/// Tabs Text
			/// </summary>
			public string Title
			{
				get { return m_Title; }
			}
			/// <summary>
			/// Tabs Reference
			/// </summary>
			public string Reference
			{
				get { return m_Reference; }
			}

			internal string m_Title = "";
			internal string m_Reference = "";
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void OnEditTab(object sender, CommandEventArgs args)
		{
			((AdminPortal)Parent).SelectTab((string)args.CommandArgument);
		}
		protected void OnAddTab(object sender, EventArgs args)
		{
			((AdminPortal)Parent).AddTab();
		}
		protected void OnTabUp(object sender, CommandEventArgs args)
		{
			int idx = Int32.Parse((string)args.CommandArgument);
			((AdminPortal)Parent).MoveTabUp(idx);			
		}
		protected void OnTabDown(object sender, CommandEventArgs args)
		{
			int idx = Int32.Parse((string)args.CommandArgument);
			((AdminPortal)Parent).MoveTabDown(idx);			
		}

		public void LoadData(PortalDefinition pd)
		{
			LoadData(pd.tabs);
		}
		public void LoadData(PortalDefinition.Tab tab)
		{
			LoadData(tab.tabs);
		}

		private void LoadData(ArrayList subTabList)
		{
			ArrayList tabList = new ArrayList();
			foreach(PortalDefinition.Tab t in subTabList)
			{
				DisplayTabItem dt = new DisplayTabItem();
				tabList.Add(dt);

				dt.m_Title = t.title;
				dt.m_Reference = t.reference;
			}
			Tabs.DataSource = tabList;
			Tabs.DataBind();
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
