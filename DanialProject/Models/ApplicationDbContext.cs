using Microsoft.AspNet.Identity.EntityFramework;
using DanialProject.Models;
using System;

namespace DanialProject.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base("DefaultConnection", false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}