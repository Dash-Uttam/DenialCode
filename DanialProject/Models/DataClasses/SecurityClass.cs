using DanialProject.Models.Database;
using DanialProject.Models.Others;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web;

namespace DanialProject.Models.DataClasses
{
	public class SecurityClass
	{
		private static readonly AdminContext db;

		static SecurityClass()
		{
			SecurityClass.db = new AdminContext();
		}

		public SecurityClass()
		{
		}

		public static string GetAllAccessPass(int level)
		{
			string temp = "";
			try
			{
				List<LevelAccess> AllLevels = (
					from x in SecurityClass.db.LevelAccess
					where x.SecurityLevelID == (int?)level
					select x).ToList<LevelAccess>();
				foreach (LevelAccess details in AllLevels)
				{
					temp = string.Concat(temp, details.url, ",");
				}
			}
			catch (Exception exception)
			{
			}
			return temp;
		}

		public static List<LevelAccess> GetAllLevelAccess(int Id)
		{
			List<LevelAccess> li = new List<LevelAccess>();
			try
			{
				li = (
					from x in SecurityClass.db.LevelAccess
					where x.SecurityLevelID == (int?)Id
					select x).ToList<LevelAccess>();
			}
			catch (Exception exception)
			{
			}
			return li;
		}

		public static Admins GetLoginStatus(LoginViewModel lv)
		{
			Admins admin;
			Admins temp = new Admins();
			try
			{
				temp = SecurityClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.UserName.Equals(lv.Email) && x.Password.Equals(lv.Password));
				if (temp == null)
				{
					return temp;
				}
				else
				{
					admin = temp;
				}
			}
			catch (Exception exception)
			{
				admin = temp;
			}
			return admin;
		}

		public static List<SecurityLevel> GetSecurityLevel()
		{
			List<SecurityLevel> li = new List<SecurityLevel>();
			try
			{
				li = SecurityClass.db.SecurityLevel.ToList<SecurityLevel>();
			}
			catch (Exception exception)
			{
			}
			return li;
		}

		public static int PostLevelAccesses(LevelAccess LA)
		{
			int check = 0;
			try
			{
				IQueryable<LevelAccess> temp = 
					from x in SecurityClass.db.LevelAccess
					where x.SecurityLevelID == LA.SecurityLevelID
					select x;
				if (temp != null)
				{
					SecurityClass.db.LevelAccess.RemoveRange(temp);
					string[] strArrays = LA.url.Split(new char[] { ',' });
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						string details = strArrays[i];
						if (!string.IsNullOrWhiteSpace(details))
						{
							LevelAccess level = new LevelAccess()
							{
								SecurityLevelID = LA.SecurityLevelID,
								url = details
							};
							SecurityClass.db.LevelAccess.Add(level);
							SecurityClass.db.SaveChanges();
						}
					}
					check = LA.SecurityLevelID.Value;
				}
				else
				{
					string[] strArrays1 = LA.url.Split(new char[] { ',' });
					for (int j = 0; j < (int)strArrays1.Length; j++)
					{
						string details = strArrays1[j];
						if (!string.IsNullOrWhiteSpace(details))
						{
							LevelAccess level = new LevelAccess()
							{
								SecurityLevelID = LA.SecurityLevelID,
								url = details
							};
							SecurityClass.db.LevelAccess.Add(level);
							SecurityClass.db.SaveChanges();
						}
					}
					check = LA.SecurityLevelID.Value;
				}
			}
			catch (Exception exception)
			{
			}
			return check;
		}

		public static int PostSecurityLevel(SecurityLevel SL)
		{
			int check = 0;
			try
			{
				if (SecurityClass.db.SecurityLevel.FirstOrDefault<SecurityLevel>((SecurityLevel x) => x.levelName.Equals(SL.levelName)) == null)
				{
					SecurityClass.db.SecurityLevel.Add(SL);
					SecurityClass.db.SaveChanges();
					check = SL.ID;
				}
			}
			catch (Exception exception)
			{
			}
			return check;
		}

		public static void SessionRenew()
		{
			try
			{
				HttpCookie oldCookie = new HttpCookie("AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd")
				{
					Expires = DateTime.Now.AddDays(20)
				};
				HttpContext.Current.Request.Cookies.Add(oldCookie);
			}
			catch (Exception exception)
			{
			}
		}

		public static int UpdateSecurityLevel(SecurityLevel SL)
		{
			int check = 0;
			try
			{
				if (SecurityClass.db.SecurityLevel.FirstOrDefault<SecurityLevel>((SecurityLevel x) => x.levelName.Equals(SL.levelName)) == null)
				{
					SecurityLevel user = SecurityClass.db.SecurityLevel.FirstOrDefault<SecurityLevel>((SecurityLevel x) => x.ID.Equals(SL.ID));
					if (user != null)
					{
						user.levelName = SL.levelName;
						SecurityClass.db.Entry<SecurityLevel>(user).State = EntityState.Modified;
						SecurityClass.db.SaveChanges();
						check = SL.ID;
					}
				}
			}
			catch (Exception exception)
			{
			}
			return check;
		}
	}
}