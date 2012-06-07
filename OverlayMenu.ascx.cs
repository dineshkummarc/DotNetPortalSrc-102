namespace Portal
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public class OverlayMenuItem
	{
		private string text = "";
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		private string icon = "";
		public string Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		private int menuItemIndex = -1;
		public int MenuItemIndex
		{
			get { return menuItemIndex; }
			set { menuItemIndex = value; }
		}

		public event EventHandler Click = null;
		public void InvokeClick()
		{
			if(Click != null)
			{
				Click(this, new EventArgs());
			}
		}
	}

	public class OverlayMenuSeparatorItem : OverlayMenuItem
	{
	}

	public class OverlayMenuItemCtrlBuilder : System.Web.UI.ControlBuilder
	{
		public override Type GetChildControlType(String tagName,
			IDictionary attributes)
		{
			if (String.Compare(tagName, "MenuItem", true) == 0) 
			{
				return typeof(OverlayMenuItem);
			}
			if (String.Compare(tagName, "SeparatorItem", true) == 0) 
			{
				return typeof(OverlayMenuSeparatorItem);
			}
			return null;
		}
	}

	/// <summary>
	///		Summary description for OverlayMenu.
	/// </summary>
	[ControlBuilder(typeof(OverlayMenuItemCtrlBuilder)), ParseChildren(false)]
	public abstract class OverlayMenu : System.Web.UI.UserControl, IPostBackEventHandler
	{
		public string RootText = "";

		ArrayList miList = new ArrayList();
		public ArrayList Items
		{
			get { return miList; }
		}

		protected Repeater MenuRepeater;

		protected override void AddParsedSubObject(object obj)
		{
			OverlayMenuItem m = obj as OverlayMenuItem;
			if(m != null)
			{
				m.MenuItemIndex = miList.Add(m);
			}
			else
			{
				base.AddParsedSubObject(obj);
			}
		}

		public void RaisePostBackEvent(string args)
		{
			int i = Int32.Parse(args);
			OverlayMenuItem mi = (OverlayMenuItem)miList[i];
			mi.InvokeClick();
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			MenuRepeater.DataSource = miList;
			MenuRepeater.DataBind();
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
