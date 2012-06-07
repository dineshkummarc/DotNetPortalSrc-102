//--------------------------------------------------------------------------------
//Product
//		AWS File Picker 1.0
//Author
//		Alex Avrutin (alex@awsystems.spb.ru)
//Company
//		Advanced Web Systems (http://awsystems.spb.ru)
//											Join The Progress!
//Version 
//		1.0
//Description:
//		This components reminds the "Open File" dialog in windows apps. It let users
//		navigate an upload folder and its subfolders on the server and select a file. 
//		Also, user can upload files to the selected folder, create subfolders and
//		delete them if necessary.
//		You can easily adjust the permissions you grant each person. You can forbid
//		uploading files, deleting them, creating folders. You also can limit size of
//		files that can be uploaded and restrict allowed types of files. For example, 
//		you can let users ability to upload only pictures (gifs and jpgs) with the 
//		size not more then 50Kb.
//Credits:
//		The idea of this component and a pair of routines inside :-) were inspired by  
//		Tim Mackey's Web Based File Manager (www.scootasp.net). This is a very interesting
//		and useful web application, however I did not like how Tim built the  
//		list of files (Tom, you know, there is a great DataGrid control, isn't it? :-).
//		Anyway, thank you a lot. It is a really good app, especially for its zero price. 
//		Also I'd like to thank Peter Blum (www.PeterBlum.com) who showed how a Real Web 
//		Control must be built with his DateTextBox control (also, a very robust and free 
//		one). I borrowed here the framework (code for main routines and properties for
//		rendering and pop up) and a few lines of JavaScript code (can't stand client 
//		programming, you know ;-).
//This file:
//		A page that isa loaded in pop up window and fulfills the main machinery for the app
//		(file browsing, uploading, restrictions and so on).



using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.IO;
using System.Security;



namespace AWS
{

	public class FilePickerPage : System.Web.UI.Page 
	{

		protected TextBox NewFolderText;
		protected Button NewFolderBtn;
		protected Button UploadBtn;
		protected HtmlInputFile fileToUpload;
		protected Label Header;
		protected ImageButton GoRoot;
		protected TextBox setRootTxt;
		protected ImageButton UpBtn;
		protected System.Web.UI.WebControls.Image	imgFSObject;
		protected DataGrid dgFSList;
		protected PlaceHolder UploadArea;
		protected PlaceHolder CreateDirArea;


		bool _AllowUpload = false;
		bool _AllowCreateDirs = false;
		bool _AllowDelete = false;
   
		private void Page_Load(object sender, System.EventArgs e) 
		{
//			DirectoryInfo mainDir; //information on the current directory (if is)

			// add  "/" at end of base upload path if doesn't exist			
			Session["fpUploadDir"] =  AddSlashIfNotExist(Session["fpUploadDir"].ToString());

			//Find enabled actions for the current user
			if (Session["fpAllowUpload"] != null) 
				_AllowUpload = Convert.ToBoolean(Session["fpAllowUpload"]);
			if (Session["fpAllowCreateDirs"] != null)
				_AllowCreateDirs = Convert.ToBoolean(Session["fpAllowCreateDirs"]);
			if (Session["fpAllowDelete"]!= null )
				_AllowDelete = Convert.ToBoolean(Session["fpAllowDelete"]);
			
			//Disable a control if an action disabled
			UploadArea.Visible = _AllowUpload;
			CreateDirArea.Visible = _AllowCreateDirs;
			//Delete file action will be disabled later in the dgFSList_ItemDataBound procedure
			//as we don't have access to Del button now.
			
			if (!IsPostBack)
			{
				//First, check if we have value telling what path we should return - 
				//relative to application directory or relative to Upload directory
				//if no, set default value (relative to application directory)
				if (Session["fpUseAppRelPath"] == null) 
				{
					Session["fpUseAppRelPath"] = false;	
				}

				//If it is the firs load, try to get a directory to follow into 
				//from TextBox on the parent page
				Session["fpRelativePath"]= ""; //just cleaning up..

			}

			// the current relative path (to the base dir)
			//is stored in Session["fpRelativePath"], if it isn't set, set it 
			if(Session["fpRelativePath"] == null)
				Session["fpRelativePath"] = "";	//default is base upload directory

			showFiles();	// list the files and folders in the current directory
		}
		
