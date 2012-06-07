Personal .NET Web Portal
========================

Introduction
------------
This is a .NET based Web Portal for personal use. The purpose is to build a Homepage easily. 
Pages are build through Tabs and Modules. Like the IBuySpyPortal, but free.


Install
-------
1) Unzip to the Web Applications folder.
1.1) If you are upgrading, watch out for your changed WebCustomControls or Pages!
1.2) Configuration Files, Stylesheet and some .ascx files are not replaced. a ".install" file is copied instead.
2) Create a Web Application in IIS (if not done yet).
3) Edit the web.config file.
4) View the Users.config file to look up the current valid accounts
5) Have fun!

Upgrade to 1.0
--------------
Since a XSD Schema has been added to the Quick Links Module the XML Schema has been changed.
The Root Tag is now "<QuickLinks xmlns="Module.xsd">". You have to change this.

Security
--------
Each User has Roles, minimum the Build in Role "User".
There are 4 Build in Roles:
	
	Admin - Administrator Role. May edit/view everything
	User - Signed in User
	Everyone - Signed in User or Anonymous User
	Anonymous - Not signed in User

Besides these Roles every Administrator may define own Roles ("Editor" for example).
Build in Roles should never be deleted!!


Foreign Controls
----------------

TreeCtrl:
	- by Iulian Iuga 
	- Found at codeproject.com (http://www.codeproject.com/useritems/TreeWeb.asp)

ImageBrowser:
	- by Dan Glass (http://www.danglass.com/Web/)
	- Found at codeproject.com (http://www.codeproject.com/useritems/ImageBrowser.asp)

HitCounter:
	- by JediBaron (?) - No Full Name provided
	- Found at codeproject.com (http://www.codeproject.com/aspnet/fastcustomhitcounter.asp?target=counter)

HtmlEditor:
	- by (?) - No Name provided
	- Project Name: Yet Another HTML Editor for .Net 
	- Found at gotdotnet.com (http://www.cwebrun.com)

Forum:
	- by Mike Puddephat
	- Project Name: Riverside Internet Forums
	- Fount at codeproject.com (http://www.codeproject.com/aspnet/aspnetforums.asp?target=forum)

Change Log
----------
(1.0.2) Beta 2
	- Forum
	- TabHttpHandler supports now URL Parameter

(1.0.1) Beta 1
	- Exeption Message: More Informations
	- throws Exceptions in DEBUG Mode
	- HasEditRight Property in Module
	- HtmlEdit Module - like the Html Module but with a Editor
	- BuildURL Method
	- Edit Portal Definition inplace (on the Tabs)

(1.0) First Release
	- Hit Counter Module
	- Validators in EditModule.ascx
	- ModuleFailed.ascx added
	- TabHttpHandler added
	- XSD Schema added to Module Config
	- News Module added
	- UrlReferrer logging
	- TabPath added

(0.6) Beta 2
	- Configuration Files renamed to .config
	- ImageBrowser Module added and adapted (http://www.codeproject.com/useritems/ImageBrowser.asp)
	- QuickLinks Module added
	- Design/Lock and Feel improved
	- "Make Installer" Batch files
	- Portal.API Assembly created (there where problems with referencing Web-Applications)
	- Read/Write Configuration Support for Modules

(0.5) Beta 1 (Bug Fix)
	- Fixed a bug in the HTML Edit Module. "The File could not be created."

(0.4) Beta 1
	- Admin Pages
	- WebSite Module added
	- TabList Module added
	- Table Module added

(0.3) 
	- Usereditor, Roleeditor
	
(0.2)
	- Security (Rolebased security added)
	- CSS

(Preview) Initial Verion
	- Render Table/Frames
	- Edit Modules
	- HTML Sample Module
