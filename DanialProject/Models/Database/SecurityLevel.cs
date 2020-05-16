using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class SecurityLevel
	{
		public int ID
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string levelName
		{
			get;
			set;
		}

		public SecurityLevel()
		{
		}
	}
}