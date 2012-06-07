using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Security;
using System.Security.Principal;
using Portal.API;

namespace Portal
{
	/// <summary>
	/// Helper Class for the Portals User Management.
	/// </summary>
	public class UserManagement
	{
		/// <summary>
		/// Returns the Users Dataset
		/// </summary>
		/// <returns>Users Dataset</returns>
		public static Users GetUsers()
		{
			Users u = new Users();
			u.ReadXml(Config.GetUserListPhysicalPath());
			return u;
		}

		/// <summary>
		/// Saves the Users Dataset
		/// </summary>
		/// <param name="u">Users Dataset</param>
		public static void SetUsers(Users u)
		{
			u.WriteXml(Config.GetUserListPhysicalPath());
		}

		/// <summary>
		/// Performs the Login.
		/// </summary>
		/// <param name="account">Users account</param>
		/// <param name="password">Users password</param>
		/// <returns>true if the credentials are valid</returns>
		public static bool Login(string account, string password)
		{
			Users u = GetUsers();

			Users.UserRow user = u.User.FindBylogin(account.ToLower());
			if(user == null) return false;
			if(user.password != password) return false;

			FormsAuthentication.SetAuthCookie(account, false);
			return true;
		}

		/// <summary>
		/// Returns the current Users Roles.
		/// </summary>
		/// <param name="account">Users account</param>
		/// <returns>string array of the users roles. Returns a empty array if the user is not found</returns>
		public static string[] GetRoles(string account)
		{
			Users u = GetUsers();

			Users.UserRow user = u.User.FindBylogin(account.ToLower());
			if(user == null) return new string[0];

			Users.UserRoleRow[] roles = user.GetUserRoleRows();
			string[] result = new string[roles.Length];
			for(int i=0;i<roles.Length;i++)
			{
				result[i] = roles[i].RoleRow.name;
			}

			return result;
		}

		/// <summary>
		/// Saves a single User. Do not use this Method in combination with GetUsers/SetUsers!
		/// </summary>
		/// <param name="account">Users Account. If it does not exists a new User is created</param>
		/// <param name="password">Users password</param>
		/// <param name="firstName">Users First Name</param>
		/// <param name="surName">Users Sur Name</param>
		/// <param name="roles">ArrayList of Roles</param>
		public static void SaveUser(string account, string password, string firstName, string surName, ArrayList roles)
		{
			Users u = GetUsers();

			Users.UserRow user = u.User.FindBylogin(account.ToLower());
			if(user == null)
			{
				user = u.User.AddUserRow(account, password, firstName, surName);
			}
			else
			{
				user.password = password;
				user.firstName = firstName;
				user.surName = surName;
			}

			// Delete old Roles
			foreach(Users.UserRoleRow r in user.GetUserRoleRows())
			{
				r.Delete();
			}

			// Add new Roles
			foreach(string newRole in roles)
			{
				u.UserRole.AddUserRoleRow(u.Role.FindByname(newRole), user);
			}
			

			SetUsers(u);
		}

		/// <summary>
		/// Deletes a single user. Do not use this Method in combination with GetUsers/SetUsers!
		/// </summary>
		/// <param name="account"></param>
		public static void DeleteUser(string account)
		{
			Users u = GetUsers();
			Users.UserRow user = u.User.FindBylogin(account.ToLower());
			if(user == null)
			{
				throw new Exception("User not found");
			}
			string a = account.ToLower();
			if(account.ToLower() == API.Config.AdminRole.ToLower())
			{
				throw new Exception("Deleteing Admin Role is not allowed!");
			}
			user.Delete();
			SetUsers(u);
		}

		/// <summary>
		/// Checks if a user has View Rights on a Tab or Module
		/// </summary>
		/// <param name="user">User Principal Object</param>
		/// <param name="roles">ArrayList with the users Roles</param>
		/// <returns>true if the user has View Rights</returns>
		public static bool HasViewRights(IPrincipal user, ArrayList roles)
		{
			if(user.IsInRole(Config.AdminRole)) return true;

			foreach(PortalDefinition.Role r in roles)
			{
				if(r.name == Config.EveryoneRole) return true;
				if(r.name == Config.AnonymousRole && !user.Identity.IsAuthenticated) return true;
				if(r.name == Config.UserRole && user.Identity.IsAuthenticated) return true;
				if(user.IsInRole(r.name))
				{
					return true;
				}
			}
			
			return false;
		}
		/// <summary>
		/// Checks if a user has Edit Rights on a Tab or Module
		/// </summary>
		/// <param name="user">User Principal Object</param>
		/// <param name="roles">ArrayList with the users Roles</param>
		/// <returns>true if the user has Edit Rights</returns>
		public static bool HasEditRights(IPrincipal user, ArrayList roles)
		{
			if(user.IsInRole(Config.AdminRole)) return true;

			foreach(PortalDefinition.Role r in roles)
			{
				PortalDefinition.EditRole er = r as PortalDefinition.EditRole;
				if(er != null)
				{
					if(er.name == Config.EveryoneRole) return true;
					if(er.name == Config.AnonymousRole && !user.Identity.IsAuthenticated) return true;
					if(r.name == Config.UserRole && user.Identity.IsAuthenticated) return true;
					if(user.IsInRole(er.name))
					{
						return true;
					}
				}
			}
			
			return false;
		}
	}
}
