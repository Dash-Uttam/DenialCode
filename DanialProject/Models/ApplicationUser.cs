using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DanialProject.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			return await manager.CreateIdentityAsync(this, "ApplicationCookie");
		}
	}
}