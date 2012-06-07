using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Portal.API;

namespace Portal
{
	/// <summary>
	/// Manages the PortalDefinition.
	/// </summary>
	[XmlRoot("portal"), Serializable]
	public class PortalDefinition
	{
		private static XmlSerializer xmlPortalDef = new XmlSerializer(typeof(PortalDefinition));

		public PortalDefinition()
		{
		}

		private Tab InternalGetTab(ArrayList tabs, string reference)
		{
			reference = reference.ToLower();
			if(tabs == null) return null;

			foreach(Tab t in tabs)
			{
				if(t.reference.ToLower() == reference)
				{
					return t;
				}
				Tab tb = InternalGetTab(t.tabs, reference);
				if(tb != null) return tb;
			}

			return null;
		}

		/// <summary>
		/// Returns a Tab by a reference. 
		/// If not reference is provided it returns the default (first) Tab.
		/// </summary>
		/// <param name="reference">Tabs reference</param>
		/// <returns>null if Tab not found or the default Tab if no reference is provided</returns>
		public Tab GetTab(string reference)
		{
			if(reference == null || reference == "")
			{
				return (Tab)tabs[0];
			}

			return InternalGetTab(tabs, reference);
		}

		/// <summary>
		/// Returns the current Tab. 
		/// The current HTTPContext is used to determinate the current Tab (TabRef=[ref])
		/// </summary>
		/// <returns>The current Tab or the default Tab</returns>
		public static Tab GetCurrentTab()
		{
			PortalDefinition pd = Load();
			return pd.GetTab(HttpContext.Current.Request["TabRef"]);
		}

		public static void DeleteTab(string reference)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(reference);

			string parentRef = "";

			if(t.parent == null) // Root Tab
			{
				for(int i=0;i<pd.tabs.Count;i++)
				{
					if(((PortalDefinition.Tab)pd.tabs[i]).reference == reference)
					{
						pd.tabs.RemoveAt(i);
						break;
					}
				}
			}
			else
			{
				PortalDefinition.Tab pt = t.parent;
				parentRef = pt.reference;
				for(int i=0;i<pt.tabs.Count;i++)
				{
					if(((PortalDefinition.Tab)pt.tabs[i]).reference == reference)
					{
						pt.tabs.RemoveAt(i);
						break;
					}
				}
			}

			pd.Save();
		}

		/// <summary>
		/// Sets the parent Tab object
		/// </summary>
		/// <param name="tabs">Collection of child tabs</param>
		/// <param name="parent">Parrent Tab or null if there is no parrent Tab (root collection)</param>
		internal static void UpdatePortalDefinitionProperties(ArrayList tabs, Tab parent)
		{
			if(tabs == null) return;

			foreach(Tab t in tabs)
			{
				t.parent = parent;
				UpdatePortalDefinitionProperties(t.tabs, t);
			}
		}

		/// <summary>
		/// Loads the Portal Definition
		/// </summary>
		/// <returns>Portal Definition</returns>
		public static PortalDefinition Load()
		{
			XmlTextReader xmlReader = null;
			PortalDefinition pd = null;
			try
			{
				xmlReader = new XmlTextReader(Config.GetPortalDefinitionPhysicalPath());
				pd = (PortalDefinition)xmlPortalDef.Deserialize(xmlReader);
				xmlReader.Close();

				UpdatePortalDefinitionProperties(pd.tabs, null);
			}
			catch(Exception e)
			{
				if(xmlReader != null)
				{
					xmlReader.Close();
				}
				throw new Exception(e.Message, e);
			}

			return pd;
		}

		public void Save()
		{
			XmlTextWriter xmlWriter = null;
			try
			{
				xmlWriter = new XmlTextWriter(Config.GetPortalDefinitionPhysicalPath(), System.Text.Encoding.UTF8);
				xmlWriter.Formatting = Formatting.Indented;
				xmlPortalDef.Serialize(xmlWriter, this);
				xmlWriter.Close();
			}
			catch(Exception e)
			{
				if(xmlWriter != null)
				{
					xmlWriter.Close();
				}
				throw new Exception(e.Message, e);
			}
		}

		/// <summary>
		/// Array of root Tabs
		/// </summary>
		[XmlArray("tabs"), XmlArrayItem("tab", typeof(Tab))]
		public ArrayList tabs = new ArrayList();

		/// <summary>
		/// Tab Definition Object
		/// </summary>
		[Serializable]
		public class Tab
		{
			/// <summary>
			/// Parent Tab. null if it is a root Tab
			/// </summary>
			[XmlIgnore]
			public Tab parent = null;

			/// <summary>
			/// Tabs reference. Must be unique!
			/// </summary>
			[XmlAttribute("ref")]
			public string reference = "";

			/// <summary>
			/// Tabs title.
			/// </summary>
			[XmlElement("title")]
			public string title = "";

			/// <summary>
			/// Collection of view and edit roles.
			/// A View Role is represented by a 'ViewRole' class, a Edit Role by a 'EditRole' class
			/// </summary>
			[	XmlArray("roles"), 
			XmlArrayItem("view", typeof(ViewRole)), 
			XmlArrayItem("edit", typeof(EditRole))]
			public ArrayList roles = new ArrayList();

			/// <summary>
			/// Sub Tab collection.
			/// </summary>
			[XmlArray("tabs"), XmlArrayItem("tab", typeof(Tab))]
			public ArrayList tabs = new ArrayList();

