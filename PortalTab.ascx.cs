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
	///	Renders a Tab.
	/// </summary>
	public abstract class PortalTab : System.Web.UI.UserControl
	{
		protected HtmlTableCell left;
		protected HtmlTableCell middle;
		protected HtmlTableCell right;
		protected OverlayMenu ovm;

		private void RenderModules(HtmlTableCell td, PortalDefinition.Tab tab, ArrayList modules)
		{
			if(modules.Count == 0)
			{
				td.Visible = false;
				return;
			}
			foreach(PortalDefinition.Module md in modules)
			{
				if(UserManagement.HasViewRights(Page.User, md.roles))
				{
					md.LoadModuleSettings();

					// Initialize the Module
					Module m = null;
#if !DEBUG
					try
					{
#endif
						if(md.moduleSettings == null)
						{
							m = (Module)LoadControl(Config.GetModuleVirtualPath(md.type) + md.type + ".ascx");
						}
						else
						{
							m = (Module)LoadControl(Config.GetModuleVirtualPath(md.type) + md.moduleSettings.ctrl);
						}
						m.InitModule(tab.reference, md.reference, 
							Config.GetModuleVirtualPath(md.type), 
							UserManagement.HasEditRights(Page.User, md.roles));
						if(m.IsVisible())
						{
							// Add Module Header
							ModuleHeader mh = (ModuleHeader)LoadControl("ModuleHeader.ascx");
							mh.SetModuleConfig(md);
							td.Controls.Add(mh);

							// Add Module
							HtmlGenericControl div = new HtmlGenericControl("div");
							div.Attributes.Add("class", "Module");
							div.Controls.Add(m);
							td.Controls.Add(div);
						}
#if !DEBUG
					}
					catch(Exception e)
					{
						// Add Module Header
						ModuleHeader mh = (ModuleHeader)LoadControl("ModuleHeader.ascx");
						mh.SetModuleConfig(md);
						td.Controls.Add(mh);

						// Add Error Module
						ModuleFailed mf = (ModuleFailed)LoadControl("ModuleFailed.ascx");
						while(e != null)
						{
							mf.Message += e.GetType().Name + ": ";
							mf.Message += e.Message + "<br>";
							e = e.InnerException;
						}

						mf.Message = mf.Message.Remove(mf.Message.Length - 4, 4);

						HtmlGenericControl div = new HtmlGenericControl("div");
						div.Attributes.Add("class", "Module");
						div.Controls.Add(mf);
						td.Controls.Add(div);
					}
#endif
				}
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			ovm.Visible = Page.User.IsInRole(Portal.API.Config.AdminRole);
		}

		protected void OnAddTab(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = new PortalDefinition.Tab();
			t.reference = Guid.NewGuid().ToString();

			pd.GetTab(Request["TabRef"]).tabs.Add(t);

			pd.Save();

			Response.Redirect(Helper.GetEditTabLink(t.reference));
		}
		protected void OnEditTab(object sender, EventArgs args)
		{
			Response.Redirect(Helper.GetEditTabLink());
		}
		protected void OnDeleteTab(object sender, EventArgs args)
		{
			PortalDefinition.Tab t = PortalDefinition.GetCurrentTab();
			PortalDefinition.DeleteTab(t.reference);

			if(t.parent == null)
			{
				Response.Redirect(Helper.GetTabLink(""));
			}
			else
			{
				Response.Redirect(Helper.GetTabLink(t.parent.reference));
			}
		}
		protected void OnAddLeftModule(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			// Do NOT use GetCurrentTab! You will be unable to save
			PortalDefinition.Tab t = pd.GetTab(Request["TabRef"]);

			PortalDefinition.Module m = new PortalDefinition.Module();
			m.reference = Guid.NewGuid().ToString();
			t.left.Add(m);

			pd.Save();

			Response.Redirect(Helper.GetEditModuleLink(m.reference));
		}
		protected void OnAddMiddleModule(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			// Do NOT use GetCurrentTab! You will be unable to save
			PortalDefinition.Tab t = pd.GetTab(Request["TabRef"]);

			PortalDefinition.Module m = new PortalDefinition.Module();
			m.reference = Guid.NewGuid().ToString();
			t.middle.Add(m);

			pd.Save();

			Response.Redirect(Helper.GetEditModuleLink(m.reference));
		}
		protected void OnAddRightModule(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			// Do NOT use GetCurrentTab! You will be unable to save
			PortalDefinition.Tab t = pd.GetTab(Request["TabRef"]);

			PortalDefinition.Module m = new PortalDefinition.Module();
			m.reference = Guid.NewGuid().ToString();
			t.right.Add(m);

			pd.Save();
		
			Response.Redirect(Helper.GetEditModuleLink(m.reference));
		}

		override protected void OnInit(EventArgs e)
		{
			PortalDefinition.Tab tab = PortalDefinition.GetCurrentTab();

			if(UserManagement.HasViewRights(Page.User, tab.roles))
			{
				// Render
				RenderModules(left, tab, tab.left);
				RenderModules(middle, tab, tab.middle);
				RenderModules(right, tab, tab.right);
			}

			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
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
	}
}
