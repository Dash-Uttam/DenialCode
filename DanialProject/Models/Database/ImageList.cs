using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class ImageList
	{
		public List<B64Image> B64Images
		{
			get;
			set;
		}

		public int BuildingId
		{
			get;
			set;
		}

		[MaxLength(10)]
		public string Category
		{
			get;
			set;
		}

		public ImageList()
		{
		}
	}
}