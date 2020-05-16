using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Web;

namespace DanialProject.Models.Database
{
	public class Agents
	{
		[MaxLength(15)]
		public string Category
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		[Display(Name="Email")]
		[EmailAddress(ErrorMessage="Invalid email address.")]
		[MaxLength(50)]
		public string EmailField
		{
			get;
			set;
		}

		[MaxLength(500)]
		public string Facebook
		{
			get;
			set;
		}

		[Display(Name="Agent Picture:")]
		[DanialProject.Models.Database.FileExtensions("jpg,jpeg,png,JPG,JPEG,PNG", ErrorMessage="Only Image files allowed.")]
		[NotMapped]
		public HttpPostedFileBase File
		{
			get;
			set;
		}

		[Display(Name="First Name")]
		[MaxLength(50)]
		[Required(ErrorMessage="First Name is required.")]
		[StringLength(50, MinimumLength=3, ErrorMessage="Must have min length of 3 and max Length of 50")]
		public string FirstName
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string HomeTown
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		[MaxLength(500)]
		public string Instagram
		{
			get;
			set;
		}

		[MaxLength(500)]
		public string Interests
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Languages
		{
			get;
			set;
		}

		[Display(Name="Last Name")]
		[MaxLength(50)]
		[Required(ErrorMessage="Last Name is required.")]
		[StringLength(50, MinimumLength=3, ErrorMessage="Must have min length of 3 and max Length of 50")]
		public string LastName
		{
			get;
			set;
		}

		[DataType(DataType.Password)]
		[Display(Name="Password")]
		[MaxLength(50)]
		[Required(ErrorMessage="Password is required.")]
		[StringLength(50, MinimumLength=8, ErrorMessage="Must have min length of 8 and max Length of 50")]
		public string PasswordField
		{
			get;
			set;
		}

		[MaxLength(200)]
		public string Path
		{
			get;
			set;
		}

		[Required(ErrorMessage="Phone is required.")]
		[StringLength(50, MinimumLength=3, ErrorMessage="Must have min length of 3 and max Length of 50")]
		public string Phone
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Position
		{
			get;
			set;
		}

		[MaxLength(10)]
		public string SecurityCode
		{
			get;
			set;
		}

		[MaxLength(500)]
		public string Twitter
		{
			get;
			set;
		}

		public Agents()
		{
		}
	}
}