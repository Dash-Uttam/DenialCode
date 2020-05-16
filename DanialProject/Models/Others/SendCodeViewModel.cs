using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace DanialProject.Models.Others
{
	public class SendCodeViewModel
	{
		public ICollection<SelectListItem> Providers
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

		public string SelectedProvider
		{
			get;
			set;
		}

		public SendCodeViewModel()
		{
		}
	}
}