		///<summary>Get a list with file systems objects (files and dirs),
		///bind it to the DataGrid. Also, hide or show the navigation buttons (up/root)
		///and set the header.</summary>
		private void showFiles()	
		{

			if(Session["fpRelativePath"].ToString() != "") 
			{	
				Header.Text = "Showing contents of <b>" + Session["fpRelativePath"].ToString() + "</b> directory.";				
				UpBtn.Visible = true;		// this button has no context in the root dir
				GoRoot.Visible = true;		// same
			}
			else
			{
				Header.Text = "Showing contents of <B>root directory.</b>";	
				UpBtn.Visible = false;		// this button has no context in the root dir
				GoRoot.Visible = false;		// same
			}

			//safety checks handled by ValidatePath function
			DirectoryInfo mainDir = ValidatePath(Session["fpUploadDir"].ToString() + Session["fpRelativePath"].ToString());			
			if(mainDir == null)	return;	// bail out

		dgFSList.DataSource = mainDir.GetFileSystemInfos();
			dgFSList.DataBind();
		}
		
		///<summary>Invoked when the DataGrid binds a row. Acquire and set a file/dir information.
		///</summary>
		private void dgFSList_ItemDataBound(Object sender, DataGridItemEventArgs e) 
		{
			String _FileSizeStr;

			//only process non-header or footer items
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				
				//fill in common file/folder data
				//Trace.Warn(((FileSystemInfo)e.Item.DataItem).Extension);
				((LinkButton)e.Item.FindControl("lbFileName")).Text = e.Item.DataItem.ToString();
				((Label)e.Item.FindControl("lblModified")).Text = ((FileSystemInfo)e.Item.DataItem).LastWriteTime.ToString();
				//Disable delete action if this is forbidden for the current user
				((LinkButton)e.Item.FindControl("imbDel")).Visible = _AllowDelete;
				System.Web.UI.WebControls.Image FSOImage = ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgFSObject"));
				Session["fpRelativePath"] = AddSlashIfNotExist(Session["fpRelativePath"].ToString());
				((HyperLink)e.Item.FindControl("hlView")).NavigateUrl = Session["fpUploadDir"].ToString() + Session["fpRelativePath"].ToString() + e.Item.DataItem.ToString();
				
				//Add delete confirmation
				LinkButton imbDel = (LinkButton)e.Item.FindControl("imbDel");
				String js = "return confirm('Do you want to proceed?');";
				imbDel.Attributes["onclick"] = js;
				
				//Data depending on whether DataGrid works with file or folder
				switch (e.Item.DataItem.GetType().ToString()) 
				{
					case "System.IO.DirectoryInfo": 
					{
						//a directory
						FSOImage.ImageUrl = "/images/folder.gif";
						FSOImage.AlternateText = "Folder";
						((Label)e.Item.FindControl("lblSize")).Text = "Dir";
						e.Item.Attributes.Add("FSOType", "Folder");
						break;
					}
					case "System.IO.FileInfo": 
					{
						//a file
						FSOImage.ImageUrl = "/images/file.gif";
						FSOImage.AlternateText = "File";
						_FileSizeStr = GetFileSize(((FileInfo)e.Item.DataItem).Length);
						((Label)e.Item.FindControl("lblSize")).Text = _FileSizeStr;
						e.Item.Attributes.Add("FSOType", "File");						
						break;
					}
				}
			}
		}
		
