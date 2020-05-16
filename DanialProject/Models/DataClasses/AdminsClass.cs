using DanialProject.Models.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace DanialProject.Models.DataClasses
{
	public class AdminsClass
	{
		private static readonly AdminContext db;

		static AdminsClass()
		{
			AdminsClass.db = new AdminContext();
		}

		public AdminsClass()
		{
		}

		public static int AddRecord(Admins admin)
		{
			int Status = 0;
			try
			{
				if (AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.UserName.Equals(admin.UserName)) == null)
				{
					try
					{
						TimeZoneInfo.GetSystemTimeZones();
						TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
						DateTime time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);
						admin.AddDate = new DateTime?(time);
					}
					catch (TimeZoneNotFoundException timeZoneNotFoundException)
					{
						admin.AddDate = new DateTime?(DateTime.Now);
					}
					AdminsClass.db.Admins.Add(admin);
					AdminsClass.db.SaveChanges();
					Status = admin.ID;
				}
			}
			catch (Exception exception)
			{
			}
			return Status;
		}

		public static DataTableResponse AdminTable(DataTableRequest request)
		{
			object value;
			List<string[]> li = new List<string[]>();
			int filteredRows = 0;
			try
			{
				HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
				string cookiePath = ticket.CookiePath;
				DateTime expiration = ticket.Expiration;
				bool expired = ticket.Expired;
				bool isPersistent = ticket.IsPersistent;
				DateTime issueDate = ticket.IssueDate;
				string CookieId = ticket.Name;
				string userData = ticket.UserData;
				int version = ticket.Version;
				if (!expired)
				{
					using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminContext"].ConnectionString))
					{
						SqlCommand cmd = new SqlCommand("sP_Admins", con)
						{
							CommandType = CommandType.StoredProcedure
						};
						SqlParameter sqlParameter = new SqlParameter()
						{
							ParameterName = "@DisplayLength",
							Value = request.Length
						};
						cmd.Parameters.Add(sqlParameter);
						SqlParameter sqlParameter1 = new SqlParameter()
						{
							ParameterName = "@DisplayStart",
							Value = request.Start
						};
						cmd.Parameters.Add(sqlParameter1);
						SqlParameter sqlParameter2 = new SqlParameter()
						{
							ParameterName = "@SortCol",
							Value = request.Order[0].Column
						};
						cmd.Parameters.Add(sqlParameter2);
						SqlParameter sqlParameter3 = new SqlParameter()
						{
							ParameterName = "@SortDir",
							Value = request.Order[0].Dir
						};
						cmd.Parameters.Add(sqlParameter3);
						SqlParameter sqlParameter4 = new SqlParameter()
						{
							ParameterName = "@Search"
						};
						if (string.IsNullOrEmpty(request.Search.Value))
						{
							value = null;
						}
						else
						{
							value = request.Search.Value;
						}
						sqlParameter4.Value = value;
						cmd.Parameters.Add(sqlParameter4);
						con.Open();
						SqlDataReader rdr = cmd.ExecuteReader();
						while (rdr.Read())
						{
							string[] CP = new string[] { string.Concat("<img src='", rdr["Image"].ToString(), "' class='img-circle' style='width:60px !important; height:60px !important; margin-left:25px;'>"), rdr["Occupation"].ToString(), rdr["FullName"].ToString(), rdr["UserName"].ToString(), "**********", null, null, null };
							try
							{
								DateTime dateTime = DateTime.Parse(rdr["addDate"].ToString());
								CP[6] = dateTime.ToShortDateString();
							}
							catch (Exception exception)
							{
								CP[6] = rdr["addDate"].ToString();
							}
							if (CP[1] != "MasterAdmin")
							{
								CP[5] = rdr["levelName"].ToString();
								CP[7] = string.Concat("<button type='button' class='btn btn-link' onclick='Edit(\"", rdr["ID"].ToString(), "\");' data-toggle='tooltip' title='Edit'><i class='fa fa-edit'></i></button>");
								if (userData.Contains("MasterAdmin"))
								{
									CP[7] = string.Concat(CP[7], "<button type='button' class='btn btn-link' onclick='Delete(\"", rdr["ID"].ToString(), "\");' data-toggle='tooltip' title='Delete'><i class='fa fa-trash'></i></button>");
								}
							}
							else
							{
								CP[7] = string.Concat("<button type='button' class='btn btn-link' onclick='Edit(\"", rdr["ID"].ToString(), "\");' data-toggle='tooltip' title='Edit'><i class='fa fa-edit'></i></button>");
								CP[5] = "";
							}
							li.Add(CP);
							filteredRows = int.Parse(rdr["TotalCount"].ToString());
						}
					}
				}
			}
			catch (Exception exception1)
			{
			}
			return new DataTableResponse()
			{
				draw = request.Draw,
				recordsTotal = (long)filteredRows,
				recordsFiltered = filteredRows,
				data = li.ToArray()
			};
		}

		public static int DeleteRecord(int Id)
		{
			int Status = 0;
			try
			{
				Admins temp = AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.ID == Id);
				AdminsClass.db.Admins.Remove(temp);
				AdminsClass.db.SaveChanges();
				Status = 1;
			}
			catch (Exception exception)
			{
				Status = 0;
			}
			return Status;
		}

		public static Admins GetCurrentUserInfo()
		{
			Admins admin = new Admins();
			try
			{
				HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
				string cookiePath = ticket.CookiePath;
				DateTime expiration = ticket.Expiration;
				bool expired = ticket.Expired;
				bool isPersistent = ticket.IsPersistent;
				DateTime issueDate = ticket.IssueDate;
				string CookieId = ticket.Name;
				string userData = ticket.UserData;
				int version = ticket.Version;
				if (!expired)
				{
					int num = int.Parse(CookieId);
					admin = AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.ID == num);
					admin.SecurityLevel = AdminsClass.db.SecurityLevel.FirstOrDefault<SecurityLevel>((SecurityLevel x) => x.ID == admin.SecurityLevelID.Value);
				}
			}
			catch (Exception exception)
			{
			}
			return admin;
		}

		public static Admins GetRecord(int id)
		{
			Admins temp = new Admins();
			try
			{
				temp = AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.ID == id);
			}
			catch (Exception exception)
			{
			}
			return temp;
		}

		public static void PostYourDetailsImage()
		{
			try
			{
				HttpCookie authCookie = HttpContext.Current.Request.Cookies["AdminEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
				string cookiePath = ticket.CookiePath;
				DateTime expiration = ticket.Expiration;
				bool expired = ticket.Expired;
				bool isPersistent = ticket.IsPersistent;
				DateTime issueDate = ticket.IssueDate;
				string CookieId = ticket.Name;
				string userData = ticket.UserData;
				int version = ticket.Version;
				if (!expired)
				{
					string typeOfImage = "";
					if (HttpContext.Current.Request.Files.AllKeys.Any<string>())
					{
						int num = int.Parse(HttpContext.Current.Request["ImageId"].ToString());
						HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["Picture"];
						string httpPostedFile1 = httpPostedFile.FileName;
						string[] httpPostedFile2 = httpPostedFile1.Split(new char[] { '.' });
						typeOfImage = string.Concat(".", httpPostedFile2[(int)httpPostedFile2.Length - 1]);
						string Date = DateTime.Now.ToString();
						Date = Date.Replace(" ", string.Empty);
						Date = Date.Replace("/", string.Empty);
						Date = Date.Replace(":", string.Empty);
						if (httpPostedFile != null)
						{
							WebImage img = new WebImage(httpPostedFile.InputStream);
							img.Save(string.Concat("~\\Content\\Images\\Admin\\", num.ToString(), Date, typeOfImage), null, true);
						}
						try
						{
							string image = string.Concat("../../Content/Images/Admin/", num.ToString(), Date, typeOfImage.ToString());
							Admins user = AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.ID == num);
							if (user != null)
							{
								user.Image = image;
								AdminsClass.db.Entry<Admins>(user).State = EntityState.Modified;
								AdminsClass.db.SaveChanges();
							}
						}
						catch (Exception exception)
						{
						}
					}
				}
			}
			catch (Exception exception1)
			{
			}
		}

		public static int UpdateRecord(Admins UA)
		{
			int Status = 0;
			try
			{
				if (AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.UserName.Equals(UA.UserName) && x.ID != UA.ID) == null)
				{
					Admins user = AdminsClass.db.Admins.FirstOrDefault<Admins>((Admins x) => x.ID == UA.ID);
					if (user != null)
					{
						if (user.Occupation != "MasterAdmin")
						{
							user.FullName = UA.FullName;
							user.UserName = UA.UserName;
							user.SecurityLevelID = UA.SecurityLevelID;
							if (!string.IsNullOrWhiteSpace(UA.Password))
							{
								user.Password = UA.Password;
							}
							AdminsClass.db.Entry<Admins>(user).State = EntityState.Modified;
							AdminsClass.db.SaveChanges();
							Status = user.ID;
						}
						else
						{
							user.FullName = UA.FullName;
							user.UserName = UA.UserName;
							if (!string.IsNullOrWhiteSpace(UA.Password))
							{
								user.Password = UA.Password;
							}
							AdminsClass.db.Entry<Admins>(user).State = EntityState.Modified;
							AdminsClass.db.SaveChanges();
							Status = user.ID;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
			return Status;
		}
	}
}