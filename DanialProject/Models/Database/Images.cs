using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class Images
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

		public int ID
		{
			get;
			set;
		}

		[NotMapped]
		public bool No_Fee
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Path
		{
			get;
			set;
		}

		[MaxLength(50)]
		[NotMapped]
		public string Type
		{
			get;
			set;
		}

		public Images()
		{
		}
	}
}