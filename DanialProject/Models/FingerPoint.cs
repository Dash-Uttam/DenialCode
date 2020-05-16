using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace DanialProject.Models
{
	public class FingerPoint
	{
		public FingerPoint()
		{
		}

		public static string Tag(string rootRelaticePath)
		{
			if (HttpRuntime.Cache[rootRelaticePath] != null)
			{
				return HttpRuntime.Cache[rootRelaticePath] as string;
			}
			string absolute = HostingEnvironment.MapPath(string.Concat("~", rootRelaticePath));
			DateTime date = File.GetLastWriteTime(absolute);
			int index = rootRelaticePath.LastIndexOf('/');
			string result = rootRelaticePath.Insert(index, string.Concat("/v-1.5-", date.Ticks));
			return result;
		}
	}
}