		///<summary>Invoked when an user clicked a file(folder) or a Del button 
		private void dgFSList_ItemCommand(Object sender, DataGridCommandEventArgs e) 
		{			
			if (e.CommandName == "FSOClick") 
			{
				String FileName = ((LinkButton)e.Item.FindControl("lbFileName")).Text;
				// add  "/" at end of relative path if it doesn't exist				
				Session["fpRelativePath"] = AddSlashIfNotExist(Session["fpRelativePath"].ToString());
				//if user clicked file or folder name
				switch (e.Item.Attributes["FSOType"]) 
				{								
					case "Folder": 
					{
						//if we work with directory, follow into it
						ChangePath(FileName);
						showFiles();
						break;
					}

					case "File": 
					{
						//if we work with file, select this file	
						SelectFile(Session["fpRelativePath"].ToString() + FileName);
						break;
					}
				}
			}

			else if (e.CommandName == "Del") 
			{
				String FileName = ((LinkButton)e.Item.FindControl("lbFileName")).Text;

				switch (e.Item.Attributes["FSOType"]) 
				{
					case "File": 
					{					
						FileInfo file = new FileInfo(Server.MapPath(Session["fpUploadDir"].ToString() + Session["fpRelativePath"].ToString()+ FileName));
						try	
						{
							file.Delete();
						}
						catch //(Exception ex)
						{
							ReportError("Cannot delete the file.",  "The file can be busy, in use by an application n\\or you do not have write permissions for the current directory or for the file.");
						}						
						break;
					}

					case "Folder": 
					{
						try	
						{
							DirectoryInfo dir = new DirectoryInfo(Server.MapPath(Session["fpUploadDir"].ToString() + Session["fpRelativePath"].ToString()+FileName));
							dir.Delete(true); //Delete recursive							
						}
						catch	//(Exception ex)	
						{
							ReportError("Could not delete folder.",  "Check that the folder exists and you have \\nwrite permissions for the current directory.");
						}
						break;
					}
				}
				showFiles();
			}
		}
		
		///<summary>Invoked when an user clicked the Create a New Directory button.</summary>
		private void NewFolderBtn_Click(object sender, System.EventArgs e) 
		{
			if(this.NewFolderText.Text == "")	// user gave no folder name
				ReportError("No folder name given for new folder.", "Please type in a new folder name.");
			else
			{
				try
				{
					DirectoryInfo dir = new DirectoryInfo(Server.MapPath(Session["fpUploadDir"].ToString() + Session["fpRelativePath"].ToString() + "/" + NewFolderText.Text));
					dir.Create();
					showFiles();
				}
				catch		//(Exception ex)
				{
					ReportError("Cannot not create folder: " + this.NewFolderText.Text.ToString(),  "Ensure you have write permissions for the current directory");
				}
			}
		}
		
		///<summary>Invoked when an user clicked the Upload button.</summary>
		private void UploadBtn_Click(object sender, System.EventArgs e)	
		{
			
			bool IsFileAllowed = false;			
			if(fileToUpload.PostedFile.FileName == "")	// no file selected
				ReportError("No file selected for upload.", "Please click Browse and select the file you wish to upload.");
			else
			{
				try
				{
					//check if it is allowed to upload file with these extensions and size					
					string filename = fileToUpload.PostedFile.FileName;	
					long fileSize = fileToUpload.PostedFile.ContentLength;
						
					//If a limit by exts set
					if (Session["fpAllowedUploadFileExts"] != null) 
					{
						String AllowedUploadFileExts = Session["fpAllowedUploadFileExts"].ToString();
						if (AllowedUploadFileExts!="") 
						{
							//get an array of allowed exts
							Char[] cSplit = new Char[1];
							cSplit[0]=',';	
							String[] AllowedExts = AllowedUploadFileExts.Split(cSplit);

							//Check if the file extension conforms the restrictions
							if (Path.HasExtension(filename)) 
							{
								String UploadFileExt = Path.GetExtension(filename);
								//Remove a dot from the file extension
								UploadFileExt = UploadFileExt.Replace(".", "");
								UploadFileExt = UploadFileExt.Trim();
								foreach (String Extension in AllowedExts) 
								{
									//Remove a dot form a restricting extension (if is)
									String CleanExtension = Extension.Replace(".", "");
									CleanExtension = CleanExtension.Trim();
									if (UploadFileExt.ToLower() == CleanExtension.ToLower())
									{
										IsFileAllowed = true;
										break;
									}
								}
							}
						}
						else 
						{
							IsFileAllowed = true;
						}
					}
					else 
					{
						IsFileAllowed = true;
					}
					
					if (!IsFileAllowed)	 
					{	
						ReportError("Sorry, this file type is not served.", "Please, upload files with the following expensions:\\n" + Session["fpAllowedUploadFileExts"].ToString());
					}

					//Check if uploaded file conforms the size limit
					if (Session["fpUploadSizeLimit"] != null) 
					{
						long UploadSizeLimit = Convert.ToInt64(Session["fpUploadSizeLimit"]);
						if (UploadSizeLimit>0) 
						{
							if (fileSize > UploadSizeLimit) 
							{
								Response.Write("<script>alert(\"Too large file. Max file size is " + GetFileSize(UploadSizeLimit) + ".\"); </script>");
								IsFileAllowed = false;
							}
						}
					}
					
					//if everything is O'k
					if (IsFileAllowed) 
					{
						String FileSizeStr = GetFileSize(fileSize);
						string togo = Session["fpUploadDir"] + Session["fpRelativePath"].ToString() + "/" + filename.Remove(0, filename.LastIndexOf("\\") + 1);
						fileToUpload.PostedFile.SaveAs(Server.MapPath(togo));	 					
						showFiles();
					}
				}
				catch //(Exception ex)
				{
					ReportError("Upload failed.",  "The file may be too large or \\naccess to the upload directory denied.");
				}
			}
		}
		
