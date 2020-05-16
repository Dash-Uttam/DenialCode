using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Others
{
	public class RegisterViewModel
	{
		[Compare("Password", ErrorMessage="The password and confirmation password do not match.")]
		[DataType(DataType.Password)]
		[Display(Name="Confirm password")]
		public string ConfirmPassword
		{
			get;
			set;
		}

		[Display(Name="Email")]
		[EmailAddress]
		[Required]
		public string Email
		{
			get;
			set;
		}

		[DataType(DataType.Password)]
		[Display(Name="Password")]
		[Required]
		[StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6)]
		public string Password
		{
			get;
			set;
		}

		public RegisterViewModel()
		{
		}
	}
}