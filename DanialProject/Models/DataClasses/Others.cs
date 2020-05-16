using DanialProject.Models.Database;
using System;
using System.Globalization;
using System.Web;

namespace DanialProject.Models.DataClasses
{
	public class Others
	{
		public Others()
		{
		}

		public static string Check_OP(string op)
		{
			try
			{
				string[] split_Op = op.Split(new char[] { '.' });
				if (int.Parse(split_Op[1].ToString()[0].ToString()) <= 0)
				{
					op = string.Concat(split_Op[0], "%");
				}
			}
			catch (Exception exception)
			{
			}
			return op;
		}

		public static FilterFeatures FilterFeatures(FilterFeatures filterFeatures)
		{
			bool noFee;
			DateTime now;
			try
			{
				if (filterFeatures.Brough == null)
				{
					HttpCookie cookie = HttpContext.Current.Request.Cookies["FilterFeaturesDeatils"];
					if (cookie != null)
					{
						cookie.Value = "Select One&&false&&0&0&&false&false";
						now = DateTime.Now;
						cookie.Expires = now.AddDays(1);
						HttpContext.Current.Response.Cookies.Set(cookie);
					}
					else
					{
						HttpCookie httpCookie = new HttpCookie("FilterFeaturesDeatils")
						{
							Value = "Select One&&false&&0&0&&false&false"
						};
						now = DateTime.Now;
						httpCookie.Expires = now.AddDays(1);
						HttpContext.Current.Response.Cookies.Add(httpCookie);
					}
				}
				else
				{
					HttpCookie cookie = HttpContext.Current.Request.Cookies["FilterFeaturesDeatils"];
					if (cookie != null)
					{
						string neighborArray = "";
						for (int i = 0; i < (int)filterFeatures.Neighborhood.Length; i++)
						{
							neighborArray = string.Concat(neighborArray, filterFeatures.Neighborhood[i], ",");
						}
						string featureArray = "";
						for (int i = 0; i < (int)filterFeatures.Array.Length; i++)
						{
							featureArray = string.Concat(featureArray, filterFeatures.Array[i], ",");
						}
						object[] brough = new object[19];
						brough[0] = filterFeatures.Brough;
						brough[1] = "&";
						brough[2] = neighborArray;
						brough[3] = "&";
						noFee = filterFeatures.NoFee;
						brough[4] = noFee.ToString();
						brough[5] = "&";
						brough[6] = filterFeatures.AvailibilityDate;
						brough[7] = "&";
						brough[8] = filterFeatures.Guarantors;
						brough[9] = "&";
						brough[10] = filterFeatures.PassengerElevators;
						brough[11] = "&";
						brough[12] = filterFeatures.FrieghtElevators;
						brough[13] = "&";
						brough[14] = featureArray;
						brough[15] = "&";
						noFee = filterFeatures.CasebyCase;
						brough[16] = noFee.ToString();
						brough[17] = "&";
						noFee = filterFeatures.Under30lbs;
						brough[18] = noFee.ToString();
						cookie.Value = string.Concat(brough);
						now = DateTime.Now;
						cookie.Expires = now.AddDays(1);
						HttpContext.Current.Response.Cookies.Set(cookie);
					}
					else
					{
						string neighborArray = "";
						for (int i = 0; i < (int)filterFeatures.Neighborhood.Length; i++)
						{
							neighborArray = string.Concat(neighborArray, filterFeatures.Neighborhood[i], ",");
						}
						string featureArray = "";
						for (int i = 0; i < (int)filterFeatures.Array.Length; i++)
						{
							featureArray = string.Concat(featureArray, filterFeatures.Array[i], ",");
						}
						HttpCookie httpCookie1 = new HttpCookie("FilterFeaturesDeatils");
						object[] str = new object[19];
						str[0] = filterFeatures.Brough;
						str[1] = "&";
						str[2] = neighborArray;
						str[3] = "&";
						noFee = filterFeatures.NoFee;
						str[4] = noFee.ToString();
						str[5] = "&";
						str[6] = filterFeatures.AvailibilityDate;
						str[7] = "&";
						str[8] = filterFeatures.Guarantors;
						str[9] = "&";
						str[10] = filterFeatures.PassengerElevators;
						str[11] = "&";
						str[12] = filterFeatures.FrieghtElevators;
						str[13] = "&";
						str[14] = featureArray;
						str[15] = "&";
						noFee = filterFeatures.CasebyCase;
						str[16] = noFee.ToString();
						str[17] = "&";
						noFee = filterFeatures.Under30lbs;
						str[18] = noFee.ToString();
						httpCookie1.Value = string.Concat(str);
						now = DateTime.Now;
						httpCookie1.Expires = now.AddDays(1);
						HttpContext.Current.Response.Cookies.Add(httpCookie1);
					}
				}
			}
			catch (Exception exception)
			{
			}
			return filterFeatures;
		}