			/// <summary>
			/// Left Modules collection
			/// </summary>
			[XmlArray("left"), XmlArrayItem("module", typeof(Module))]
			public ArrayList left = new ArrayList();

			/// <summary>
			/// Middle Modules collection
			/// </summary>
			[XmlArray("middle"), XmlArrayItem("module", typeof(Module))]
			public ArrayList middle = new ArrayList();
			
			/// <summary>
			/// Right Modules collection
			/// </summary>
			[XmlArray("right"), XmlArrayItem("module", typeof(Module))]
			public ArrayList right = new ArrayList();

			/// <summary>
			/// Returns the Tabs root Tab or this if it is a root Tab
			/// </summary>
			/// <returns>Root Tab or this</returns>
			public Tab GetRootTab()
			{
				if(parent == null) return this;
				return parent.GetRootTab();
			}

			/// <summary>
			/// Returns a Tabs Module by reference
			/// </summary>
			/// <param name="ModuleRef">Module reference</param>
			/// <returns>Module or null</returns>
			public Module GetModule(string ModuleRef)
			{
				ModuleRef = ModuleRef.ToLower();
				foreach(Module m in left)
				{
					if(m.reference.ToLower() == ModuleRef)
					{
						return m;
					}
				}
				foreach(Module m in middle)
				{
					if(m.reference.ToLower() == ModuleRef)
					{
						return m;
					}
				}
				foreach(Module m in right)
				{
					if(m.reference.ToLower() == ModuleRef)
					{
						return m;
					}
				}

				return null;
			}

			public bool DeleteModule(string ModuleRef)
			{
				for(int i=0;i<left.Count;i++)
				{
					if(((PortalDefinition.Module)left[i]).reference == ModuleRef)
					{
						left.RemoveAt(i);
						return true;
					}
				}
				for(int i=0;i<middle.Count;i++)
				{
					if(((PortalDefinition.Module)middle[i]).reference == ModuleRef)
					{
						middle.RemoveAt(i);
						return true;
					}
				}
				for(int i=0;i<right.Count;i++)
				{
					if(((PortalDefinition.Module)right[i]).reference == ModuleRef)
					{
						right.RemoveAt(i);
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// Module Definition Object
		/// </summary>
		[Serializable]
		public class Module
		{
			/// <summary>
			/// Modules reference
			/// </summary>
			[XmlAttribute("ref")]
			public string reference = "";

			/// <summary>
			/// Modules Title
			/// </summary>
			[XmlElement("title")]
			public string title = "";

			/// <summary>
			/// Modules Ctrl Type
			/// </summary>
			[XmlElement("type")]
			public string type = "";

			/// <summary>
			/// Collection of view and edit roles.
			/// A View Role is represented by a 'ViewRole' class, a Edit Role by a 'EditRole' class
			/// </summary>
			[	XmlArray("roles"), 
				XmlArrayItem("view", typeof(ViewRole)), 
				XmlArrayItem("edit", typeof(EditRole))]
			public ArrayList roles = new ArrayList();

			/// <summary>
			/// Module Settings Object. Loaded by the internal Method 'LoadModuleSettings'
			/// </summary>
			[XmlIgnore]
			public ModuleSettings moduleSettings = null;

			/// <summary>
			/// Loads the Modules Settings represented by the 'ModuleSettings.config' File.
			/// Called by the Methods 'Helper.GetEditControl()' and 'PortalTab.RenderModules()'
			/// </summary>
			internal void LoadModuleSettings()
			{
				string path = Config.GetModulePhysicalPath(type) + "ModuleSettings.config";
				if(File.Exists(path))
				{
					XmlTextReader xmlReader = new XmlTextReader(path);
					moduleSettings = (ModuleSettings)ModuleSettings.xmlModuleSettings.Deserialize(xmlReader);
					xmlReader.Close();
				}
				else
				{
					moduleSettings = null;
				}
			}
		}

		/// <summary>
		/// Base class of a Role. Abstract.
		/// </summary>
		[Serializable]
		public abstract class Role
		{
			/// <summary>
			/// Name of the Role
			/// </summary>
			[XmlText]
			public string name = "";
		}

		/// <summary>
		/// Edit Role. Derived from Role.
		/// </summary>
		[Serializable]
		public class EditRole : Role
		{
		}
		/// <summary>
		/// View Role. Derived from Role.
		/// </summary>
		[Serializable]
		public class ViewRole : Role
		{
		}
	}

	/// <summary>
	/// Module Settings Object. Loaded from the Modules 'ModulesSettings.xml' File.
	/// </summary>
	[XmlRoot("module"), Serializable]
	public class ModuleSettings
	{
		internal static XmlSerializer xmlModuleSettings = new XmlSerializer(typeof(ModuleSettings));

		/// <summary>
		/// Modules View .ascx Control.
		/// </summary>
		[XmlElement("ctrl")]
		public string ctrl = "";
		
		/// <summary>
		/// Modules Edit .ascx Control. 'none' if the Module has no Edit Control.
		/// </summary>
		[XmlElement("editCtrl")]
		public string editCtrl = "";

		/// <summary>
		/// True if the Module has no Edit Control. Property editCtrl mus be set to 'none' (case sensitive!)
		/// </summary>
		[XmlIgnore]
		public bool HasEditCtrl
		{
			get { return editCtrl != "none"; }
		}
	}
}
