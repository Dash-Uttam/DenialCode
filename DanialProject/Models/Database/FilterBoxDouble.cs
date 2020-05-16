using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class FilterBoxDouble
	{
		public double Bath
		{
			get;
			set;
		}

		public bool BathPlus
		{
			get;
			set;
		}

		public double Bed
		{
			get;
			set;
		}

		public bool BedPlus
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

		public double From_price
		{
			get;
			set;
		}

		public double To_price
		{
			get;
			set;
		}

		public FilterBoxDouble()
		{
		}
	}
}