		public static FilterBox GetFilterSession()
		{
			FilterBox filter = new FilterBox();
			try
			{
				string data = HttpContext.Current.Request.Cookies["FilterBoxCookiesInDallienWebsite"].Value;
				string[] split = data.Split(new char[] { '&' });
				filter.Filter = split[0];
				filter.From_price = split[1];
				filter.To_price = split[2];
				filter.Bed = split[3];
				filter.Bath = split[4];
			}
			catch (Exception exception)
			{
				filter.Filter = "Rent";
				filter.From_price = "0";
				filter.To_price = "10000";
				filter.Bed = "0+";
				filter.Bath = ".5+";
			}
			return filter;
		}

		public static double Getlatitude(double latitude, double dy)
		{
			return latitude + dy / 6378 * 57.3248407643312;
		}

		public static double Getlongitude(double longitude, double latitude, double dx)
		{
			double ans_dx_pi = dx / 6378 * 57.3248407643312;
			double New_long = longitude + ans_dx_pi / Math.Cos(latitude * 3.14 / 180);
			return New_long;
		}

		public static void SetFilterSession(FilterBox filter)
		{
			DateTime now;
			if (filter.Filter == null)
			{
				HttpCookie cookie = HttpContext.Current.Request.Cookies["FilterBoxCookiesInDallienWebsite"];
				if (cookie != null)
				{
					cookie.Value = "Rent&$0&$10000&0+&.5+";
					now = DateTime.Now;
					cookie.Expires = now.AddDays(1);
					HttpContext.Current.Response.Cookies.Set(cookie);
				}
				else
				{
					HttpCookie httpCookie = new HttpCookie("FilterBoxCookiesInDallienWebsite")
					{
						Value = "Rent&$0&$10000&0+&.5+"
					};
					now = DateTime.Now;
					httpCookie.Expires = now.AddDays(1);
					HttpContext.Current.Response.Cookies.Add(httpCookie);
				}
				FilterFeatures filterFeatures = new FilterFeatures()
				{
					Brough = null
				};
				DanialProject.Models.DataClasses.Others.FilterFeatures(filterFeatures);
				return;
			}
			HttpCookie cookie1 = HttpContext.Current.Request.Cookies["FilterBoxCookiesInDallienWebsite"];
			if (cookie1 != null)
			{
				cookie1.Value = string.Concat(new string[] { filter.Filter, "&", filter.From_price, "&", filter.To_price, "&", filter.Bed, "&", filter.Bath });
				now = DateTime.Now;
				cookie1.Expires = now.AddDays(1);
				HttpContext.Current.Response.Cookies.Set(cookie1);
				return;
			}
			HttpCookie httpCookie1 = new HttpCookie("FilterBoxCookiesInDallienWebsite")
			{
				Value = string.Concat(new string[] { filter.Filter, "&", filter.From_price, "&", filter.To_price, "&", filter.Bed, "&", filter.Bath })
			};
			now = DateTime.Now;
			httpCookie1.Expires = now.AddDays(1);
			HttpContext.Current.Response.Cookies.Add(httpCookie1);
		}

		public static FilterBoxDouble ValueToDouble()
		{
			double number;
			FilterBoxDouble filterBoxDouble = new FilterBoxDouble();
			try
			{
				FilterBox filterBox = DanialProject.Models.DataClasses.Others.GetFilterSession();
				filterBoxDouble.Filter = filterBox.Filter;
				NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol | NumberStyles.Integer | NumberStyles.Number;
				CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
				if (double.TryParse(filterBox.From_price, style, culture, out number))
				{
					filterBoxDouble.From_price = number;
				}
				if (double.TryParse(filterBox.To_price, style, culture, out number))
				{
					filterBoxDouble.To_price = number;
				}
				if (!filterBox.Bath.Contains("+"))
				{
					filterBoxDouble.Bath = Convert.ToDouble(filterBox.Bath);
					filterBoxDouble.BathPlus = false;
				}
				else
				{
					string[] bathvalue = filterBox.Bath.Split(new char[] { '+' });
					filterBoxDouble.Bath = Convert.ToDouble(bathvalue[0]);
					filterBoxDouble.BathPlus = true;
				}
				if (!filterBox.Bed.Contains("+"))
				{
					filterBoxDouble.Bed = Convert.ToDouble(filterBox.Bed);
					filterBoxDouble.BedPlus = false;
				}
				else
				{
					string[] bedValue = filterBox.Bed.Split(new char[] { '+' });
					filterBoxDouble.Bed = Convert.ToDouble(bedValue[0]);
					filterBoxDouble.BedPlus = true;
				}
			}
			catch (Exception)
			{
			}
			return filterBoxDouble;
		}
	}
}