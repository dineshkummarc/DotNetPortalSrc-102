using System;
using System.Xml;
using System.Xml.Serialization;
using System.Web;

namespace ImageBrowser
{
	[XmlRoot("ImageBrowser")]
	public class ImageBrowserConfig
	{
		/// <summary>
		/// The virtual directory of the images
		/// ie. /pics
		/// </summary>
		[XmlElement("PictureVirtualDirectory")]
		public string PictureVirtualDirectory = "";

		/// <summary>
		/// Where the virtual directory maps to on disk
		/// </summary>
		[XmlIgnore]
		public string PictureRootDirectory
		{
			get { return HttpContext.Current.Server.MapPath(PictureVirtualDirectory); }
		}

		[XmlElement("RootName")]
		public string RootName = "My Pictures";
	}
}
