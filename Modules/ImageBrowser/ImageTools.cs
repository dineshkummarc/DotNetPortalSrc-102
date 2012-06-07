using System;
using ImageBrowser.Entities;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ImageBrowser
{
	/// <summary>
	/// static helper methods
	/// </summary>
	public class ImageTools
	{
		/// <summary>
		/// 
		/// </summary>
		public ImageTools(Portal.API.Module m)
		{
			currentModule = m;
			cfg = (ImageBrowserConfig)currentModule.ReadConfig(typeof(ImageBrowserConfig));
		}

		public Portal.API.Module currentModule = null;
		public ImageBrowserConfig cfg = null;

		/// <summary>
		/// Gets a new SubDirectory Wrapper
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public SubDirectoryWrapper GetSubDirectoryWrapper(string dir)
		{
			//FIXME
			return new SubDirectoryWrapper(dir, currentModule.ModuleVirtualPath + "folder.jpg", this);
		}

		/// <summary>
		/// Gets a new Image Wrapper
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public ImageWrapper GetImageWrapper(string file)
		{
			return new ImageWrapper(file, this);
		}

		/// <summary>
		/// Helper function to get the file image path based in the virtual path
		/// </summary>
		/// <param name="queryPath"></param>
		/// <returns></returns>
		public string GetPath(string queryPath)
		{
			string path = "";

			if ( queryPath != null && queryPath.Length > 0 ) path = queryPath.Replace(@"\","/");

			if ( path.StartsWith(cfg.PictureVirtualDirectory) ) path = path.Substring(cfg.PictureVirtualDirectory.Length ,path.Length - cfg.PictureVirtualDirectory.Length );

			return path;
		}

		/// <summary>
		/// Creates a new image of the specified size from the source image
		/// </summary>
		/// <param name="src">Source image path</param>
		/// <param name="dest">Destination image path</param>
		/// <param name="width">With to resize the picture</param>
		/// <returns></returns>
		public string CreateImage( string src, string dest, int width )
		{
			string description = null;

			try
			{
				if ( File.Exists(cfg.PictureRootDirectory + "/" + dest) ) return description;

				string path = Directory.GetParent(cfg.PictureRootDirectory + "/" + dest).FullName;

				if ( ! Directory.Exists(path)) Directory.CreateDirectory(path);


				Image image = Image.FromFile(cfg.PictureRootDirectory + "/" + src);

				int y = (int)(((double)((double)width/(double)image.Size.Width)) * (double)image.Size.Height);
				
				Image thumb;

				
				if ( width > image.Width )// dont make it larger!
				{
					thumb = image;
				}
				else if ( width > 200 )// only do this if we need quality
				{
					thumb = Resize(new Bitmap(image),width,y,(bool)(width > 200));
				}
				else
				{
					//use dodgy MS call
					Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
					thumb = image.GetThumbnailImage(width,y,myCallback,IntPtr.Zero);
				}
				thumb.Save(cfg.PictureRootDirectory + "/" + dest,ImageFormat.Jpeg);
			}
			catch(Exception){}

			return description;
		}

		/// <summary>
		/// Bah! MS bug
		/// </summary>
		/// <returns></returns>
		public static bool ThumbnailCallback()
		{
			return false;
		}

		/// <summary>
		/// Taken from http://www.codeproject.com/cs/media/imageprocessing4.asp
		/// Christian Graus
		/// </summary>
		/// <param name="b">Image to resize</param>
		/// <param name="nWidth">Width to make it</param>
		/// <param name="nHeight">Height to make it</param>
		/// <param name="bBilinear">Whether to use the bilenear method (alot more cpu)</param>
		/// <returns></returns>
		public static Bitmap Resize(Bitmap b, int nWidth, int nHeight, bool bBilinear)
		{
			//Bitmap bTemp = (Bitmap)b.Clone();
			Bitmap bTemp = b;

			b = new Bitmap(nWidth, nHeight, bTemp.PixelFormat);

			double nXFactor = (double)bTemp.Width/(double)nWidth;
			double nYFactor = (double)bTemp.Height/(double)nHeight;

			if (bBilinear)
			{
				double fraction_x, fraction_y, one_minus_x, one_minus_y;
				int ceil_x, ceil_y, floor_x, floor_y;
				Color c1 = new Color();
				Color c2 = new Color();
				Color c3 = new Color();
				Color c4 = new Color();
				byte red, green, blue;

				byte b1, b2;

				for (int x = 0; x < b.Width; ++x)
					for (int y = 0; y < b.Height; ++y)
					{
						// Setup

						floor_x = (int)Math.Floor(x * nXFactor);
						floor_y = (int)Math.Floor(y * nYFactor);
						ceil_x = floor_x + 1;
						if (ceil_x >= bTemp.Width) ceil_x = floor_x;
						ceil_y = floor_y + 1;
						if (ceil_y >= bTemp.Height) ceil_y = floor_y;
						fraction_x = x * nXFactor - floor_x;
						fraction_y = y * nYFactor - floor_y;
						one_minus_x = 1.0 - fraction_x;
						one_minus_y = 1.0 - fraction_y;

						c1 = bTemp.GetPixel(floor_x, floor_y);
						c2 = bTemp.GetPixel(ceil_x, floor_y);
						c3 = bTemp.GetPixel(floor_x, ceil_y);
						c4 = bTemp.GetPixel(ceil_x, ceil_y);

						// Blue
						b1 = (byte)(one_minus_x * c1.B + fraction_x * c2.B);

						b2 = (byte)(one_minus_x * c3.B + fraction_x * c4.B);
            
						blue = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

						// Green
						b1 = (byte)(one_minus_x * c1.G + fraction_x * c2.G);

						b2 = (byte)(one_minus_x * c3.G + fraction_x * c4.G);
            
						green = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

						// Red
						b1 = (byte)(one_minus_x * c1.R + fraction_x * c2.R);

						b2 = (byte)(one_minus_x * c3.R + fraction_x * c4.R);
            
						red = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

						b.SetPixel(x,y, System.Drawing.Color.FromArgb(255, red, green, blue));
					}
			}
			else
			{
				for (int x = 0; x < b.Width; ++x)
					for (int y = 0; y < b.Height; ++y)
						b.SetPixel(x, y, bTemp.GetPixel((int)(Math.Floor(x * nXFactor)),
							(int)(Math.Floor(y * nYFactor))));
			}

			return b;
		}
	}
}
