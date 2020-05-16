using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Web;

namespace DanialProject.Models.Database
{
	public class Features
	{
		[Display(Name="Icon:")]
		[DanialProject.Models.Database.FileExtensions("jpg,jpeg,png,JPG,JPEG,PNG", ErrorMessage="Only Image files allowed.")]
		[NotMapped]
		public HttpPostedFileBase File
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Icon
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string IconType
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
		public string ListingType
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Name
		{
			get;
			set;
		}

		public Features()
		{
		}
	}
}