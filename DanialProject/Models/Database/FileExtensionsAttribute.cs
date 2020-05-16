using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace DanialProject.Models.Database
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false, Inherited=true)]
	public class FileExtensionsAttribute : ValidationAttribute
	{
		private List<string> AllowedExtensions
		{
			get;
			set;
		}

		public FileExtensionsAttribute(string fileExtensions)
		{
			this.AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
		}

		public override bool IsValid(object value)
		{
			HttpPostedFileBase file = value as HttpPostedFileBase;
			if (file == null)
			{
				return true;
			}
			string fileName = file.FileName;
			return this.AllowedExtensions.Any<string>((string y) => fileName.EndsWith(y));
		}
	}
}