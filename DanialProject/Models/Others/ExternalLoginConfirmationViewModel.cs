using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Others
{
	public class ExternalLoginConfirmationViewModel
	{
		public string Email
		{
			get;
			set;
		}

		[Display(Name="User Name")]
		[Required]
		public string Name
		{
			get;
			set;
		}

		public ExternalLoginConfirmationViewModel()
		{
		}
	}
}