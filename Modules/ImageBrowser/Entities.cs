using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Collections;

namespace ImageBrowser.Entities
{
	/// <summary>
	/// Holds all the information of the contents of a directoty
	/// </summary>
	public class DirectoryWrapper
	{
		private ArrayList directories =  new ArrayList();
		private ArrayList images =  new ArrayList();
		private string directory;
		private ImageTools imageTools = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dir">Directory to hold contents of</param>
		public DirectoryWrapper(string dir, ImageTools imgTools)
		{
			imageTools = imgTools;
			directory = dir;

			// add the sub-directories
			foreach ( string s in Directory.GetDirectories(imageTools.cfg.PictureRootDirectory + "/" + directory) )
			{
				string[] path = s.Replace( "\\", "/" ).Split('/');

				if ( path[path.Length - 1] != "thumbs" && path[path.Length - 1] != "webpics" && path[path.Length - 1][0] != '_' )
				{
					directories.Add(imageTools.GetSubDirectoryWrapper(directory + "/" + path[path.Length - 1]));
				}
			}

			// add pictures
			foreach ( string s in Directory.GetFiles(imageTools.cfg.PictureRootDirectory + "/" + directory) )
			{
				string[] path = s.Replace(@"\","/").Split('/');
				string fileName = path[path.Length - 1];
				if (  fileName[0] != '_' )
				{
					string extension = null;
					if (fileName.IndexOf(".") > 0)
					{
						string[] parts = fileName.Split('.');
						extension = parts[parts.Length - 1];
					}
					if ( extension == null ) continue;

					extension = extension.ToLower();

					if ( extension == "jpg" ||
						extension == "png" ||
						extension == "gif" )
					{
						images.Add(imageTools.GetImageWrapper(directory + "/" + fileName));
					}
				}
			}
		}

		/// <summary>
		/// Subdirectories
		/// </summary>
		public ArrayList Directories
		{
			get
			{
				return directories;
			}
		}

		/// <summary>
		/// images
		/// </summary>
		public ArrayList Images
		{
			get
			{
				return images;
			}
		}

		/// <summary>
		/// The name of this directory
		/// </summary>
		public string Name
		{
			get
			{
				string[] paths = directory.Replace(@"\","/").Split('/');
				return paths[paths.Length - 1];
			}
		}
		
		/// <summary>
		/// The blurb - loaded from a file with the same name as the directory,
		/// in the directory
		/// </summary>
		public string Blurb
		{
			get
			{
				FileStream fs = null;
				try
				{
					if ( File.Exists(imageTools.cfg.PictureRootDirectory + "/" + directory + "/_dirtext.txt" ) )
					{
						fs = File.OpenRead(imageTools.cfg.PictureRootDirectory + "/" + directory + "/_dirtext.txt" );

						byte[] b = new Byte[fs.Length];
						fs.Read(b,0,(int)fs.Length);
						fs.Close();
						return System.Text.ASCIIEncoding.ASCII.GetString(b);
					}
				}
				catch(Exception)
				{
					if ( fs != null && fs.CanRead ) fs.Close();
				}
				return "";
			}
			set
			{
				FileStream fs = null;
				try
				{
					fs = new FileStream(imageTools.cfg.PictureRootDirectory + "/" + directory + "/_dirtext.txt", 
						FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
					fs.SetLength(0); // Truncate
					StreamWriter sw = new StreamWriter(fs);
					sw.Write(value);
					sw.Close();
				}
				finally
				{
					if(fs != null)
					{
						fs.Close();
					}
				}				
			}
		}
	}

	/// <summary>
	/// Wraps a directory object ( not to be confused with the directory that your in.
	/// </summary>
	public class SubDirectoryWrapper
	{
		private string directory;
		private string name;
		private string defaultImage;
		private ImageTools imageTools = null;

		public SubDirectoryWrapper(string dir, string defaultFolderImage, ImageTools imgTools)
		{
			directory = dir;
			defaultImage = defaultFolderImage;
			imageTools = imgTools;

			string[] dirs = directory.Replace(@"\","/").Split('/');
			name = dirs[dirs.Length - 1];
		}

		/// <summary>
		/// Name of the subdirectory
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Imahe to use to represent the subdirectory
		/// </summary>
		public string Src
		{
			get
			{
				string parent = imageTools.cfg.PictureRootDirectory + @"\" + directory;
				string[] files = Directory.GetFiles( parent, @"_dirimage.*");

				if ( files.Length > 0 )
				{
					string file = null;

					foreach ( string s in files )
					{
						string extension = null;
						if (s.IndexOf(".") > 0)
						{
							string[] parts = s.Split('.');
							extension = parts[parts.Length - 1];
						}
						if ( extension == null ) continue;

						extension = extension.ToLower();

						if ( extension == "jpg" ||
							extension == "png" ||
							extension == "gif" )
						{
							string[] filepath = s.Replace(@"\","/").Split('/');
							file = directory + "/" + filepath[filepath.Length - 1];
							break;
						}
					}
					if ( file != null )
					{
						string[] filename = file.Replace(@"\",@"/").Split('/');

						string thumb = string.Join("/",filename,0,filename.Length - 1) + "/thumbs/" + filename[filename.Length - 1];

						imageTools.CreateImage(file,thumb,100);
						return imageTools.cfg.PictureVirtualDirectory + thumb;
					}
				}
				return defaultImage;
			}
		}

		/// <summary>
		/// The link to use to get to the subdirectory
		/// </summary>
		public string HREF
		{
			get
			{
				return imageTools.cfg.PictureVirtualDirectory + directory;
			}
		}

	}

	/// <summary>
	/// Wraps an image object for when you ar looking at it in a directory
	/// </summary>
	public class ImageWrapper
	{
		private string file;
		private string name;
		private ImageTools imageTools = null;

		public ImageWrapper(string fileName, ImageTools imgTools)
		{
			imageTools = imgTools;
			file = fileName;
			string[] dirs = file.Replace(@"\","/").Split('/');
			name = dirs[dirs.Length - 1];

		}
	
		/// <summary>
		/// Name of the file
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Link to use when they click on it
		/// This will take them to a 'web friendly' version of the image
		/// </summary>
		public string WebImageHref
		{
			get
			{
				string[] name = file.Split('/');

				string thumb = string.Join("/",name,0,name.Length - 1) + "/webpics/" + name[name.Length - 1];

				imageTools.CreateImage(imageTools.GetPath(file),imageTools.GetPath(thumb),640);
				
				return imageTools.cfg.PictureVirtualDirectory + imageTools.GetPath(thumb);
			}
		}

		/// <summary>
		/// The path of the orgional image
		/// </summary>
		public string FullImageHref
		{
			get
			{
				return imageTools.cfg.PictureVirtualDirectory + imageTools.GetPath(file);
			}
		}

		/// <summary>
		/// The path of the thumbnail
		/// </summary>
		public string ThumbHref
		{
			get
			{
				string[] name = file.Split('/');

				string thumb = string.Join("/",name,0,name.Length - 1) + "/thumbs/" + name[name.Length - 1];

				imageTools.CreateImage(imageTools.GetPath(file),imageTools.GetPath(thumb),100);

				return imageTools.cfg.PictureVirtualDirectory + imageTools.GetPath(thumb);
			}
		}
		/// <summary>
		/// The blurb - loaded from a file with the same name as the directory,
		/// in the directory
		/// </summary>
		public string Blurb
		{
			get
			{
				FileStream fs = null;
				try
				{
					string fileName = imageTools.cfg.PictureRootDirectory + @"\" + file.Replace("/", "\\") + ".txt";
					if ( File.Exists(fileName ) )
					{
						fs = File.OpenRead(fileName);

						byte[] b = new Byte[fs.Length];
						fs.Read(b,0,(int)fs.Length);
						fs.Close();
						return System.Text.ASCIIEncoding.ASCII.GetString(b);
					}
				}
				catch(Exception)
				{
					if ( fs != null && fs.CanRead ) fs.Close();
				}
				return "";
			}
			set
			{
				FileStream fs = null;
				try
				{
					fs = new FileStream(imageTools.cfg.PictureRootDirectory + @"\" + file.Replace("/", "\\") + ".txt", 
						FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
					fs.SetLength(0); // Truncate
					StreamWriter sw = new StreamWriter(fs);
					sw.Write(value);
					sw.Close();
				}
				finally
				{
					if(fs != null)
					{
						fs.Close();
					}
				}				
			}
		}
	}
}
