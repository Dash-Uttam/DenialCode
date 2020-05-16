using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class OpenDates
	{
		public DanialProject.Models.Database.Buildings Buildings
		{
			get;
			set;
		}

		[ForeignKey("Buildings")]
		public int? BuildingsID
		{
			get;
			set;
		}

		public DateTime Date
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		public DateTime Time1
		{
			get;
			set;
		}

		public DateTime Time2
		{
			get;
			set;
		}

		public DateTime Time3
		{
			get;
			set;
		}

		public OpenDates()
		{
		}
	}
}