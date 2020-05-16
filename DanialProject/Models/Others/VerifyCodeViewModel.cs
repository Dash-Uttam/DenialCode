using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Others
{
	public class VerifyCodeViewModel
	{
		[Display(Name="Code")]
		[Required]
		public string Code
		{
			get;
			set;
		}

		[Required]
		public string Provider
		{
			get;
			set;
		}

		[Display(Name="Remember this browser?")]
		public bool RememberBrowser
		{
			get;
			set;
		}

		public bool RememberMe
		{
			get;
			set;
		}

		public string ReturnUrl
		{
			get;
			set;
		}

		public VerifyCodeViewModel()
		{
		}
	}
}