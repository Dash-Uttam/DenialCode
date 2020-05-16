using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class ImagesList
	{
		public List<Images> list
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

		[MaxLength(50)]
		[NotMapped]
		public string Type
		{
			get;
			set;
		}

		public ImagesList()
		{
		}
	}
}