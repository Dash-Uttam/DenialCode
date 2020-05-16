using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class FilterBox
	{
		[MaxLength(100)]
		public string Bath
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Bed
		{
			get;
			set;
		}

		[MaxLength(25)]
		public string Filter
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string From_price
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string To_price
		{
			get;
			set;
		}

		public FilterBox()
		{
		}
	}
}