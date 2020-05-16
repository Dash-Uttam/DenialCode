using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class B64Image
	{
		public int Id
		{
			get;
			set;
		}

		public string Image
		{
			get;
			set;
		}

		public int ImageId
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string ImagePath
		{
			get;
			set;
		}

		[MaxLength(10)]
		public string Type
		{
			get;
			set;
		}

		public B64Image()
		{
		}
	}
}