		///<summary>Invoked when an user changes a page.</summary>
		private void dgFSList_PageChaged(Object sender, DataGridPageChangedEventArgs e) 
		{
			dgFSList.CurrentPageIndex = e.NewPageIndex;
			Trace.Warn(e.NewPageIndex.ToString());
			showFiles();

		}

		///<summary>Prepares a string representation of a file size.</summary>
		private String GetFileSize(long Lenght) 
		{
			if(Lenght > 1000000) return ((Lenght/1000000).ToString() + " Mb");
			else if(Lenght > 1000) return ((Lenght/1000).ToString() + " Kb");
			else return (Lenght.ToString() + " b");

		}

		///<summary>Adds a slash in the end of a path if it does not exist. </summary>
		private String AddSlashIfNotExist(String str) 
		{
			if(str.LastIndexOf("/") != str.Length-1)
			{	
				str += "/";
			}
			return(str);
		}


		///<summary>Changes a path.</summary>
		private void ChangePath (String Direction) 
		{				
			if(Direction == "/")	// goto root
				Session["fpRelativePath"] = "";
			else if(Direction == "../") // go one level up in directory tree
				Session["fpRelativePath"] = getParentDirectory(Session["fpRelativePath"].ToString());
			else	// add the directory name to the end of the current path
				Session["fpRelativePath"] = Session["fpRelativePath"].ToString() + Direction + "/";			

		}

		///<summary>Returns a name of a file that user selected into the parent TextBox.</summary>
		public void SelectFile(String FileName) 
		{
			if ((bool)Session["fpUseAppRelPath"] == true) 
			{
				FileName = (String)Session["fpUploadDir"] + FileName; 
			}

			if (!Page.IsStartupScriptRegistered("UpdateCaller")) 
			{
				StringBuilder strjscript = new StringBuilder("<script language='javascript'>\n", 2000);
				//strjscript.Append("function StartupScript() {\n");
				strjscript.Append("if (window.opener == null) ");
				strjscript.Append("  { window.close(); }\n");
				strjscript.Append("else \n");
				strjscript.Append("{\n");
				strjscript.Append("  var vCallerDocument = window.opener.document;\n  var vItem;\n");
				strjscript.Append("  vItem = vCallerDocument.getElementById('" + Request.QueryString["TextBoxID"] + "');\n");
				strjscript.Append("  vItem.value = '" + FileName + "';\n");
				strjscript.Append("}\n");
				strjscript.Append("window.close();\n");
				//strjscript.Append("}\n");  // function end
				strjscript.Append("\n</script>");

				Page.RegisterStartupScript("UpdateCaller", strjscript.ToString());
			}
		} 

