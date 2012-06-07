using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Portal.API;

namespace Portal
{
	/// <summary>
	/// Collection of Helper Methods. For internal use only.
	/// </summary>
	internal class Helper
	{
		/// <summary>
		/// Returns the proper edit ascx Control. Uses the current Page to load the Control.
		/// If the user has no right a error control is returned
		/// </summary>
		/// <param name="p">The current Page</param>
		/// <returns>Edit ascx Control</returns>
		internal static Control GetEditControl(Page p)
		{
			PortalDefinition.Tab tab = PortalDefinition.GetCurrentTab();
			PortalDefinition.Module m = tab.GetModule(p.Request["ModuleRef"]);

			if(!UserManagement.HasEditRights(HttpContext.Current.User, m.roles)) 
			{
				// No rights, return a error Control
				Label l = new Label();
				l.CssClass = "Error";
				l.Text = "Access denied!";
				return l;
			}
			m.LoadModuleSettings();
			Module em = null;
			if(m.moduleSettings != null)
			{
				// Module Settings are present, use custom ascx Control
				em = (Module)p.LoadControl(Config.GetModuleVirtualPath(m.type) + m.moduleSettings.editCtrl);
			}
			else
			{
				// Use default ascx control (Edit[type].ascx)
				em = (Module)p.LoadControl(Config.GetModuleVirtualPath(m.type) + "Edit" + m.type + ".ascx");
			}

			// Initialize the control
			em.InitModule(
				tab.reference, 
				m.reference, 
				Config.GetModuleVirtualPath(m.type),
				true);

			return em;
		}

		internal static string GetEditLink(string ModuleRef)
		{
			if(Config.GetPortalType() == Config.PortalType.Table)
			{
				// Portal Type Table
				return "EditPageTable.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
			}
			else
			{
				// Portal Type Frame
				return "EditPageFrame.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
			}
		}

		internal static string GetEditModuleLink(string ModuleRef)
		{
			if(Config.GetPortalType() == Config.PortalType.Table)
			{
				// Portal Type Table
				return "EditModuleTable.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
			}
			else
			{
				// Portal Type Frame
				return "EditModuleFrame.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
			}
		}

		internal static string GetEditTabLink()
		{
			return GetEditTabLink(HttpContext.Current.Request["TabRef"]);
		}
		internal static string GetEditTabLink(string tabRef)
		{
			if(Config.GetPortalType() == Config.PortalType.Table)
			{
				// Portal Type Table
				return "EditTabTable.aspx?TabRef=" + tabRef;
			}
			else
			{
				// Portal Type Frame
				return "EditTabFrame.aspx?TabRef=" + tabRef;
			}
		}
		internal static string GetTabLink(string reference)
		{
			if(Config.GetPortalType() == Config.PortalType.Frames)
			{
				return "javascript:SelectTab('" + reference + "');";
			}
			else
			{
				return Config.GetTabURL(reference);
			}
		}
	}
}
