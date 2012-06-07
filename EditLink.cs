using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Portal.API;

namespace Portal
{
	/// <summary>
	/// UserControl for the EditLink. Derived form HtmlAnchor. 
	/// Link redirects either to EditPageTable.aspx or EditPageFrame.apsx
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:EditLink runat=server></{0}:EditLink>")]
	public class EditLink : HtmlAnchor
	{
		/// <summary>
		/// The Modules reference
		/// </summary>
		internal string ModuleRef = "";

		/// <summary>
		/// Mono needs that
		/// </summary>
		public string Text = "";

		protected override void OnLoad(EventArgs args)
		{
			base.OnLoad(args);
			if(Text != "")
			{
				// Mono needs that
				InnerText = Text;
			}
			HRef = Helper.GetEditLink(ModuleRef);
		}
	}
}
