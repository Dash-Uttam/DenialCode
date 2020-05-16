using System;
using System.Runtime.CompilerServices;
using System.Web.Http.ModelBinding;

namespace DanialProject.Models
{
	[ModelBinder(typeof(DataTableModelBinder))]
	public class DataTableRequest
	{
		public DataTableColumn[] Columns
		{
			get;
			set;
		}

		public int Draw
		{
			get;
			set;
		}

		public int Length
		{
			get;
			set;
		}

		public DataTableOrder[] Order
		{
			get;
			set;
		}

		public DataTableSearch Search
		{
			get;
			set;
		}

		public int Start
		{
			get;
			set;
		}

		public DataTableRequest()
		{
		}
	}
}