		///<summary>Checks if a path valid and accessible.</summary>
		private DirectoryInfo ValidatePath(String Path)
		{
			// called by showFiles(), does checks to see if directory to be opened is valid
			DirectoryInfo mainDir;
			try
			{
				mainDir = new DirectoryInfo(Server.MapPath(Path));
				if(mainDir.Exists == true)
					return mainDir;	// no problems
				else	// directory does not exist
					ReportError(@"The directory " + Server.MapPath(Path).Replace(@"\", @"\\") + " does not exist.",  "If you set the site root manually, try resetting it to the current directory: ./");
			}
			catch (DirectoryNotFoundException e)
			{
				ReportError("The path could not be found.",  "Try going to the root directory.");
				//Use the exception variable somehow to avoid warnings during compilation
				e.ToString();
			}
			
			catch (SecurityException e)
			{
				ReportError("You do not have permissions to view this directory.", "Please, or contact support service.");
				//Use the exception variable somehow to avoid warnings during compilation
				e.ToString();
			}
			catch (ArgumentException e)
			{
				ReportError("The path has invalid characters.",  "Try renaming the file and removing non-standard characters.");
				//Use the exception variable somehow to avoid warnings during compilation
				e.ToString();
			}
			catch (Exception e)
			{
				ReportError("Could not get directory information.",  "Try manually resetting the root path to ./");
				//Use the exception variable somehow to avoid warnings during compilation
				e.ToString();
			}
			return null;
		}

		///<summary>Returns a parent directory for a given directory.</summary>
		private string getParentDirectory(String RelativePath)
		{	
			// this function works for /main/db/test/ as well as /main/db/test.aspx
			System.Web.HttpContext context = System.Web.HttpContext.Current;
			if(RelativePath == "./")
				return("../");	// trivial, no string manipulation required
			else if(RelativePath == "")
				return("");	// can't go higher than root
			else
			{
				// remove trailing "/" at end of path
				if(RelativePath.LastIndexOf("/") == RelativePath.Length-1)
				{	
					RelativePath = RelativePath.Remove(RelativePath.LastIndexOf("/"), (RelativePath.Length - RelativePath.LastIndexOf("/")));
				}
				try
				{
					// remove the characters after the last occurence of / in the string. => parent directory
					RelativePath = RelativePath.Remove(RelativePath.LastIndexOf("/"), (RelativePath.Length - RelativePath.LastIndexOf("/")));
					return(RelativePath);
				}
				catch
				{
					return("");	// default to root;
				}
			}			
		}

		///<summary>Displays an error message.</summary>
		private void ReportError(string problem, string suggestion)
		{
			// outputs error, in english, and in tech, and with any suggestions.
			System.Web.HttpContext context = System.Web.HttpContext.Current;
			StringBuilder strjscript = new StringBuilder("<script language='javascript'>\n");
			strjscript.Append("var Msg='There is a problem: \\n';\n");
			strjscript.Append("Msg +='" + problem + "\\n';\n");
			strjscript.Append("Msg +='Suggestion: \\n" + suggestion + "\\n';\n");
			strjscript.Append("alert(Msg);\n");
			strjscript.Append("</script>");
			context.Response.Write(strjscript.ToString());
		}

		///<summary>Invoked when an user clicked the Up button.</summary>
		private void UpBtn_Click(Object sender, ImageClickEventArgs e)
		{
			ChangePath("../");
			showFiles();
		}

		///<summary>Invoked when an user clicked the Go Root button.</summary>
		private void GoRoot_Click(Object sender, ImageClickEventArgs e)
		{
			ChangePath("/");
			showFiles();
		}
		    
		///<summary>Invoked when a page initializes.</summary>
		override protected void OnInit(EventArgs e) 
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{ 
			this.UpBtn.Click += new System.Web.UI.ImageClickEventHandler(this.UpBtn_Click);
			this.GoRoot.Click += new System.Web.UI.ImageClickEventHandler(this.GoRoot_Click);
			this.dgFSList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgFSList_ItemCommand);
			this.dgFSList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgFSList_PageChaged);
			this.dgFSList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgFSList_ItemDataBound);
			this.UploadBtn.Click += new System.EventHandler(this.UploadBtn_Click);
			this.NewFolderBtn.Click += new System.EventHandler(this.NewFolderBtn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	}
}