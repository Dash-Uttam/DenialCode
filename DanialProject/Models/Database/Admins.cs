using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class Admins
	{
		public DateTime? AddDate
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string FullName
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Image
		{
			get;
			set;
		}

		[MaxLength(11)]
		public string Occupation
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Password
		{
			get;
			set;
		}

		public DanialProject.Models.Database.SecurityLevel SecurityLevel
		{
			get;
			set;
		}

		[ForeignKey("SecurityLevel")]
		public int? SecurityLevelID
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string UserName
		{
			get;
			set;
		}

		public Admins()
		{
		}
	}
}