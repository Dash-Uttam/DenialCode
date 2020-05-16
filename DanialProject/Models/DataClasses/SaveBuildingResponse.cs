using System;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.DataClasses
{
	public class SaveBuildingResponse
	{
		public int BuildingId
		{
			get;
			set;
		}

		public int CookieId
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		public SaveBuildingResponse()
		{
		}
	}
}