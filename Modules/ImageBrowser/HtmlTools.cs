using System;
using System.Web.UI.WebControls;
using System.Drawing;

using ImageBrowser.Entities;

namespace ImageBrowser
{
	/// <summary>
	/// static helper methods
	/// </summary>
	public class HtmlTools
	{
		/// <summary>
		/// Dont want anyone to instantiate
		/// </summary>
		private HtmlTools(){}

		/// <summary>
		/// This creates a Table object with all the thumbnails in it
		/// </summary>
		/// <param name="x">Pictures wide</param>
		/// <param name="y">Puctures down (not used)</param>
		/// <param name="data">The directory to render</param>
		/// <param name="url">The URL to use in the links</param>
		/// <returns></returns>
		public static Table RenderImageTable(int x, int y, DirectoryWrapper data, /*string url*/System.Web.UI.Control ctrl)
		{
			Table table = new Table();
			table.Width = Unit.Percentage(100);

			TableRow tr = null;

			foreach ( ImageWrapper image in data.Images )
			{
				if ( tr == null ) tr = new TableRow();

				HyperLink h = new HyperLink();
				h.ImageUrl = image.ThumbHref;
				h.NavigateUrl = ctrl.Page.GetPostBackClientHyperlink(ctrl, "picture;" + image.FullImageHref);
				h.Text = image.Name;
				h.CssClass = "LinkButton";

				Label lbText = new Label();
				lbText.Text = image.Blurb;


				TableCell td = new TableCell();
				td.Attributes.Add("align", "center");
				td.Controls.Add(h);
				if(lbText.Text != "")
				{
					lbText.Text = @"<div align=""center"" width=""100%"">" + lbText.Text + @"</div>";
					td.Controls.Add(lbText);
				}
				tr.Cells.Add(td);

				if ( tr.Cells.Count == x )
				{
					table.Rows.Add(tr);
					tr = null;
				}
			}

			if ( tr != null ) table.Rows.Add(tr);

			return table;
		}

		/// <summary>
		///  This creates a Table object with all the subdirectories in it
		/// </summary>
		/// <param name="x">Subdirectories wide</param>
		/// <param name="data">The directory to render</param>
		/// <param name="url">The URL to use in the links</param>
		/// <returns></returns>
		public static Table RenderDirectoryTable(int x, DirectoryWrapper data/*, string url*/, System.Web.UI.Control ctrl)
		{
			Table table = new Table();
			table.CellPadding = 10;
			table.CellSpacing = 10;
			table.Width = Unit.Percentage(100);

			TableRow tr = null;

			foreach ( SubDirectoryWrapper dir in data.Directories )
			{
				if ( tr == null ) tr = new TableRow();

				TableCell td = new TableCell();
				td.Attributes["align"] = "center";

				HyperLink h = new HyperLink();

				h.ImageUrl = dir.Src;
				h.CssClass = "LinkButton";
				h.NavigateUrl = ctrl.Page.GetPostBackClientHyperlink(ctrl, "directory;" + dir.HREF);
				h.Text = dir.Name;

				td.Controls.Add(h);
				
				td.Controls.Add(BR);

				h = new HyperLink();
				h.CssClass = "LinkButton";
				h.NavigateUrl = ctrl.Page.GetPostBackClientHyperlink(ctrl, "directory;" + dir.HREF);
				h.Text = dir.Name;
				

				td.Controls.Add(h);

				tr.Cells.Add(td);

				if ( tr.Cells.Count == x )
				{
					table.Rows.Add(tr);
					tr = null;
				}
			}

			if ( tr != null ) table.Rows.Add(tr);

			return table;
		}
		

		/// <summary>
		/// Outputs some navigation links to the page.
		/// </summary>
		/// <param name="controlCollection">the pages' contols</param>
		/// <param name="path">The path of the current image directory being browsed</param>
		/// <param name="url">The URL to use in the links</param>
		public static void RendenderLinkPath(System.Web.UI.ControlCollection controlCollection, string path, System.Web.UI.Control ctrl, ImageBrowserConfig cfg)
		{

			HyperLink h = null;
			Literal l = null;

			if ( path != null && path.Length > 0 )
				path = path.Replace(@"\","/");
			else
			{
				h = new HyperLink();
				h.NavigateUrl = "";
				h.Text = cfg.RootName;
				h.Attributes.Add("class","LinkButton");
				controlCollection.Add(h);
				return;
			}

			string[] paths = path.Split('/');

			paths[0] = cfg.RootName;

			for ( int i = 1; i <= paths.Length; i++ )
			{

				if ( i < paths.Length )
				{
					h = new HyperLink();
					h.NavigateUrl = ctrl.Page.GetPostBackClientHyperlink(ctrl, "directory;" + string.Join("/",paths,0,i).Replace(cfg.RootName,""));
					h.Text = paths[i-1];
					h.Attributes.Add("class","LinkButton");
					controlCollection.Add(h);

					l = new Literal();
					l.Text = " &raquo; \n";
					controlCollection.Add(l);
				}
				else
				{
					h = new HyperLink();
					h.NavigateUrl = "";
					h.Text = paths[i-1];
					h.Attributes.Add("class","LinkButton");
					controlCollection.Add(h);
				}
			}
		}

		/// <summary>
		/// Helper property
		/// </summary>
		public static Literal BR
		{
			get
			{
				Literal l = new Literal();
				l.Text = "<BR>";
				return l;
			}
		}

		/// <summary>
		/// Helper property
		/// </summary>
		public static Literal HR
		{
			get
			{
				Literal l = new Literal();
				l.Text = "<div width=\"100%\" style=\"border-bottom: solid 1px black;font-size: 5px;\">&nbsp;</div>";
				return l;
			}
		}
	}
}
