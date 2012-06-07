using System;
using System.Web;

namespace Portal.API
{
	/// <summary>
	/// Configuration Management Class
	/// </summary>
	public class Config
	{
		/// <summary>
		/// Administrator Role. May edit/view everything
		/// </summary>
		public const string AdminRole = "Admin";
		/// <summary>
		/// Signed in User
		/// </summary>
		public const string UserRole = "User";
		/// <summary>
		/// Signed in User or Anonymous User
		/// </summary>
		public const string EveryoneRole = "Everyone";
		/// <summary>
		/// Not signed in User
		/// </summary>
		public const string AnonymousRole = "Anonymous";

		/// <summary>
		/// Renders the Portal with Frames or as a Table
		/// </summary>
		public enum PortalType
		{
			/// <summary>
			/// Renders the Portal as a Table
			/// </summary>
			Table = 0,
			/// <summary>
			/// Renders the Portal with Frames
			/// </summary>
			Frames = 1
		}

		/// <summary>
		/// Returns the virtual Modules Path
		/// </summary>
		/// <returns>~/Modules/</returns>
		public static string GetModuleVirtualPath()
		{
			return "~/Modules/";
		}
		/// <summary>
		/// Returns the physical Module Path. Uses the "GetModuleVirtualPath()" Method
		/// </summary>
		/// <returns>[Application Base Path]/Modules</returns>
		public static string GetModulePhysicalPath()
		{
			return HttpContext.Current.Server.MapPath(GetModuleVirtualPath());
		}
		/// <summary>
		/// Returns the virtual Path of a Module. Uses the "GetModuleVirtualPath()" method 
		/// </summary>
		/// <param name="ctrlType">Module Type</param>
		/// <returns>~/Module/[ctrlType]/</returns>
		public static string GetModuleVirtualPath(string ctrlType)
		{
			return GetModuleVirtualPath() + ctrlType + "/";
		}
		/// <summary>
		/// Returns the physical Path of a Module. Uses the "GetModuleVirtualPath()" method 
		/// </summary>
		/// <param name="ctrlType"></param>
		/// <returns></returns>
		public static string GetModulePhysicalPath(string ctrlType)
		{
			return HttpContext.Current.Server.MapPath(GetModuleVirtualPath(ctrlType));
		}

		/// <summary>
		/// Returns the physical path to the Portal Definition File
		/// </summary>
		/// <returns>[Application Base Path]/Portal.config</returns>
		public static string GetPortalDefinitionPhysicalPath()
		{
			return HttpContext.Current.Server.MapPath("~/Portal.config");
		}
		/// <summary>
		/// Returns the physical path to the User List File
		/// </summary>
		/// <returns>[Application Base Path]/Users.config</returns>
		public static string GetUserListPhysicalPath()
		{
			return HttpContext.Current.Server.MapPath("~/Users.config");
		}

		/// <summary>
		/// Returns the Main Render Page in dependent of the Portal Type
		/// </summary>
		/// <returns>RenderTable.aspx or RenderFrame.aspx</returns>
		public static string GetMainPage()
		{
			if(GetPortalType() == PortalType.Table)
			{
				return "RenderTable.aspx";
			}
			else
			{
				return "RenderFrame.aspx";
			}
		}

		public static string GetTabURL(string tabRef)
		{
			if(UseTabHttpModule)
			{
				return tabRef + ".tab.aspx";
			}
			else
			{
				return GetMainPage() + "?TabRef=" + tabRef;
			}
		}

		/// <summary>
		/// Returns the current Portal Type
		/// </summary>
		/// <returns>Table or Frames</returns>
		public static PortalType GetPortalType()
		{
			return (PortalType)Enum.Parse(typeof(PortalType), 
				System.Configuration.ConfigurationSettings.AppSettings["PortalType"],
				true);
		}

		/// <summary>
		/// Use the Tab HTTP Module. 
		/// The Tab ID will be passed as a "Page", not encoded in the URL
		/// </summary>
		public static bool UseTabHttpModule
		{
			get
			{
				try
				{
					return Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["UseTabHttpModule"]);
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Log the Requests URLReferrer Property a startup.
		/// </summary>
		public static bool LogUrlReferrer
		{
			get
			{
				try
				{
					return Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["LogUrlReferrer"]);
				}
				catch
				{
					return false;
				}
			}
		}
	}
}
