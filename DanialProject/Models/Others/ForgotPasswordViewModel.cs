using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Others
{
	public class ForgotPasswordViewModel
	{
		[Display(Name="Email")]
		[EmailAddress]
		[Required]
		public string Email
		{
			get;
			set;
		}

		public ForgotPasswordViewModel()
		{
		}
	}
}