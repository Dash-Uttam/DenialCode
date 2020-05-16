using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class LevelAccess
	{
		public int ID
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

		[MaxLength(100)]
		public string url
		{
			get;
			set;
		}

		public LevelAccess()
		{
		}
	}
}