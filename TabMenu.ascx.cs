using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Portal.API;

namespace Portal
{
	/// <summary>
	///	Renders the Headers Tab Menu. 
	///	For the 'Frame' mode a 'SelectTab' client script function is provided.
	/// </summary>
	public abstract class TabMenu : System.Web.UI.UserControl
	{
		protected Repeater Tabs;

		/// <summary>
		/// Wrapper Class for the Tab Object.
		/// </summary>
		public class DisplayTabItem
		{
			/// <summary>
			/// Menus Text
			/// </summary>
			public string Text
			{
				get { return m_Text; }
			}
			/// <summary>
			/// Tabs URL.
			/// </summary>
			public string URL
			{
				get { return m_URL; }
			}
			/// <summary>
			/// True if the menu item represents the current Tab
			/// </summary>
			public bool CurrentTab
			{
				get { return m_CurrentTab; }
			}

			internal string m_Text = "";
			internal string m_URL = "";
			internal bool m_CurrentTab = false;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Load Protal Definition and the current Tab
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab currentTab = pd.GetTab(Request["TabRef"]);

			// Foreach Tab...
			ArrayList tabList = new ArrayList();
			foreach(PortalDefinition.Tab t in pd.tabs)
			{
				if(UserManagement.HasViewRights(Page.User, t.roles))
				{
					// User may view the tab, create a Display Item
					DisplayTabItem dt = new DisplayTabItem();
					tabList.Add(dt);

					dt.m_Text = t.title;

					// Set current Tab Property
					if(currentTab == null)
					{
						if(tabList.Count == 1)
						{
							// First tab -> default
							dt.m_CurrentTab = true;
						}
					}
					else
					{
						dt.m_CurrentTab = currentTab.GetRootTab() == t;
					}

					dt.m_URL = Helper.GetTabLink(t.reference);
				} // if(User may view)
			} // foreach(tab)

			// Bind Repeater
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
