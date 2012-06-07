using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Portal.API
{
	/// <summary>
	/// Base class for each Portal Module Control. Provides the current Tab and Module Definition.
	/// </summary>
	public class Module : UserControl
	{
		/// <summary>
		/// Parent Tab
		/// </summary>
		private string m_TabRef = "";
		/// <summary>
		/// Module Definition from the Portal Definition
		/// </summary>
		private string m_ModuleRef = "";
		/// <summary>
		/// Modules virtual base path.
		/// </summary>
		private string m_ModuleVirtualPath = "";

		private bool m_HasEditRights = false;

		/// <summary>
		/// Initializes the Control. Called by the Protal Framework
		/// </summary>
		/// <param name="tab"></param>
		/// <param name="module"></param>
		/// <param name="virtualPath"></param>
		public void InitModule(string tabRef, string moduleRef, string virtualPath, bool hasEditRights)
		{			
			m_TabRef = tabRef;
			m_ModuleRef = moduleRef;
			m_ModuleVirtualPath = virtualPath;
			m_HasEditRights = hasEditRights;
		}

		/// <summary>
		/// The Module can control its visibility. The Login Module does so
		/// </summary>
		/// <returns>true if the Module should be visible</returns>
		public virtual bool IsVisible()
		{
			return true;
		}

		/// <summary>
		/// The current Tab reference. Readonly
		/// </summary>
		public string TabRef
		{
			get 
			{ 
				return m_TabRef;
			}
		}
		/// <summary>
		/// The Modules reference. Readonly
		/// </summary>
		public string ModuleRef
		{
			get 
			{ 
				return m_ModuleRef;
			}
		}
		/// <summary>
		/// Modules virtual base path. Readonly
		/// </summary>
		public string ModuleVirtualPath
		{
			get { return m_ModuleVirtualPath; }
		}
		/// <summary>
		/// Modules physical base path. Readonly
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(true)]
		public string ModulePhysicalPath
		{
			get 
			{
				try
				{
					return Server.MapPath(m_ModuleVirtualPath);
				}
				catch // Fuck Designer
				{
					return "";
				}
			}
		}

		/// <summary>
		/// Build a URL to the current Page. Use this method to implement Modules that needs URL Parameter.
		/// </summary>
		/// <param name="parameter">URL Parameter.</param>
		/// <returns>URL with parameter</returns>
		/// <example>Response.Redirect(BuildURL("dir=myPhotos&size=large"));</example>
		public string BuildURL(string parameter)
		{
			string p = "";
			if(!parameter.StartsWith("&")) 
			{
				p = "&" + parameter;
			}
			else
			{
				p = parameter;
			}
			return Config.GetTabURL(TabRef) + p;
		}

		public bool ModuleHasEditRights
		{
			get { return m_HasEditRights; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ModuleConfigFile
		{
			get
			{
				return ModulePhysicalPath + "/" + "Module_" + ModuleRef + ".config";
			}
		}

		public string ModuleConfigSchemaFile
		{
			get
			{
				return ModulePhysicalPath + "/" + "Module.xsd";
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public object ReadCommonConfig(System.Type t)
		{
			string fileName = ModulePhysicalPath + "/Module.config";
			
			if(!System.IO.File.Exists(fileName))
				return null;

			XmlTextReader xmlReader = null;
			XmlSerializer xmlSerial = new XmlSerializer(t);
			object o = null;
			try
			{
				xmlReader = new XmlTextReader(fileName);
				o = xmlSerial.Deserialize(xmlReader);
				xmlReader.Close();
			}
			catch(Exception e)
			{
				if(xmlReader != null)
				{
					xmlReader.Close();
				}
				// Do not throw exceptions
				Trace.Warn("Module", "Error loading Modules Common Config", e);
			}

			return o;
		}
		
		/// <summary>
		/// Reads the config file. The schema is optional.
		/// </summary>
		/// <returns>
		/// Null if config file does not exists and no schema exists,
		/// else the schema is read and a empty DataSet is returned.
		/// </returns>
		public DataSet ReadConfig()
		{
			DataSet ds = null;
			try
			{
				if(System.IO.File.Exists(ModuleConfigFile))
				{
					ds = new DataSet();
					if(System.IO.File.Exists(ModuleConfigSchemaFile))
					{
						ds.ReadXmlSchema(ModuleConfigSchemaFile);
					}
					ds.ReadXml(ModuleConfigFile);
				}
			}
			catch(Exception e)
			{
				// Do not throw exceptions
				Trace.Warn("Module", "Error loading Modules Config as Dataset", e);
				return null;
			}

			if(ds == null && System.IO.File.Exists(ModuleConfigSchemaFile))
			{
				ds = new DataSet();
				ds.ReadXmlSchema(ModuleConfigSchemaFile);
			}

			return ds;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public object ReadConfig(System.Type t)
		{
			return ReadConfig(t, ModuleConfigFile);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public object ReadConfig(System.Type t, string fileName)
		{
			if(!System.IO.File.Exists(fileName))
				return null;

			XmlTextReader xmlReader = null;
			XmlSerializer xmlSerial = new XmlSerializer(t);
			object o = null;
			try
			{
				xmlReader = new XmlTextReader(fileName);
				o = xmlSerial.Deserialize(xmlReader);
				xmlReader.Close();
			}
			catch(Exception e)
			{
				if(xmlReader != null)
				{
					xmlReader.Close();
				}
				// Do not throw exceptions
				Trace.Warn("Module", "Error loading Modules Config", e);
			}

			return o;
		}

		public void WriteConfig(DataSet ds)
		{
			try
			{
				ds.WriteXml(ModuleConfigFile);
			}
			catch(Exception e)
			{
				// Do not throw exceptions
				Trace.Warn("Module", "Error writing Modules Config Dataset", e);
			}
		}

		public void WriteConfig(object o)
		{
			WriteConfig(o, ModuleConfigFile);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="fileName"></param>
		public void WriteConfig(object o, string  fileName)
		{
			XmlTextWriter xmlWriter = null;
			XmlSerializer xmlSerial = new XmlSerializer(o.GetType());
			try
			{
				xmlWriter = new XmlTextWriter(fileName, System.Text.Encoding.UTF8);
				xmlWriter.Formatting = Formatting.Indented;
				xmlSerial.Serialize(xmlWriter, o);
				xmlWriter.Close();
			}
			catch(Exception e)
			{
				if(xmlWriter != null)
				{
					xmlWriter.Close();
				}
				// Do not throw exceptions
				Trace.Warn("Module", "Error writing Modules Config", e);
			}
		}
	}

	/// <summary>
	/// Base class for each Portal Edit Module Control. Derived from Module
	/// </summary>
	public class EditModule : Module
	{
		/// <summary>
		/// Returns the callers URL. 
		/// The return value depends on the portals render settings "Table or Frame" 
		/// and "UseTabHttpModule"
		/// </summary>
		/// <returns>The BackURL</returns>
		public string GetBackURL()
		{
			return Config.GetTabURL(TabRef);
		}

		/// <summary>
		/// Redirects back to the callers URL. Uses the GetBackURL() Method.
		/// </summary>
		public void RedirectBack()
		{
			Response.Redirect(GetBackURL());
		}
	}
}
