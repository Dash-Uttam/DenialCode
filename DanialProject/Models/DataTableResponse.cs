using System;
using System.Runtime.CompilerServices;

namespace DanialProject.Models
{
	public class DataTableResponse
	{
		public object[] data
		{
			get;
			set;
		}

		public int draw
		{
			get;
			set;
		}

		public int recordsFiltered
		{
			get;
			set;
		}

		public long recordsTotal
		{
			get;
			set;
		}

		public DataTableResponse()
		{
		}
	}
}