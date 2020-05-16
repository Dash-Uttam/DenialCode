using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Web;

namespace DanialProject.Models.Database
{
	public class Transportation
	{
		public Buildings Buildings
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

		[Display(Name="Icon:")]
		[FileExtensions("jpg,jpeg,png,JPG,JPEG,PNG", ErrorMessage="Only Image files allowed.")]
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

		public int ID
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

		public Transportation()
		{
		}
	}
}